namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "TotalMembers", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "TotalMembers", c => c.Int(nullable: false));
        }
    }
}
