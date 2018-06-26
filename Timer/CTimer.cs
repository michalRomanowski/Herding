using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Timer
{
    public class CTimer : System.Windows.Forms.Timer
    {
        private int time;
        
        private bool instant;
        
        public Dictionary<int, List<VoidDelegate>> events;
        
        public CTimer(int interval)
        {
            this.events = new Dictionary<int, List<VoidDelegate>>();

            this.Tick += new System.EventHandler(onTick);

            this.time = 0;

            if (interval == 0)
            {
                this.instant = true;
            }
            else this.Interval = interval;
        }

        public void Add(int interval, VoidDelegate del)
        {
            if (!this.events.Keys.Contains(interval))
                this.events.Add(interval, new List<VoidDelegate>());

            List<VoidDelegate> vdl;

            this.events.TryGetValue(interval, out vdl);

            vdl.Add(del);
        }

        new public void Start()
        {
            if (this.instant != true)
                this.Enabled = true;
            else new Thread(new ThreadStart(this.instantTicking)).Start();
        }
        
        private void onTick(object source, EventArgs e)
        {
            (source as CTimer).Tack();
        }

        private void instantTicking()
        {
            while (true)
            {
                if (this.Enabled == true)
                    this.Tack();
            }
        }

        private void Tack()
        {
            this.time += 1;

            //Na wszelki wypadek.
            if (this.time == int.MaxValue) this.time = 0;

            //Sprawdzamy i wywołujemy odpowiednie delegaty.
            foreach (KeyValuePair<int, List<VoidDelegate>> kv in this.events)
            {
                if (this.time % kv.Key == 0)
                {
                    foreach (VoidDelegate vd in kv.Value)
                        vd();
                }
            }

        }
    }

    public delegate void VoidDelegate();
}
