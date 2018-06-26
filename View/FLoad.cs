using Simulation;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DBManager;

namespace View
{
    public partial class FLoad : Form
    {
        private FSimulation fSim;

        public FLoad(FSimulation fSim)
        {
            InitializeComponent();

            this.fSim = fSim;
            MdiParent = fSim.MdiParent;
        }

        private void FLoad_Load(object sender, EventArgs e)
        {
            listBoxSimulations.Items.Clear();

            foreach (string dir in DBManagement.LoadSimulationsNames())
            {
                string[] splittedDir = dir.Split('\\');
                listBoxSimulations.Items.Add(splittedDir.Last());
            }
        }

        private void FLoad_FormClosed(object sender, FormClosedEventArgs e)
        {
            fSim.Enabled = true;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if(listBoxSimulations.SelectedIndex == -1)
            {
                Close();
                Dispose();
            }

            try
            {
                var selectedName = listBoxSimulations.SelectedItem.ToString();

                fSim.SimulationParameters = SimulationParameters.Decompress(DBManagement.LoadSimulationsParameters(selectedName));

                fSim.Populations = new Populations(fSim.SimulationParameters)
                {
                    Shepards = new Population(fSim.SimulationParameters, DBManagement.LoadPopulation(selectedName))
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            fSim.SetControls();
            fSim.BlockControlsAfterPopulationCreation();

            Close();
            Dispose();
        }
    }
}
