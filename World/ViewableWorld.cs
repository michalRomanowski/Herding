using Agent;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Teams;

namespace World
{
    public class ViewableWorld : SimulationWorld, IViewableWorld
    {
        private const int MILISECONDS_BETWEEN_STEPS = 33;

        private readonly object pauseLocker = new object();
        private bool paused;

        private bool stopWork = false;

        private const string WORK_THREAD_NAME = "ViewableWorldWorkThread";
        private Thread workThread;
       
        public int Step { get; private set; }

        private readonly int numberOfSeenSheep;
        private readonly int numberOfSeenShepherds;

        private Drawing drawing;

        public ViewableWorld(
            Team shepherds,
            IList<IMovingAgent> sheep,
            int numberOfSeenSheep,
            int numberOfSeenShepherds) : base(shepherds, sheep, true)
        {
            foreach (var agent in Shepherds.Members)
            {
                agent.Path.Clear();
            }

            this.numberOfSeenSheep = numberOfSeenSheep;
            this.numberOfSeenShepherds = numberOfSeenShepherds;

            this.drawing = new Drawing(10, 10, 600, 600, Sheep, Shepherds.Members, numberOfSeenSheep, numberOfSeenShepherds);
        }

        public void Start(int numberOfSteps)
        {
            foreach(var agent in Shepherds.Members)
            {
                agent.Path.Clear();
            }

            workThread = new Thread(this.Work);
            workThread.Name = WORK_THREAD_NAME;
            workThread.Start(numberOfSteps);
        }

        public void Stop()
        {
            if (workThread != null && this.workThread.IsAlive)
            {
                stopWork = true;
                workThread.Join();
            }
        }

        public void Pause()
        {
            lock (pauseLocker)
            {
                paused = true;
            }
        }

        public void Resume()
        {
            lock (pauseLocker)
            {
                paused = false;
            }
        }

        public void StepForward()
        {
            Turn();
        }

        public void StepBack()
        {
            foreach (var a in Shepherds.Members)
            {
                a.StepBack();
            }

            foreach (var s in Sheep)
            {
                s.StepBack();
            }
        }

        public void SavePositions(string path)
        {
            PathesSaver.SavePathes(path, Shepherds.Members, Sheep);
        }

        public void Work(object numberOfSteps)
        {
            int numberOfStepsInt = Convert.ToInt32(numberOfSteps);

            Step = 0;

            while (Step < numberOfStepsInt)
            {
                if (!paused)
                {
                    Step++;
                    Turn();
                }
                Thread.Sleep(MILISECONDS_BETWEEN_STEPS);

                if (stopWork == true)
                    return;
            }
        }

        public void Draw(Graphics gfx, int offsetX, int offsetY, DrawingFlags flags)
        {
            drawing.Draw(gfx, flags);
        }
    }
}
