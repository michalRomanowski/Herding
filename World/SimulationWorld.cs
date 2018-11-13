using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using Teams;

namespace World
{
    public class SimulationWorld : IWorld
    {
        public Team Shepherds { get; private set; }
        public IList<IMovingAgent> Sheep { get; private set; }

        protected bool recordSheepPositionsFlag;
        public IList<IList<Position>> SheepPositionsRecord { get; protected set; }
        
        public SimulationWorld(
            Team shepherds,
            IList<IMovingAgent> sheep,
            bool recordSheepPositions)
        {
            this.Shepherds = shepherds;
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
            foreach (var s in Shepherds.Members)
            {
                var input = ShepherdExtractor.ExtractFeatures(this, s);

                s.Decide(input);
            }

            foreach (var s in Sheep)
            {
                var input = SheepExtractor.ExtractFeatures(this, s);

                s.Decide(input);
            }

            foreach (var s in Shepherds.Members)
            {
                ShepherdExtractor.InterpretOutput(s, this, s.DecideOutput);

                if(recordSheepPositionsFlag)
                    s.Path.Add(new Position(s.Position));
            }

            foreach (var s in Sheep)
            {
                SheepExtractor.InterpretOutput(s, this, s.DecideOutput);

                if (recordSheepPositionsFlag)
                    s.Path.Add(new Position(s.Position));
            }
        }
    }
}
