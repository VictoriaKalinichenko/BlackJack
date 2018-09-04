namespace BlackJack.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameResult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Result", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Result");
        }
    }
}
