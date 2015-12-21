namespace ScrumManagementApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityCleanup1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductBacklogs", "Locked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductBacklogs", "Locked", c => c.Boolean(nullable: false));
        }
    }
}
