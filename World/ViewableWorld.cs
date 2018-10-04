using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Teams;

namespace World
{
    public class ViewableWorld : SimulationWorld, IViewableWorld
    {
        private const int MLISECONDS_BETWEEN_STEPS = 33;

        private SolidBrush brush;

        private readonly object pauseLocker = new object();
        private bool paused;

        private bool stopWork = false;

        private const string WORK_THREAD_NAME = "ViewableWorldWorkThread";
        private Thread workThread;
       

        public int Step { get; private set; }

        public ViewableWorld(
        Team shepards,
        IList<ISheep> sheep) : base(shepards, sheep)
        {
            foreach (var agent in Shepards.Members)
            {
                agent.Path.Clear();
            }
        }

        public void Start(int numberOfSteps)
        {
            foreach(var agent in Shepards.Members)
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
            foreach (var a in Shepards.Members)
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
            PathesSaver.SavePathes(path, Shepards.Members, Sheep);
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
                Thread.Sleep(MLISECONDS_BETWEEN_STEPS);

                if (stopWork == true)
                    return;
            }
        }

        public void Draw(Graphics gfx, int offsetX, int offsetY, bool showShepardsSight, bool showSheepRange, bool showCenterOfGravityOfSheep, bool showShepardsPath, bool showSheepPath)
        {
            if (brush == null)
            {
                brush = new SolidBrush(Color.Black);
            }

            gfx.FillRectangle(brush, offsetX, offsetY, 600, 600);

            offsetX += 100;
            offsetY += 100;

            if (showSheepRange)
            {
                foreach (var s in Sheep)
                {
                    s.DrawSight(gfx, offsetX, offsetY);
                }
            }

            if (showSheepPath)
            {
                foreach (var s in Sheep)
                {
                    s.DrawPath(gfx, offsetX, offsetY);
                }
            }

            if (showShepardsPath)
            {
                foreach (var s in Shepards.Members)
                {
                    s.DrawPath(gfx, offsetX, offsetY);
                }
            }

            if (showShepardsSight)
            {
                foreach (var s in Shepards.Members)
                {
                    s.DrawSight(gfx, offsetX, offsetY,
                        Finder.FindClosestAgents(Shepards.Members.Cast<IMovingAgent>().ToList(), s, s.NumberOfSeenShepards),
                        Color.LightBlue);

                    s.DrawSight(gfx, offsetX, offsetY,
                        Finder.FindClosestAgents(Sheep.Cast<IMovingAgent>().ToList(), s, s.NumberOfSeenSheep),
                        Color.LightBlue);
                }
            }

            foreach (var s in Sheep)
            {
                s.Draw(gfx, offsetX, offsetY);
            }

            foreach (var a in Shepards.Members)
            {
                a.Draw(gfx, offsetX, offsetY);
            }

            if (showCenterOfGravityOfSheep)
            {
                Position centre = Sheep.Select(x => x.Position).CentreOfGravity();
                gfx.FillEllipse(new SolidBrush(Color.White), new Rectangle(offsetX + (int)centre.X - 2, offsetY + (int)centre.Y - 2, 4, 4));
            }
        }
    }
}
