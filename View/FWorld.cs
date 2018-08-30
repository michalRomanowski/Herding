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
        private Team shepards;

        private IViewableWorld world;

        private SimulationParameters simulationParameters;

        public FWorld(SimulationParameters simulationParameters, Team shepards)
        {
            this.simulationParameters = simulationParameters;

            InitializeComponent();

            this.shepards = shepards;
            shepards.SetPositions(simulationParameters.PositionsOfShepards);

            var sheep = SheepProvider.GetSheep(
                simulationParameters.PositionsOfSheep,
                simulationParameters.SheepType,
                simulationParameters.SeedForRandomSheepForBest);

            world = new ViewableWorld(shepards, sheep);            
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            new Thread(Repainting).Start();
        }


        private void brainToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

            shepards.ClearPath();
            shepards.SetPositions(simulationParameters.PositionsOfShepards);

            var sheep = SheepProvider.GetSheep(
                simulationParameters.PositionsOfSheep,
                simulationParameters.SheepType,
                simulationParameters.SeedForRandomSheepForBest);

            world = new ViewableWorld(shepards, sheep);            

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
            world.Draw(e.Graphics, 10, 10, CheckBoxShepardSight.Checked, CheckBoxSheepSight.Checked, CheckBoxCentreOfGravity.Checked, CheckBoxShepardsPath.Checked, CheckBoxSheepPath.Checked);
            
            labelFitness.Text = "Fitness:\n" + 
                CenterOfGravityCalculator.SumOfDistancesFromCenterOfGravity(
                    world.Sheep.Select(s => s.Position).ToList());

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
