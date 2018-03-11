namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "TotalTickets", c => c.Int());
            DropColumn("dbo.Events", "TotalTicketAllowed");
            DropColumn("dbo.Users", "IsFree");
            DropColumn("dbo.Users", "TotalTickets");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "TotalTickets", c => c.Int());
            AddColumn("dbo.Users", "IsFree", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "TotalTicketAllowed", c => c.Int(nullable: false));
            DropColumn("dbo.Events", "TotalTickets");
        }
    }
}
