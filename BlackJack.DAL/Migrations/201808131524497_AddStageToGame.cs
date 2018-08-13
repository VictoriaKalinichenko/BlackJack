namespace BlackJack.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStageToGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Stage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Stage");
        }
    }
}
