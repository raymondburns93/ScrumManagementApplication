namespace ScrumManagementApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(),
                        TaskDescription = c.String(),
                        HoursRemaining = c.Time(nullable: false, precision: 7),
                        DeveloperOwnedById = c.Int(),
                        UserStoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Users", t => t.DeveloperOwnedById)
                .ForeignKey("dbo.UserStories", t => t.UserStoryID, cascadeDelete: true)
                .Index(t => t.DeveloperOwnedById)
                .Index(t => t.UserStoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "UserStoryID", "dbo.UserStories");
            DropForeignKey("dbo.Tasks", "DeveloperOwnedById", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "UserStoryID" });
            DropIndex("dbo.Tasks", new[] { "DeveloperOwnedById" });
            DropTable("dbo.Tasks");
        }
    }
}
