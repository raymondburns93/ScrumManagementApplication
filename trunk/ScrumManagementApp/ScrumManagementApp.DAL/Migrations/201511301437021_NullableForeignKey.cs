namespace ScrumManagementApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserStories", "SprintBacklogId", "dbo.SprintBacklogs");
            DropIndex("dbo.UserStories", new[] { "SprintBacklogId" });
            AlterColumn("dbo.UserStories", "SprintBacklogId", c => c.Int());
            CreateIndex("dbo.UserStories", "SprintBacklogId");
            AddForeignKey("dbo.UserStories", "SprintBacklogId", "dbo.SprintBacklogs", "SprintBacklogId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserStories", "SprintBacklogId", "dbo.SprintBacklogs");
            DropIndex("dbo.UserStories", new[] { "SprintBacklogId" });
            AlterColumn("dbo.UserStories", "SprintBacklogId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserStories", "SprintBacklogId");
            AddForeignKey("dbo.UserStories", "SprintBacklogId", "dbo.SprintBacklogs", "SprintBacklogId", cascadeDelete: true);
        }
    }
}
