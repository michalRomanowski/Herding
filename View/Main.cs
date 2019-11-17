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
using Repository;

namespace View
{
    public partial class Main : Form
    {
        private OptimizationParameters optimizationParameters;
        private Team shepherds;

        private IViewableWorld world;

        private bool paint = false;

        private XMLRepository repository = new XMLRepository();
        
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            new Thread(Repainting).Start();
        }

        private void World_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(world != null)
                world.Stop();

            Application.Exit();
        }

        #region Loading
        
        private void buttonLoadOptimizationParameters_Click(object sender, EventArgs e)
        {
            openFileDialogOptimizationParameters.ShowDialog();
        }

        private void openFileDialogOptimizationParameters_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                optimizationParameters = repository.LoadOptimizationParametersByPath(openFileDialogOptimizationParameters.FileName);
                labelOptimizationParameters.Text = "Optimization: " + openFileDialogOptimizationParameters.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonLoadShepherds_Click(object sender, EventArgs e)
        {
            openFileDialogShepherds.ShowDialog();
        }

        private void openFileDialogShepherds_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                var path = Path.GetDirectoryName(openFileDialogShepherds.FileName);

                shepherds = repository.LoadTeam(path, optimizationParameters);
                labelShepherds.Text = "Shepherds: " + path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Buttons

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
            
            shepherds = repository.LoadTeam(Path.GetDirectoryName(openFileDialogShepherds.FileName), optimizationParameters);

            shepherds.ClearPath();
            shepherds.SetPositions(optimizationParameters.PositionsOfShepherds);

            var sheep = AgentFactory.GetSheep(
                optimizationParameters.PositionsOfSheep,
                optimizationParameters.SheepType,
                optimizationParameters.SeedForRandomSheepForBest);

            world = new ViewableWorld(shepherds, sheep, optimizationParameters.NumberOfSeenSheep, optimizationParameters.NumberOfSeenShepherds);

            world.Start(optimizationParameters.TurnsOfHerding);

            paint = true;
        }

        private void ButtonStepBack_Click(object sender, EventArgs e)
        {
            world.StepBack();
        }

        private void ButtonSavePositions_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Directory.CreateDirectory(SaveFileDialog.FileName);
                world.SavePositions(SaveFileDialog.FileName);
            }
        }

        private void ButtonStepForward_Click(object sender, EventArgs e)
        {
            world.StepForward();
        }

        private void ButtonRandomizePositions_Click(object sender, EventArgs e)
        {
            optimizationParameters.PositionsOfShepherds = optimizationParameters.PositionsOfShepherds.Randmize().ToList();
            optimizationParameters.PositionsOfSheep = optimizationParameters.PositionsOfSheep.Randmize().ToList();
        }

        #endregion

        #region Painting

        private void Repainting()
        {
            while (true)
            {
                if (paint)
                {
                    Invalidate();

                    Thread.Sleep(33);
                }
            }
        }

        private void FWorld_Paint(object sender, PaintEventArgs e)
        {
            if (paint)
            {
                var drawingFlags = new DrawingFlags()
                {
                    DrawSheepSight = CheckBoxSheepSight.Checked,
                    DrawShepherdsSight = CheckBoxShepherdSight.Checked,
                    DrawSheepPath = CheckBoxSheepPath.Checked,
                    DrawShepherdsPath = CheckBoxShepherdsPath.Checked,
                    DrawCenterOfSheep = CheckBoxCenterOfGravity.Checked
                };
                
                world.Draw(e.Graphics, drawingFlags);

                labelFitness.Text = "Total Distances:\n" +
                        world.Sheep.Select(s => s.Position).SumOfDistancesFromCenter();

                labelEra.Text = "Era: " + world.Step;
            }
        }

        #endregion
    }
}
