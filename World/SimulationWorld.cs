using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using Teams;
using MathNet.Spatial.Euclidean;

namespace World
{
    public class SimulationWorld : IWorld
    {
        public Team Shepherds { get; private set; }
        public IList<IMovingAgent> Sheep { get; private set; }

        protected bool recordSheepPositionsFlag;
        public IList<IList<Vector2D>> SheepPositionsRecord { get; protected set; }
        
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
                SheepPositionsRecord = new List<IList<Vector2D>>();
            }
        }

        public void Work(int numberOfSteps)
        {
            for (int i = 0; i < numberOfSteps; i++)
            {
                Turn();

                if (recordSheepPositionsFlag)
                    SheepPositionsRecord.Add(Sheep.Select(s => new Vector2D(s.Position.X, s.Position.Y)).ToList());
            }
        }

        protected void Turn()
        {
            var centreOfSheep = Sheep.Select(x => x.Position).ToList().Center();

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
                ShepherdExtractor.InterpretOutput(s, this, s.decision);

                if(recordSheepPositionsFlag)
                    s.Path.Add(new Vector2D(s.Position.X, s.Position.Y));
            }

            foreach (var s in Sheep)
            {
                s.Move();

                if (recordSheepPositionsFlag)
                    s.Path.Add(new Vector2D(s.Position.X, s.Position.Y));
            }
        }
    }
}
