using System;
using System.Windows.Forms;
using EFDatabase;

namespace View
{
    public partial class FSave : Form
    {
        FSimulation fSim;

        public FSave(FSimulation fSim)
        {
            InitializeComponent();

            this.fSim = fSim;
            this.MdiParent = fSim.MdiParent;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (TextBoxNazwa.Text == "")
            {
                MessageBox.Show("Insert name.");
                return;
            }

            Save(TextBoxNazwa.Text);

            Close();
            Dispose();
        }

        private void FSave_FormClosed(object sender, FormClosedEventArgs e)
        {
            fSim.Enabled = true;
        }

        private void Save(string saveName)
        {
            OptimizationInstance.Optimization.Name = saveName;

            try
            {
                new EFDatabaseManager().SaveOptimization(OptimizationInstance.Optimization);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
