namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecongMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsFree", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "TotalTickets", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "TotalTickets");
            DropColumn("dbo.Users", "IsFree");
        }
    }
}
