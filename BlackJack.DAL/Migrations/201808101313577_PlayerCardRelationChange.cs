namespace BlackJack.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerCardRelationChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "Player_Id", "dbo.Players");
            DropIndex("dbo.Cards", new[] { "Player_Id" });
            CreateTable(
                "dbo.PlayerCards",
                c => new
                    {
                        Player_Id = c.Int(nullable: false),
                        Card_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_Id, t.Card_Id })
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cards", t => t.Card_Id, cascadeDelete: true)
                .Index(t => t.Player_Id)
                .Index(t => t.Card_Id);
            
            DropColumn("dbo.Cards", "Player_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "Player_Id", c => c.Int());
            DropForeignKey("dbo.PlayerCards", "Card_Id", "dbo.Cards");
            DropForeignKey("dbo.PlayerCards", "Player_Id", "dbo.Players");
            DropIndex("dbo.PlayerCards", new[] { "Card_Id" });
            DropIndex("dbo.PlayerCards", new[] { "Player_Id" });
            DropTable("dbo.PlayerCards");
            CreateIndex("dbo.Cards", "Player_Id");
            AddForeignKey("dbo.Cards", "Player_Id", "dbo.Players", "Id");
        }
    }
}
