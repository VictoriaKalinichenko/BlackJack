namespace BlackJack.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GamePlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        Bet = c.Int(nullable: false),
                        RoundScore = c.Int(nullable: false),
                        BetPayCoefficient = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Stage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDealer = c.Boolean(nullable: false),
                        IsHuman = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlayerCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GamePlayerId = c.Int(nullable: false),
                        CardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GamePlayers", t => t.GamePlayerId, cascadeDelete: true)
                .Index(t => t.GamePlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerCards", "GamePlayerId", "dbo.GamePlayers");
            DropForeignKey("dbo.GamePlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.GamePlayers", "GameId", "dbo.Games");
            DropIndex("dbo.PlayerCards", new[] { "GamePlayerId" });
            DropIndex("dbo.GamePlayers", new[] { "GameId" });
            DropIndex("dbo.GamePlayers", new[] { "PlayerId" });
            DropTable("dbo.PlayerCards");
            DropTable("dbo.Players");
            DropTable("dbo.Games");
            DropTable("dbo.GamePlayers");
        }
    }
}
