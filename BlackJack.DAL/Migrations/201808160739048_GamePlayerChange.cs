namespace BlackJack.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GamePlayerChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CardDecks", "Card_Id", "dbo.Cards");
            DropForeignKey("dbo.CardDecks", "Deck_Id", "dbo.Decks");
            DropForeignKey("dbo.PlayerCards", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.PlayerCards", "Card_Id", "dbo.Cards");
            DropForeignKey("dbo.Games", "Deck_Id", "dbo.Decks");
            DropForeignKey("dbo.Players", "Game_Id", "dbo.Games");
            DropIndex("dbo.Players", new[] { "Game_Id" });
            DropIndex("dbo.Games", new[] { "Deck_Id" });
            DropIndex("dbo.CardDecks", new[] { "Card_Id" });
            DropIndex("dbo.CardDecks", new[] { "Deck_Id" });
            DropIndex("dbo.PlayerCards", new[] { "Player_Id" });
            DropIndex("dbo.PlayerCards", new[] { "Card_Id" });
            CreateTable(
                "dbo.GamePlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Players", "Game_Id");
            DropColumn("dbo.Games", "Deck_Id");
            DropTable("dbo.Cards");
            DropTable("dbo.Decks");
            DropTable("dbo.CardDecks");
            DropTable("dbo.PlayerCards");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlayerCards",
                c => new
                    {
                        Player_Id = c.Int(nullable: false),
                        Card_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_Id, t.Card_Id });
            
            CreateTable(
                "dbo.CardDecks",
                c => new
                    {
                        Card_Id = c.Int(nullable: false),
                        Deck_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Card_Id, t.Deck_Id });
            
            CreateTable(
                "dbo.Decks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Games", "Deck_Id", c => c.Int());
            AddColumn("dbo.Players", "Game_Id", c => c.Int());
            DropTable("dbo.GamePlayers");
            CreateIndex("dbo.PlayerCards", "Card_Id");
            CreateIndex("dbo.PlayerCards", "Player_Id");
            CreateIndex("dbo.CardDecks", "Deck_Id");
            CreateIndex("dbo.CardDecks", "Card_Id");
            CreateIndex("dbo.Games", "Deck_Id");
            CreateIndex("dbo.Players", "Game_Id");
            AddForeignKey("dbo.Players", "Game_Id", "dbo.Games", "Id");
            AddForeignKey("dbo.Games", "Deck_Id", "dbo.Decks", "Id");
            AddForeignKey("dbo.PlayerCards", "Card_Id", "dbo.Cards", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlayerCards", "Player_Id", "dbo.Players", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CardDecks", "Deck_Id", "dbo.Decks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CardDecks", "Card_Id", "dbo.Cards", "Id", cascadeDelete: true);
        }
    }
}
