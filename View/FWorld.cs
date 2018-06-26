using Agent;
using Auxiliary;
using Simulation;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using World;
using System.Linq;
using Team;

namespace View
{
    public partial class FWorld : Form
    {
        private ITeam shepards;
        private ITeam wolfs;

        private IViewableWorld world;

        private SimulationParameters simulationParameters;

        public FWorld(SimulationParameters simulationParameters, ITeam shepards, ITeam wolfs)
        {
            this.simulationParameters = simulationParameters;

            InitializeComponent();

            this.shepards = shepards;
            shepards.SetPositions(simulationParameters.PositionsOfShepards);

            this.wolfs = wolfs;

            var sheep = SheepProvider.GetSheep(
                simulationParameters.PositionsOfSheep,
                simulationParameters.SheepType,
                simulationParameters.SeedForRandomSheepForBest);

            world = new ViewableWorld(shepards, sheep, wolfs);            
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            new Thread(Repainting).Start();
        }


        private void brainToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            world.Pause();

            buttonStepBack.Enabled = true;
            buttonStepForward.Enabled = true;

            buttonResume.Enabled = true;
            buttonPause.Enabled = false;
        }

        private void buttonResume_Click(object sender, EventArgs e)
        {
            world.Resume();

            buttonStepBack.Enabled = false;
            buttonStepForward.Enabled = false;

            buttonResume.Enabled = false;
            buttonPause.Enabled = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonPause.Enabled = true;
            buttonResume.Enabled = false;

            buttonStepBack.Enabled = false;
            buttonStepForward.Enabled = false;

            world.Stop();

            shepards.ClearPath();
            shepards.SetPositions(simulationParameters.PositionsOfShepards);

            wolfs.ClearPath();

            var sheep = SheepProvider.GetSheep(
                simulationParameters.PositionsOfSheep,
                simulationParameters.SheepType,
                simulationParameters.SeedForRandomSheepForBest);

            world = new ViewableWorld(shepards, sheep, wolfs);            

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
            world.Draw(e.Graphics, 10, 10, checkBoxShepardSight.Checked, checkBoxSheepSight.Checked, checkBoxCentreOfGravity.Checked, checkBoxShepardsPath.Checked, checkBoxSheepPath.Checked);
            
            labelFitness.Text = "Fitness:\n" + 
                CenterOfGravityCalculator.SumOfDistancesFromCenterOfGravity(
                    world.Sheep.Select(s => s.Position).ToList());

            labelEra.Text = "Era: " + world.Step;
        }

        private void buttonStepForward_Click(object sender, EventArgs e)
        {
            world.StepForward();
        }

        private void buttonStepBack_Click(object sender, EventArgs e)
        {
            world.StepBack();
        }

        private void FWorld_FormClosing(object sender, FormClosingEventArgs e)
        {
            world.Stop();
        }

        private void buttonSavePositions_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = saveFileDialog.FileName;

                Directory.CreateDirectory(path);

                world.SavePositions(path);
            }
        }
    }
}
