using Agent;
using Auxiliary;
using Simulations;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using World;
using System.Linq;
using Teams;

namespace View
{
    public partial class FWorld : Form
    {
        private Team shepherds;

        private IViewableWorld world;

        protected SimulationParameters simulationParameters;

        public FWorld(SimulationParameters simulationParameters, Team shepherds)
        {
            this.simulationParameters = simulationParameters;

            InitializeComponent();

            this.shepherds = shepherds;
            shepherds.SetPositions(simulationParameters.PositionsOfShepherds);

            var sheep = AgentFactory.GetSheep(
                simulationParameters.PositionsOfSheep,
                simulationParameters.SheepType,
                simulationParameters.SeedForRandomSheepForBest);

            world = new ViewableWorld(shepherds, sheep, simulationParameters.NumberOfSeenSheep, simulationParameters.NumberOfSeenShepherds);            
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            new Thread(Repainting).Start();
        }

        private void ButtonPause_Click(object sender, EventArgs e)
        {
            world.Pause();

            ButtonStepBack.Enabled = true;
            ButtonStepForward.Enabled = true;

            ButtonResume.Enabled = true;
            ButtonPause.Enabled = false;
        }

        private void ButtonResume_Click(object sender, EventArgs e)
        {
            world.Resume();

            ButtonStepBack.Enabled = false;
            ButtonStepForward.Enabled = false;

            ButtonResume.Enabled = false;
            ButtonPause.Enabled = true;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            ButtonPause.Enabled = true;
            ButtonResume.Enabled = false;

            ButtonStepBack.Enabled = false;
            ButtonStepForward.Enabled = false;

            world.Stop();

            shepherds.ClearPath();
            shepherds.SetPositions(simulationParameters.PositionsOfShepherds);

            var sheep = AgentFactory.GetSheep(
                simulationParameters.PositionsOfSheep,
                simulationParameters.SheepType,
                simulationParameters.SeedForRandomSheepForBest);

            world = new ViewableWorld(shepherds, sheep, simulationParameters.NumberOfSeenSheep, simulationParameters.NumberOfSeenShepherds);            

            world.Start(simulationParameters.TurnsOfHerding);
        }

        private void Repainting()
        {
            while (true)
            {
                Invalidate();
                Thread.Sleep(33);
            }
        }

        private void FWorld_Paint(object sender, PaintEventArgs e)
        {
            var drawingFlags = new DrawingFlags()
            {
                DrawSheepSight = CheckBoxSheepSight.Checked,
                DrawShepherdsSight = CheckBoxShepherdSight.Checked,
                DrawSheepPath = CheckBoxSheepPath.Checked,
                DrawShepherdsPath = CheckBoxShepherdsPath.Checked,
                DrawCenterOfSheep = CheckBoxCenterOfGravity.Checked
            };

            world.Draw(e.Graphics, 10, 10, drawingFlags);
            
            labelFitness.Text = "Fitness:\n" + 
                    world.Sheep.Select(s => s.Position).SumOfDistancesFromCenter();

            labelEra.Text = "Step: " + world.Step;
        }

        private void ButtonStepForward_Click(object sender, EventArgs e)
        {
            world.StepForward();
        }

        private void ButtonStepBack_Click(object sender, EventArgs e)
        {
            world.StepBack();
        }

        private void FWorld_FormClosing(object sender, FormClosingEventArgs e)
        {
            world.Stop();
        }

        private void ButtonSavePositions_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = SaveFileDialog.FileName;

                Directory.CreateDirectory(path);

                world.SavePositions(path);
            }
        }
    }
}
