using System;
using System.Windows.Forms;
using EFDatabase;

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
            ListBoxSimulations.Items.Clear();

            try
            {
                foreach (var name in new EFDatabaseManager().LoadOptimizationsNames())
                        ListBoxSimulations.Items.Add(name);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            if(ListBoxSimulations.SelectedIndex == -1)
            {
                Close();
                Dispose();
            }

            try
            {
                var selectedName = ListBoxSimulations.SelectedItem.ToString();

                OptimizationInstance.Optimization = new EFDatabaseManager().LoadOptimization(selectedName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            fSim.SetControls();
            fSim.BlockControlsAfterPopulationCreation();

            Close();
            Dispose();
        }

        private void FLoad_FormClosed(object sender, FormClosedEventArgs e)
        {
            fSim.Enabled = true;
        }
    }
}
