namespace BlackJack.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VirtualsAreRemovedAndLogsAreAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GamePlayers", "GameId", "dbo.Games");
            DropForeignKey("dbo.PlayerCards", "GamePlayerId", "dbo.GamePlayers");
            DropIndex("dbo.GamePlayers", new[] { "GameId" });
            DropIndex("dbo.PlayerCards", new[] { "GamePlayerId" });
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        GameId = c.Int(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
            CreateIndex("dbo.PlayerCards", "GamePlayerId");
            CreateIndex("dbo.GamePlayers", "GameId");
            AddForeignKey("dbo.PlayerCards", "GamePlayerId", "dbo.GamePlayers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GamePlayers", "GameId", "dbo.Games", "Id", cascadeDelete: true);
        }
    }
}
