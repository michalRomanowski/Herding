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
        private readonly int milisecondsBetweenSteps;
    
        private readonly object pauseLocker = new object();
        private bool paused;

        private bool stopWork = false;

        private const string WORK_THREAD_NAME = "ViewableWorldWorkThread";
        private Thread workThread;
       
        public int StepCount { get; private set; }
        
        private Drawing drawing;

        public ViewableWorld(
            int offsetX,
            int offsetY,
            int milisecondsBetweenSteps,
            Team shepherds,
            IList<IMovingAgent> sheep) : base(shepherds, sheep, true)
        {
            foreach (var agent in Shepherds.Members)
            {
                agent.Path.Clear();
            }
            
            this.milisecondsBetweenSteps = milisecondsBetweenSteps;

            this.drawing = new Drawing(offsetX, offsetY, 600, 600, Sheep, Shepherds.Members);
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
            Step();
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

            StepCount = 0;

            while (StepCount < numberOfStepsInt)
            {
                if (!paused)
                {
                    StepCount++;
                    Step();
                }

                if(milisecondsBetweenSteps > 0)
                    Thread.Sleep(milisecondsBetweenSteps);

                if (stopWork == true)
                    return;
            }
        }

        public void Draw(Graphics gfx, DrawingFlags flags)
        {
            drawing.Draw(gfx, flags);
        }
    }
}
