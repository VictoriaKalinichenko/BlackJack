namespace BlackJack.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameAndDeckAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Name = c.String(),
                        Player_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player_Id)
                .Index(t => t.Player_Id);
            
            CreateTable(
                "dbo.Decks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Deck_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Decks", t => t.Deck_Id)
                .Index(t => t.Deck_Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Score = c.Int(nullable: false),
                        IsDealer = c.Boolean(nullable: false),
                        IsHuman = c.Boolean(nullable: false),
                        Bet = c.Int(nullable: false),
                        RoundScore = c.Int(nullable: false),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.CardDecks",
                c => new
                    {
                        Card_Id = c.Int(nullable: false),
                        Deck_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Card_Id, t.Deck_Id })
                .ForeignKey("dbo.Cards", t => t.Card_Id, cascadeDelete: true)
                .ForeignKey("dbo.Decks", t => t.Deck_Id, cascadeDelete: true)
                .Index(t => t.Card_Id)
                .Index(t => t.Deck_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Cards", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.Games", "Deck_Id", "dbo.Decks");
            DropForeignKey("dbo.CardDecks", "Deck_Id", "dbo.Decks");
            DropForeignKey("dbo.CardDecks", "Card_Id", "dbo.Cards");
            DropIndex("dbo.CardDecks", new[] { "Deck_Id" });
            DropIndex("dbo.CardDecks", new[] { "Card_Id" });
            DropIndex("dbo.Players", new[] { "Game_Id" });
            DropIndex("dbo.Games", new[] { "Deck_Id" });
            DropIndex("dbo.Cards", new[] { "Player_Id" });
            DropTable("dbo.CardDecks");
            DropTable("dbo.Players");
            DropTable("dbo.Games");
            DropTable("dbo.Decks");
            DropTable("dbo.Cards");
        }
    }
}
