namespace BlackJack.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GamePlayerCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlayerCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GamePlayerId = c.Int(nullable: false),
                        CardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.GamePlayers", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.GamePlayers", "Bet", c => c.Int(nullable: false));
            AddColumn("dbo.GamePlayers", "RoundScore", c => c.Int(nullable: false));
            DropColumn("dbo.Players", "Score");
            DropColumn("dbo.Players", "Bet");
            DropColumn("dbo.Players", "RoundScore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "RoundScore", c => c.Int(nullable: false));
            AddColumn("dbo.Players", "Bet", c => c.Int(nullable: false));
            AddColumn("dbo.Players", "Score", c => c.Int(nullable: false));
            DropColumn("dbo.GamePlayers", "RoundScore");
            DropColumn("dbo.GamePlayers", "Bet");
            DropColumn("dbo.GamePlayers", "Score");
            DropTable("dbo.PlayerCards");
        }
    }
}
