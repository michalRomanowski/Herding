using System.Data.Entity;
using Simulations;
using Teams;
using Agent;

namespace EFDatabase
{
    public class EFDatabaseContext : DbContext
    {
        public virtual DbSet<Optimization> Optimizations { get; set; }
        public virtual DbSet<SimulationParameters> SimulationParameters { get; set; }
        public virtual DbSet<Population> Populations { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<ThinkingAgent> ThinkingAgents{ get; set; }
        public virtual DbSet<RandomSetsList> RandomSetsLists { get; set; }
        
        public EFDatabaseContext() : base("DefaultConnection") { }
    }
}
