using Agent;
using Auxiliary;
using Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer;
using Team;
using System.ComponentModel;

namespace View
{
    public partial class FSimulation : Form
    {
        public SimulationParameters SimulationParameters = new SimulationParameters();
        public Populations Populations;

        private List<Form> children = new List<Form>();

        public Optimizer optimizer = new Optimizer();

        private Thread optimizationThread;

        private CTimer timer;

        private BindingList<Position> positionsOfShepardsBindingList
        {
            get
            {
                return new BindingList<Position>(SimulationParameters.PositionsOfShepards);
            }
            set
            {
                SimulationParameters.PositionsOfShepards = value.ToList();
            }
        }

        private BindingList<Position> positionsOfSheepBindingList
        {
            get
            {
                return new BindingList<Position>(SimulationParameters.PositionsOfSheep);
            }
            set
            {
                SimulationParameters.PositionsOfSheep = value.ToList();
            }
        }

        private State state
        {
            get { return _state; }
            set
            {
                if (value == State.CountingFitness)
                {
                    labelStatus.Text = "Calculating Fitness";

                    BlockControls();
                }
                else if (value == State.Idle)
                {
                    labelStatus.Text = "Idle";

                    UnblockControls();
                }
                else if (value == State.Optimization)
                {
                    labelStatus.Text = "Optimization";

                    BlockControls();
                }

                _state = value;
            }
        }

        private State _state = State.Idle;

        public FSimulation()
        {
            InitializeComponent();

            comboBoxShepardsFitnessCryteria.DataSource = Enum.GetValues(typeof(EFitnessType));

            dataGridViewShepards.DataSource = positionsOfShepardsBindingList;
            dataGridViewSheep.DataSource = positionsOfSheepBindingList;

            timer = new CTimer(1000);
            timer.Add(1, new VoidDelegate(UpdateProgressBar));

            numericUpDownOptimizationSteps.Maximum = Int32.MaxValue;

            state = State.Idle;

            labelStep.Text = "0";
            labelBestFitness2.Text = "0";
        }

        private void GetSimulationParameters()
        {
            SimulationParameters.AbsoluteMutationFactor = (float)numericUpDownAbsoluteMutationFactor.Value;
            SimulationParameters.NumberOfChildren = (int)numericUpDownNumberOfChildren.Value;
            SimulationParameters.NumberOfHiddenLayers = (int)numericUpDownNumberOfShepardsHiddenLayers.Value;
            SimulationParameters.NumberOfNeuronsInHiddenLayer = (int)numericUpDownWolfsHiddenLayerSize.Value;
            SimulationParameters.NumberOfParticipants = (int)numericUpDownNumberOfParticipants.Value;
            SimulationParameters.NumberOfSeenShepards = (int)numericUpDownShepardShepardSight.Value;
            SimulationParameters.NumberOfSeenSheep = (int)numericUpDownShepardSheepSight.Value;
            SimulationParameters.OptimizationSteps = (int)numericUpDownOptimizationSteps.Value;
            SimulationParameters.PopulationSize = (int)numericUpDownPopulationSize.Value;
            SimulationParameters.MutationPower = (float)numericUpDownMutationPower.Value;
            SimulationParameters.TurnsOfHerding = (int)numericUpDownNumberOfTurnsOfHerding.Value;
            SimulationParameters.RandomPositions = checkBoxRandomPositions.Checked;
            SimulationParameters.NumberOfRandomSets = (int)numericUpDownNumberOfrandomSets.Value;
            SimulationParameters.SheepType = checkBoxRandomSheep.Checked ? ESheepType.Wandering : ESheepType.Passive;
            SimulationParameters.FitnessType = (EFitnessType)comboBoxShepardsFitnessCryteria.SelectedIndex;
            SimulationParameters.NotIdenticalAgents = checkBoxNotIdenticalAgents.Checked;
        }

        public void SetControls()
        {
            numericUpDownAbsoluteMutationFactor.Value = (decimal)SimulationParameters.AbsoluteMutationFactor;
            numericUpDownNumberOfChildren.Value = SimulationParameters.NumberOfChildren;
            numericUpDownNumberOfShepardsHiddenLayers.Value = SimulationParameters.NumberOfHiddenLayers;
            numericUpDownWolfsHiddenLayerSize.Value = SimulationParameters.NumberOfNeuronsInHiddenLayer;
            numericUpDownNumberOfParticipants.Value = SimulationParameters.NumberOfParticipants;
            numericUpDownNumberOfTurnsOfHerding.Value = SimulationParameters.TurnsOfHerding;
            numericUpDownPopulationSize.Value = SimulationParameters.PopulationSize;
            numericUpDownOptimizationSteps.Value = SimulationParameters.OptimizationSteps;
            numericUpDownMutationPower.Value = (decimal)SimulationParameters.MutationPower;

            comboBoxPopulation.Items.Clear();

            for (int i = 0; i < Populations.Shepards.Units.Count(); i++)
            {
                comboBoxPopulation.Items.Add(i.ToString());
            }

            checkBoxRandomPositions.Checked = SimulationParameters.RandomPositions;
            checkBoxRandomSheep.Checked = SimulationParameters.SheepType == ESheepType.Wandering ? true : false;

            numericUpDownNumberOfrandomSets.Value = SimulationParameters.NumberOfRandomSets;
            numericUpDownNumberOfRandomSetsForBest.Value = SimulationParameters.RandomSetsForBest.Count;

            comboBoxShepardsFitnessCryteria.SelectedIndex = (int)SimulationParameters.FitnessType;

            checkBoxNotIdenticalAgents.Checked = SimulationParameters.NotIdenticalAgents;

            dataGridViewShepards.DataSource = positionsOfShepardsBindingList;
            dataGridViewSheep.DataSource = positionsOfSheepBindingList;
            dataGridViewShepards.Refresh();
            dataGridViewSheep.Refresh();

            numericUpDownShepardShepardSight.Maximum = SimulationParameters.NumberOfSeenShepards > 0 ? SimulationParameters.NumberOfSeenShepards : 0;
            numericUpDownShepardShepardSight.Value = SimulationParameters.NumberOfSeenShepards;
            numericUpDownShepardSheepSight.Maximum = SimulationParameters.NumberOfSeenSheep > 0 ? SimulationParameters.NumberOfSeenSheep : 0;
            numericUpDownShepardSheepSight.Value = SimulationParameters.NumberOfSeenSheep;

            buttonShowHerdingOfBestTeam.Enabled = Populations != null && Populations.Shepards != null && Populations.Shepards.Best != null;
            buttonCountFitness.Enabled = Populations != null && Populations.Shepards != null;
        }

        private void Optimize()
        {
            optimizer.Optimize(SimulationParameters, Populations);
        }

        private void UpdateProgressBar()
        {
            float progress = SimulationParameters.Progress;

            progressBarSimulation.Value = (int)(progress * 100);

            if (progress > 0)
            {
                if (Populations.Shepards.Best != null)
                {
                    labelBestFitness2.Text = Populations.Shepards.Best.Fitness.ToString();
                }

                labelStep.Text = optimizer.StepCount.ToString();
            }


            if (state == State.CountingFitness)
            {
                return;
            }
        }

        private void buttonDodajLosowegoAgenta_Click(object sender, EventArgs e)
        {
            Random r = new Random();

            SimulationParameters.PositionsOfShepards.Add(new Position(r.Next(400), r.Next(400)));
        }

        private void buttonDodajLosowaOwce_Click(object sender, EventArgs e)
        {
            Random r = new Random();

            SimulationParameters.PositionsOfSheep.Add(new Position(r.Next(400), r.Next(400)));
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Logger.Clear();

            if (SimulationParameters.NumberOfShepards == 0)
            {
                MessageBox.Show("Add at least one Shepard.");
                return;
            }

            if (SimulationParameters.NumberOfSheep < 2)
            {
                MessageBox.Show("Add at least two Sheep.");
                return;
            }

            if (numericUpDownNumberOfChildren.Value * numericUpDownNumberOfParticipants.Value > numericUpDownPopulationSize.Value)
            {
                MessageBox.Show("Tournament size too big.");
                return;
            }
            
            numericUpDownPopulationSize.Enabled = false;

            state = State.Optimization;

            GetSimulationParameters();

            if (checkBoxRandomPositions.Checked && 
                (SimulationParameters.RandomSetsForBest.Count != (int)numericUpDownNumberOfRandomSetsForBest.Value ||
                SimulationParameters.RandomSetsForBest.PositionsOfShepardsSet.First().Count() != SimulationParameters.NumberOfShepards ||
                SimulationParameters.RandomSetsForBest.PositionsOfSheepSet.First().Count() != SimulationParameters.NumberOfSheep))
            {
                SimulationParameters.RandomSetsForBest = new RandomSetsList(
                    (int)numericUpDownNumberOfRandomSetsForBest.Value,
                    SimulationParameters.NumberOfShepards,
                    SimulationParameters.NumberOfSheep,
                    SimulationParameters.NumberOfWolfs,
                    DateTime.Now.Millisecond);
            }

            if (Populations == null || Populations.Shepards == null || Populations.Shepards.Units == null || Populations.Shepards.Units.Count == 0)
            {
                MessageBox.Show("Random Population created.");
                CreateRandomPopulation();

                SimulationParameters.SeedForRandomSheepForBest = DateTime.Now.Millisecond;
            }
            else
                Populations.Shepards.AdjustInputLayerSize(
                    SimulationParameters.NumberOfSeenShepards, SimulationParameters.NumberOfSeenSheep);

            foreach (Form f in children)
            {
                f.Close();
                f.Dispose();
            }

            Populations.Shepards.AdjustTeamSize(SimulationParameters.NumberOfShepards);
            
            timer.Start();

            optimizationThread = new Thread(new ThreadStart(Optimize));
            optimizationThread.Start();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Populations.Shepards.Units == null)
                return;

            Enabled = false;
            new FSave(this).Show();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            KillChildren();

            Enabled = false;
            new FLoad(this).Show();
        }

        private void buttonRandomPopulation_Click(object sender, EventArgs e)
        {
            CreateRandomPopulation();
        }

        private void CreateRandomPopulation()
        {
            GetSimulationParameters();
            
            Populations = new Populations(SimulationParameters);
            Populations.Shepards = new Population(SimulationParameters);

            SetControls();
        }

        private void buttonShowHerding_Click(object sender, EventArgs e)
        {
            if (comboBoxPopulation.SelectedIndex == -1)
                return;

            GetSimulationParameters();
            Populations.Shepards.AdjustTeamSize(SimulationParameters.NumberOfShepards);

            children.Add(new FWorld(SimulationParameters, Populations.Shepards.Units[comboBoxPopulation.SelectedIndex], Populations.Wolfs.Best));
            children.Last().Show();
        }

        private void numericUpDownNumberOfChildren_ValueChanged(object sender, EventArgs e)
        {
            int children = (int)numericUpDownNumberOfChildren.Value;

            if (children % 2 != 0)
            {
                children -= 1;
            }

            numericUpDownNumberOfChildren.Value = children;
            SimulationParameters.NumberOfChildren = children;
        }

        private void BlockControls()
        {
            KillChildren();

            panelOptimization.Enabled = false;
            panelPopulation.Enabled = false;
            panelRandomPositions.Enabled = false;
            panelSheep.Enabled = false;
            panelShepards.Enabled = false;

            buttonLoad.Enabled = false;
            buttonSave.Enabled = false;
            buttonStart.Enabled = false;
            buttonRandomPositions.Enabled = false;

            buttonStop.Enabled = true;

            labelBestFitness.Text = "";
            labelAverage.Text = "";
        }

        public void BlockControlsAfterPopulationCreation()
        {
            numericUpDownWolfsHiddenLayerSize.Enabled = false;
            numericUpDownNumberOfShepardsHiddenLayers.Enabled = false;
            numericUpDownPopulationSize.Enabled = false;
        }

        private void UnblockControls()
        {
            KillChildren();

            panelOptimization.Enabled = true;
            panelPopulation.Enabled = true;
            panelRandomPositions.Enabled = true;
            panelSheep.Enabled = true;
            panelShepards.Enabled = true;

            buttonLoad.Enabled = true;
            buttonSave.Enabled = true;
            buttonStart.Enabled = true;
            buttonRandomPositions.Enabled = true;

            buttonStop.Enabled = false;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            lock (optimizer.StopLocker)
            {
                optimizer.Stop = true;
            }

            optimizationThread.Join();

            SimulationParameters.Progress = 1;
            
            state = State.Idle;
            
            Logger.Flush();
        }

        public void CountFitness()
        {
            state = State.CountingFitness;

            BlockControls();

            Populations.Shepards.CountAverageFitness(SimulationParameters);

            new BestTeamManager().UpdateBestTeam(SimulationParameters, Populations.Shepards, Populations.Shepards.Units);

            labelAverage.Text = "Average Fitness: " + Populations.Shepards.AverageFitness.ToString();
            labelBestFitness.Text = "Best Fitness: " + Populations.Shepards.Best.Fitness.ToString();

            state = State.Idle;

            UnblockControls();
        }

        private enum State
        {
            Idle,
            Optimization,
            CountingFitness,
        }

        private void buttonRandomPositions_Click(object sender, EventArgs e)
        {
            Random r = new Random();

            for (int i = 0; i < SimulationParameters.PositionsOfShepards.Count; i++)
            {
                SimulationParameters.PositionsOfShepards[i] = new Position(r.Next(400), r.Next(400));
            }

            for (int i = 0; i < SimulationParameters.PositionsOfSheep.Count; i++)
            {
                SimulationParameters.PositionsOfSheep[i] = new Position(r.Next(400), r.Next(400));
            }

            dataGridViewShepards.Refresh();
            dataGridViewSheep.Refresh();
        }

        private void buttonCountFitness_Click(object sender, EventArgs e)
        {
            foreach (ITeam t in Populations.Shepards.Units)
            {
                t.ResetFitness();
            }

            CountFitness();
        }

        private void KillChildren()
        {
            foreach (Form f in children)
            {
                f.Dispose();
            }
        }

        private void checkBoxRandomPositions_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownNumberOfrandomSets.Enabled = checkBoxRandomPositions.Checked;
            numericUpDownNumberOfRandomSetsForBest.Enabled = checkBoxRandomPositions.Checked;
        }

        private void buttonShowHerdingOfBestTeam_Click(object sender, EventArgs e)
        {
            GetSimulationParameters();

            Populations.Shepards.AdjustTeamSize(SimulationParameters.NumberOfShepards);

            children.Add(new FWorld(SimulationParameters, Populations.Shepards.Best, Populations.Wolfs.Best));
            children.Last().Show();
        }

        private void FSimulation_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewShepards_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            numericUpDownShepardShepardSight.Maximum = dataGridViewShepards.RowCount > 1 ? dataGridViewShepards.RowCount - 2 : 0;
        }

        private void dataGridViewShepards_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            numericUpDownShepardShepardSight.Maximum = dataGridViewShepards.RowCount > 1 ? dataGridViewShepards.RowCount - 2 : 0;
        }

        private void dataGridViewSheep_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            numericUpDownShepardSheepSight.Maximum = dataGridViewSheep.RowCount > 0 ? dataGridViewSheep.RowCount - 1 : 0;
        }

        private void dataGridViewSheep_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            numericUpDownShepardSheepSight.Maximum = dataGridViewSheep.RowCount > 0 ? dataGridViewSheep.RowCount - 1 : 0;
        }

        private void comboBoxPopulation_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonShowHerding.Enabled = comboBoxPopulation.SelectedIndex >= 0;
        }
    }
}
