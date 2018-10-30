using System;
using System.Windows.Forms;
using EFDatabase;
using Simulations;

namespace View
{
    public partial class FLoadControlFitnessCounter : Form
    {
        private FSimulation fSim;
        
        public FLoadControlFitnessCounter(FSimulation fSim)
        {
            InitializeComponent();

            this.fSim = fSim;
            MdiParent = fSim.MdiParent;
        }
        
        private void FLoadControlFitnessCounter_Load(object sender, EventArgs e)
        {
            ListBoxSimulations.Items.Clear();

            try
            {
                foreach (var name in new EFDatabaseManager().LoadOptimizationsNames())
                    ListBoxSimulations.Items.Add(name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            if (ListBoxSimulations.SelectedIndex == -1)
            {
                Close();
                Dispose();
            }

            try
            {
                var selectedName = ListBoxSimulations.SelectedItem.ToString();

                var controlFitnessCounter = new EFDatabaseManager().LoadSimulationParameters(selectedName).GetBestTeamSelectorParameters();

                fSim.Optimization.SetControlFitnessCounter(
                    FitnessCounterFactory.GetFitnessCounter(controlFitnessCounter.FitnessType, controlFitnessCounter.CountFitnessParameters),
                    controlFitnessCounter.CountFitnessParameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            fSim.BlockControlsAfterPopulationCreation();

            Close();
            Dispose();
        }

        private void FLoadControlFitnessCounter_FormClosed(object sender, FormClosedEventArgs e)
        {
            fSim.Enabled = true;
        }
    }
}
