using System;
using Simulations;

namespace EFDatabase
{
    public class Autosaver : IAutosaver
    {
        private const int AUTOSAVE_INTERVAL = 5000;

        public void Autosave(Optimization optimization, int step)
        {
            if (step > 0 && step % AUTOSAVE_INTERVAL != 0)
                return;

            var name = optimization.Name;

            optimization.Name = AutosaveName(step);
            new EFDatabaseManager().SaveOptimization(optimization);

            optimization.Name = name;
        }

        private string AutosaveName(int step)
        {
            return $"autosave_{DateTime.Now.ToString()}_step_{step}";
        }
    }
}
