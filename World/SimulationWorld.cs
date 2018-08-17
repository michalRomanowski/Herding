using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using Team;
using World;

namespace World
{
    public class SimulationWorld : IWorld
    {
        public ITeam Shepards { get; private set; }
        public IList<ISheep> Sheep { get; private set; }

        protected bool recordSheepPositionsFlag;
        public IList<IList<Position>> SheepPositionsRecord { get; protected set; }
        
        public SimulationWorld(
            ITeam shepards,
            IList<ISheep> sheep,
            bool recordSheepPositions = false)
        {
            this.Shepards = shepards;
            this.Sheep = sheep;

            this.recordSheepPositionsFlag = recordSheepPositions;

            if (recordSheepPositions == true)
            {
                SheepPositionsRecord = new List<IList<Position>>();
            }
        }

        public void Work(int numberOfSteps)
        {
            for (int i = 0; i < numberOfSteps; i++)
            {
                Turn();

                if (recordSheepPositionsFlag)
                    SheepPositionsRecord.Add(Sheep.Select(s => new Position(s.Position)).ToList());
            }
        }

        protected void Turn()
        {
            foreach (var s in Shepards.Members)
            {
                var input = ShepardExtractor.ExtractFeatures(this, s);

                s.Decide(input);
            }

            foreach (var s in Sheep)
            {
                var input = SheepExtractor.ExtractFeatures(this, s);

                s.Decide(input);
            }

            foreach (var s in Shepards.Members)
            {
                ShepardExtractor.InterpretOutput(s, this, s.DecideOutput);

                s.Path.Add(new Position(s.Position));
            }

            foreach (var s in Sheep)
            {
                SheepExtractor.InterpretOutput(s, this, s.DecideOutput);

                s.Path.Add(new Position(s.Position));
            }
        }
    }
}
