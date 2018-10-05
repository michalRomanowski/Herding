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
                    .Include(x => x.Shepherds)
                    .Include(x => x.Shepherds.Best)
                    .Include(x => x.Shepherds.Best.Members)
                    .Include(x => x.Shepherds.Units)
                    .Include(x => x.Shepherds.Units.Select(y => y.Members))
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
