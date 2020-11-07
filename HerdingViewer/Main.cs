using Agent;
using Auxiliary;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using World;
using Teams;
using Repository;
using System.Xml.Serialization;
using Simulations.Parameters;
using System.Text;

namespace HerdingViewer
{
    public partial class Main : Form
    {
        private const int HerdingX = 600;
        private const int HerdingY = 5;
        private const int MilisecondsBetweenSteps = 33;

        private IViewableWorld world;

        private bool paint = false;

        private XMLRepository repository = new XMLRepository();
        
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            richTextBoxShepherds.Text = Properties.Resources.SmapleShepherds;
            richTextBoxParameters.Text = Properties.Resources.SampleParameters;

            new Thread(Repainting).Start();
        }

        private void World_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(world != null)
                world.Stop();

            Application.Exit();
            Dispose();
        }

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
            try
            {
                StartHerding();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Instance.AddLine(ex.Message);
            }
        }
        
        private void StartHerding()
        {
            ButtonPause.Enabled = true;
            ButtonResume.Enabled = false;

            ButtonStepBack.Enabled = false;
            ButtonStepForward.Enabled = false;

            TransformPositions();
            var parameters = (OptimizationParameters)new XmlSerializer(typeof(OptimizationParameters)).Deserialize(new StringReader(richTextBoxParameters.Text));

            if (checkBoxRandom.Checked)
            {
                parameters.PositionsOfShepherds = parameters.PositionsOfShepherds.Randomize(0, 400).ToList();
                parameters.PositionsOfSheep = parameters.PositionsOfSheep.Randomize(0, 400).ToList();
            }

            var sheep = AgentFactory.GetSheep(parameters.PositionsOfSheep, parameters.SheepType, parameters.SeedForRandomSheepForBest);

            var shepherds = richTextBoxShepherds.Text.Split('\n')
                .Where(x => string.IsNullOrEmpty(x) == false)
                .Select(x => repository.LoadShepherd(x));

            var team = TeamFactory.GetNotIdenticalTeam(shepherds.Cast<ThinkingAgent>().ToList());
            team.Resize(parameters.PositionsOfShepherds.Count());
            team.SetPositions(parameters.PositionsOfShepherds);

            world = new ViewableWorld(
                HerdingX,
                HerdingY,
                checkBoxAnimationMode.Checked ? MilisecondsBetweenSteps : 0,
                team,
                sheep);

            world.Start(parameters.TurnsOfHerding);
            
            paint = true;
        }

        private void TransformPositions()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var line in richTextBoxParameters.Lines)
            {
                string l = line.Trim();

                if (string.IsNullOrEmpty(l))
                    continue;

                if (!char.IsDigit(l[0]))
                {
                    sb.AppendLine(line);
                    continue;
                }

                var xStr = l.Split(' ')[0];
                var yStr = l.Split(' ')[1];

                sb.AppendLine("<Vector2D X=\"" + xStr + "\" Y = \"" + yStr + "\" />");
            }

            richTextBoxParameters.Text = sb.ToString();
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

                labelFitness.Text = "Sum of distances:\n" +
                        world.Sheep.Select(s => s.Position).SumOfDistancesFromCenter();

                labelStep.Text = "Step: " + world.StepCount;
                labelFitness.Text = "Sum of distances: " + world.Sheep.Select(x => x.Position).SumOfDistancesFromCenter();
            }
        }

        #endregion
    }
}
