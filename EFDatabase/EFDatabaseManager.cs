using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Simulations;

namespace EFDatabase
{
    public class EFDatabaseManager
    {
        public IEnumerable<string> LoadOptimizationsNames()
        {
            using (var context = new EFDatabaseContext())
            {
                return context.Optimizations.Select(x => x.Name).ToList();
            }
        }

        public Optimization LoadOptimization(string name)
        {
            using (var context = new EFDatabaseContext())
            {
                var optimization = context.Optimizations
                    .Where(x => name.Equals(x.Name))
                    .Include(x => x.Parameters)
                    .Include(x => x.Parameters.RandomSetsForBest)
                    .Include(x => x.Shepards)
                    .Include(x => x.Shepards.Best)
                    .Include(x => x.Shepards.Best.Members)
                    .Include(x => x.Shepards.Units)
                    .Include(x => x.Shepards.Units.Select(y => y.Members))
                    .First();

                return optimization;
            }
        }

        public void SaveOptimization(Optimization optimization)
        {
            using (var context = new EFDatabaseContext())
            {
                context.Optimizations.Add(optimization);
                context.SaveChanges();
            }
        }
    }
}
