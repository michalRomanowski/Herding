using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Simulation;
using System.Security.AccessControl;
using DBManager;

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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxNazwa.Text == "")
            {
                MessageBox.Show("Wprowadź nazwę.");
                return;
            }

            Save(textBoxNazwa.Text);

            Close();
            Dispose();
        }

        private void FSave_FormClosed(object sender, FormClosedEventArgs e)
        {
            fSim.Enabled = true;
        }

        private void Save(string saveName)
        {
            try
            {
                DBManagement.SaveSimulationParameters(saveName, fSim.SimulationParameters.Compress());
                DBManagement.SavePopulation($"{saveName}", fSim.Shepards.Compress());
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n\nMake sure that name is valid for table name for SQLite.");
            }
        }
    }
}
