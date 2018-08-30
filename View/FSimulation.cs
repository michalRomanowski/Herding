using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using Timer;
using Teams;
using Agent;
using Auxiliary;
using Simulations;

namespace View
{
    public partial class FSimulation : Form
    {
        private List<Form> children = new List<Form>();
        
        private Thread optimizationThread;

        private CTimer timer;

        private BindingList<Position> PositionsOfShepardsBindingList
        {
            get
            {
                return new BindingList<Position>(SimParameters.PositionsOfShepards);
            }
            set
            {
                SimParameters.PositionsOfShepards = value.ToList();
            }
        }
        private BindingList<Position> PositionsOfSheepBindingList
        {
            get
            {
                return new BindingList<Position>(OptimizationInstance.Optimization.Parameters.PositionsOfSheep);
            }
            set
            {
                OptimizationInstance.Optimization.Parameters.PositionsOfSheep = value.ToList();
            }
        }

        private State CorrentState
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

        private SimulationParameters SimParameters
        {
            get
            {
                return OptimizationInstance.Optimization.Parameters;
            }
            set
            {
                OptimizationInstance.Optimization.Parameters = value;
            }
        }
        private Population Shepards
        {
            get
            {
                return OptimizationInstance.Optimization.Shepards;
            }
            set
            {
                OptimizationInstance.Optimization.Shepards = value;
            }
        }

        private enum State
        {
            Idle,
            Optimization,
            CountingFitness,
        }

        public FSimulation()
        {
            InitializeComponent();

            ComboBoxShepardsFitnessCryteria.DataSource = Enum.GetValues(typeof(EFitnessType));

            DataGridViewShepards.DataSource = PositionsOfShepardsBindingList;
            DataGridViewSheep.DataSource = PositionsOfSheepBindingList;

            timer = new CTimer(1000);
            timer.Add(1, new VoidDelegate(UpdateProgressBar));

            NumericUpDownOptimizationSteps.Maximum = Int32.MaxValue;

            CorrentState = State.Idle;

            labelStep.Text = "0";
            labelBestFitness2.Text = "0";
        }

        private void GetSimulationParameters()
        {
            SimParameters.AbsoluteMutationFactor = (float)NumericUpDownAbsoluteMutationFactor.Value;
            SimParameters.NumberOfHiddenLayers = (int)NumericUpDownNumberOfShepardsHiddenLayers.Value;
            SimParameters.NumberOfNeuronsInHiddenLayer = (int)NumericUpDownHiddenLayerSize.Value;
            SimParameters.NumberOfParticipants = (int)NumericUpDownNumberOfParticipants.Value;
            SimParameters.NumberOfSeenShepards = (int)NumericUpDownShepardShepardSight.Value;
            SimParameters.NumberOfSeenSheep = (int)NumericUpDownShepardSheepSight.Value;
            SimParameters.OptimizationSteps = (int)NumericUpDownOptimizationSteps.Value;
            SimParameters.PopulationSize = (int)NumericUpDownPopulationSize.Value;
            SimParameters.MutationPower = (float)NumericUpDownMutationPower.Value;
            SimParameters.TurnsOfHerding = (int)NumericUpDownNumberOfTurnsOfHerding.Value;
            SimParameters.RandomPositions = CheckBoxRandomPositions.Checked;
            SimParameters.NumberOfRandomSets = (int)NumericUpDownNumberOfrandomSets.Value;
            SimParameters.SheepType = CheckBoxRandomSheep.Checked ? ESheepType.Wandering : ESheepType.Passive;
            SimParameters.FitnessType = (EFitnessType)ComboBoxShepardsFitnessCryteria.SelectedIndex;
            SimParameters.NotIdenticalAgents = CheckBoxNotIdenticalAgents.Checked;
        }

        public void SetControls()
        {
            NumericUpDownAbsoluteMutationFactor.Value = (decimal)SimParameters.AbsoluteMutationFactor;
            NumericUpDownNumberOfShepardsHiddenLayers.Value = SimParameters.NumberOfHiddenLayers;
            NumericUpDownHiddenLayerSize.Value = SimParameters.NumberOfNeuronsInHiddenLayer;
            NumericUpDownNumberOfParticipants.Value = SimParameters.NumberOfParticipants;
            NumericUpDownNumberOfTurnsOfHerding.Value = SimParameters.TurnsOfHerding;
            NumericUpDownPopulationSize.Value = SimParameters.PopulationSize;
            NumericUpDownOptimizationSteps.Value = SimParameters.OptimizationSteps;
            NumericUpDownMutationPower.Value = (decimal)SimParameters.MutationPower;

            ComboBoxPopulation.Items.Clear();

            for (int i = 0; i < OptimizationInstance.Optimization.Shepards.Units.Count(); i++)
                ComboBoxPopulation.Items.Add(i.ToString());

            CheckBoxRandomPositions.Checked = SimParameters.RandomPositions;
            CheckBoxRandomSheep.Checked = SimParameters.SheepType == ESheepType.Wandering ? true : false;

            if (SimParameters.RandomPositions)
            {
                NumericUpDownNumberOfrandomSets.Value = SimParameters.NumberOfRandomSets;
                NumericUpDownNumberOfrandomSetsForBest.Value = SimParameters.RandomSetsForBest.Count;
            }

            ComboBoxShepardsFitnessCryteria.SelectedIndex = (int)SimParameters.FitnessType;

            CheckBoxNotIdenticalAgents.Checked = SimParameters.NotIdenticalAgents;

            DataGridViewShepards.DataSource = PositionsOfShepardsBindingList;
            DataGridViewSheep.DataSource = PositionsOfSheepBindingList;
            DataGridViewShepards.Refresh();
            DataGridViewSheep.Refresh();

            NumericUpDownShepardShepardSight.Maximum = SimParameters.NumberOfSeenShepards > 0 ? SimParameters.NumberOfSeenShepards : 0;
            NumericUpDownShepardShepardSight.Value = SimParameters.NumberOfSeenShepards;
            NumericUpDownShepardSheepSight.Maximum = SimParameters.NumberOfSeenSheep > 0 ? SimParameters.NumberOfSeenSheep : 0;
            NumericUpDownShepardSheepSight.Value = SimParameters.NumberOfSeenSheep;

            ButtonShowHerdingOfBestTeam.Enabled = Shepards != null && Shepards.Best != null;
            ButtonCountFitness.Enabled = Shepards != null;
        }

        private void UpdateProgressBar()
        {
            float progress = SimParameters.Progress;

            ProgressBarSimulation.Value = (int)(progress * 100);

            if (progress > 0)
            {
                if (Shepards.Best != null)
                {
                    labelBestFitness2.Text = Shepards.Best.Fitness.ToString();
                }

                labelStep.Text = OptimizationInstance.Optimization.StepCount.ToString();
            }


            if (CorrentState == State.CountingFitness)
            {
                return;
            }
        }
        
        private void CreateRandomPopulation()
        {
            GetSimulationParameters();
            
            Shepards = new Population(SimParameters);

            SetControls();
        }
        
        public void CountFitness()
        {
            CorrentState = State.CountingFitness;

            BlockControls();


            var averageFitness = new AverageFitnessCounter().CountAverageFitness(SimParameters, Shepards, SimParameters.SeedForRandomSheepForBest);
            Logger.AddLine("Average fitness: " + averageFitness);


            new BestTeamManager().UpdateBestTeam(SimParameters, Shepards, Shepards.Units);

            labelAverage.Text = "Average Fitness: " + averageFitness.ToString();
            labelBestFitness.Text = "Best Fitness: " + Shepards.Best.Fitness.ToString();

            CorrentState = State.Idle;

            UnblockControls();
        }

        public void BlockControlsAfterPopulationCreation()
        {
            NumericUpDownNumberOfShepardsHiddenLayers.Enabled = false;
            NumericUpDownPopulationSize.Enabled = false;
        }

        private void BlockControls()
        {
            foreach (Form f in children)
                f.Dispose();

            panelOptimization.Enabled = false;
            panelPopulation.Enabled = false;
            panelRandomPositions.Enabled = false;
            panelSheep.Enabled = false;
            panelShepards.Enabled = false;

            ButtonLoad.Enabled = false;
            ButtonSave.Enabled = false;
            ButtonStart.Enabled = false;
            ButtonRandomPositions.Enabled = false;

            ButtonStop.Enabled = true;

            labelBestFitness.Text = "";
            labelAverage.Text = "";
        }

        private void UnblockControls()
        {
            foreach (Form f in children)
                f.Dispose();

            panelOptimization.Enabled = true;
            panelPopulation.Enabled = true;
            panelRandomPositions.Enabled = true;
            panelSheep.Enabled = true;
            panelShepards.Enabled = true;

            ButtonLoad.Enabled = true;
            ButtonSave.Enabled = true;
            ButtonStart.Enabled = true;
            ButtonRandomPositions.Enabled = true;

            ButtonStop.Enabled = false;
        }


        #region Events

        #region Buttons

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            Logger.Clear();

            if (SimParameters.NumberOfSheep < 2)
            {
                MessageBox.Show("Add at least two Sheep.");
                return;
            }
            
            NumericUpDownPopulationSize.Enabled = false;

            CorrentState = State.Optimization;

            GetSimulationParameters();

            if (CheckBoxRandomPositions.Checked &&
                (SimParameters.RandomSetsForBest.Count != (int)NumericUpDownNumberOfrandomSetsForBest.Value ||
                SimParameters.RandomSetsForBest.PositionsOfShepardsSet.First().Count() != SimParameters.NumberOfShepards ||
                SimParameters.RandomSetsForBest.PositionsOfSheepSet.First().Count() != SimParameters.NumberOfSheep))
            {
                SimParameters.RandomSetsForBest = new RandomSetsList(
                    (int)NumericUpDownNumberOfrandomSetsForBest.Value,
                    SimParameters.NumberOfShepards,
                    SimParameters.NumberOfSheep,
                    DateTime.Now.Millisecond);
            }

            if (Shepards == null || Shepards.Units == null || Shepards.Units.Count == 0)
            {
                MessageBox.Show("Random Population created.");
                CreateRandomPopulation();

                SimParameters.SeedForRandomSheepForBest = DateTime.Now.Millisecond;
            }
            else
            {
                Shepards.AdjustInputLayerSize(SimParameters.NumberOfSeenShepards, SimParameters.NumberOfSeenSheep);
                //Shepards.AdjustHiddenLayersSize(SimulationParameters.NumberOfNeuronsInHiddenLayer);
            }

            foreach (Form f in children)
            {
                f.Close();
                f.Dispose();
            }

            Shepards.AdjustTeamSize(SimParameters.NumberOfShepards);

            timer.Start();

            optimizationThread = new Thread(new ThreadStart(OptimizationInstance.Optimization.Optimize));
            optimizationThread.Start();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (Shepards == null || Shepards.Units == null)
            {
                MessageBox.Show("Nothing to save yet.");
                return;
            }
            
            Enabled = false;
            new FSave(this).Show();
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            foreach (Form f in children)
                f.Dispose();

            Enabled = false;
            new FLoad(this).Show();
        }

        private void ButtonShowHerding_Click(object sender, EventArgs e)
        {
            if (ComboBoxPopulation.SelectedIndex == -1)
                return;

            GetSimulationParameters();
            Shepards.AdjustTeamSize(SimParameters.NumberOfShepards);

            children.Add(new FWorld(SimParameters, Shepards.Units[ComboBoxPopulation.SelectedIndex]));
            children.Last().Show();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            lock (OptimizationInstance.Optimization.StopLocker)
            {
                OptimizationInstance.Optimization.Stop = true;
            }

            optimizationThread.Join();

            SimParameters.Progress = 1;

            CorrentState = State.Idle;

            Logger.Flush();
        }

        private void ButtonRandomPositions_Click(object sender, EventArgs e)
        {
            Random r = new Random();

            for (int i = 0; i < SimParameters.PositionsOfShepards.Count; i++)
            {
                SimParameters.PositionsOfShepards[i] = new Position(r.Next(400), r.Next(400));
            }

            for (int i = 0; i < SimParameters.PositionsOfSheep.Count; i++)
            {
                SimParameters.PositionsOfSheep[i] = new Position(r.Next(400), r.Next(400));
            }

            DataGridViewShepards.Refresh();
            DataGridViewSheep.Refresh();
        }

        private void ButtonCountFitness_Click(object sender, EventArgs e)
        {
            foreach (Team t in Shepards.Units)
            {
                t.ResetFitness();
            }

            CountFitness();
        }

        private void ButtonShowHerdingOfBestTeam_Click(object sender, EventArgs e)
        {
            GetSimulationParameters();

            Shepards.AdjustTeamSize(SimParameters.NumberOfShepards);

            children.Add(new FWorld(SimParameters, Shepards.Best));
            children.Last().Show();
        }

        #endregion

        private void CheckBoxRandomPositions_CheckedChanged(object sender, EventArgs e)
        {
            NumericUpDownNumberOfrandomSets.Enabled = CheckBoxRandomPositions.Checked;
            NumericUpDownNumberOfrandomSetsForBest.Enabled = CheckBoxRandomPositions.Checked;
        }
        
        private void DataGridViewShepards_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            NumericUpDownShepardShepardSight.Maximum = DataGridViewShepards.RowCount > 1 ? DataGridViewShepards.RowCount - 2 : 0;

            DataGridViewShepards.AllowUserToDeleteRows = DataGridViewShepards.RowCount > 2;
        }

        private void DataGridViewShepards_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            NumericUpDownShepardShepardSight.Maximum = DataGridViewShepards.RowCount > 1 ? DataGridViewShepards.RowCount - 2 : 0;

            DataGridViewShepards.AllowUserToDeleteRows = DataGridViewShepards.RowCount > 2;
        }

        private void DataGridViewSheep_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            NumericUpDownShepardSheepSight.Maximum = DataGridViewSheep.RowCount > 0 ? DataGridViewSheep.RowCount - 1 : 0;

            DataGridViewSheep.AllowUserToDeleteRows = DataGridViewSheep.RowCount > 3;
        }

        private void DataGridViewSheep_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            NumericUpDownShepardSheepSight.Maximum = DataGridViewSheep.RowCount > 0 ? DataGridViewSheep.RowCount - 1 : 0;

            DataGridViewSheep.AllowUserToDeleteRows = DataGridViewSheep.RowCount > 3;
        }

        private void ComboBoxPopulation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonShowHerding.Enabled = ComboBoxPopulation.SelectedIndex >= 0;
        }

        private void FSimulation_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.Close();
        }

        #endregion
    }
}
