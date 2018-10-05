namespace EFDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Optimizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Parameters_Id = c.Int(),
                        Shepherds_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SimulationParameters", t => t.Parameters_Id)
                .ForeignKey("dbo.Populations", t => t.Shepherds_Id)
                .Index(t => t.Parameters_Id)
                .Index(t => t.Shepherds_Id);
            
            CreateTable(
                "dbo.SimulationParameters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OptimizationSteps = c.Int(nullable: false),
                        TurnsOfHerding = c.Int(nullable: false),
                        NumberOfParticipants = c.Int(nullable: false),
                        MutationPower = c.Single(nullable: false),
                        AbsoluteMutationFactor = c.Single(nullable: false),
                        SheepType = c.Int(nullable: false),
                        CompressedPositionsOfShepherds = c.String(),
                        CompressedPositionsOfSheep = c.String(),
                        NumberOfSeenShepherds = c.Int(nullable: false),
                        NumberOfSeenSheep = c.Int(nullable: false),
                        NumberOfHiddenLayers = c.Int(nullable: false),
                        NumberOfNeuronsInHiddenLayer = c.Int(nullable: false),
                        PopulationSize = c.Int(nullable: false),
                        RandomPositions = c.Boolean(nullable: false),
                        NumberOfRandomSets = c.Int(nullable: false),
                        SeedForRandomSheepForBest = c.Int(nullable: false),
                        FitnessType = c.Int(nullable: false),
                        RandomSetsForBest_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RandomSetsLists", t => t.RandomSetsForBest_Id)
                .Index(t => t.RandomSetsForBest_Id);
            
            CreateTable(
                "dbo.RandomSetsLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompressedPositionsOfShepherdsSet = c.String(),
                        CompressedPositionsOfSheepSet = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Populations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Best_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Best_Id)
                .Index(t => t.Best_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fitness = c.Single(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Population_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Populations", t => t.Population_Id)
                .Index(t => t.Population_Id);
            
            CreateTable(
                "dbo.ThinkingAgents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberOfSeenShepherds = c.Int(nullable: false),
                        NumberOfSeenSheep = c.Int(nullable: false),
                        CompressedNeuralNet = c.String(),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Optimizations", "Shepherds_Id", "dbo.Populations");
            DropForeignKey("dbo.Teams", "Population_Id", "dbo.Populations");
            DropForeignKey("dbo.Populations", "Best_Id", "dbo.Teams");
            DropForeignKey("dbo.ThinkingAgents", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Optimizations", "Parameters_Id", "dbo.SimulationParameters");
            DropForeignKey("dbo.SimulationParameters", "RandomSetsForBest_Id", "dbo.RandomSetsLists");
            DropIndex("dbo.ThinkingAgents", new[] { "Team_Id" });
            DropIndex("dbo.Teams", new[] { "Population_Id" });
            DropIndex("dbo.Populations", new[] { "Best_Id" });
            DropIndex("dbo.SimulationParameters", new[] { "RandomSetsForBest_Id" });
            DropIndex("dbo.Optimizations", new[] { "Shepherds_Id" });
            DropIndex("dbo.Optimizations", new[] { "Parameters_Id" });
            DropTable("dbo.ThinkingAgents");
            DropTable("dbo.Teams");
            DropTable("dbo.Populations");
            DropTable("dbo.RandomSetsLists");
            DropTable("dbo.SimulationParameters");
            DropTable("dbo.Optimizations");
        }
    }
}
