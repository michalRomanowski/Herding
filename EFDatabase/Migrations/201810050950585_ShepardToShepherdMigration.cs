namespace EFDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShepardToShepherdMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Optimizations", name: "Shepards_Id", newName: "Shepherds_Id");
            RenameIndex(table: "dbo.Optimizations", name: "IX_Shepards_Id", newName: "IX_Shepherds_Id");
            AddColumn("dbo.SimulationParameters", "CompressedPositionsOfShepherds", c => c.String());
            AddColumn("dbo.SimulationParameters", "NumberOfSeenShepherds", c => c.Int(nullable: false));
            AddColumn("dbo.RandomSetsLists", "CompressedPositionsOfShepherdsSet", c => c.String());
            AddColumn("dbo.ThinkingAgents", "NumberOfSeenShepherds", c => c.Int(nullable: false));
            DropColumn("dbo.SimulationParameters", "CompressedPositionsOfShepards");
            DropColumn("dbo.SimulationParameters", "NumberOfSeenShepards");
            DropColumn("dbo.RandomSetsLists", "CompressedPositionsOfShepardsSet");
            DropColumn("dbo.ThinkingAgents", "NumberOfSeenShepards");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ThinkingAgents", "NumberOfSeenShepards", c => c.Int(nullable: false));
            AddColumn("dbo.RandomSetsLists", "CompressedPositionsOfShepardsSet", c => c.String());
            AddColumn("dbo.SimulationParameters", "NumberOfSeenShepards", c => c.Int(nullable: false));
            AddColumn("dbo.SimulationParameters", "CompressedPositionsOfShepards", c => c.String());
            DropColumn("dbo.ThinkingAgents", "NumberOfSeenShepherds");
            DropColumn("dbo.RandomSetsLists", "CompressedPositionsOfShepherdsSet");
            DropColumn("dbo.SimulationParameters", "NumberOfSeenShepherds");
            DropColumn("dbo.SimulationParameters", "CompressedPositionsOfShepherds");
            RenameIndex(table: "dbo.Optimizations", name: "IX_Shepherds_Id", newName: "IX_Shepards_Id");
            RenameColumn(table: "dbo.Optimizations", name: "Shepherds_Id", newName: "Shepards_Id");
        }
    }
}
