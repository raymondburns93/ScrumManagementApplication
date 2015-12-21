namespace ScrumManagementApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductBacklogs",
                c => new
                    {
                        ProductBacklogId = c.Int(nullable: false, identity: true),
                        BacklogTitle = c.String(),
                        Locked = c.Boolean(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        ProductOwnerID = c.Int(nullable: false),
                        ProductOwner_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductBacklogId)
                .ForeignKey("dbo.Users", t => t.ProductOwner_UserId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.ProductOwner_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        Skillset = c.String(maxLength: 100),
                        Alias = c.String(maxLength: 20),
                        Firstname = c.String(maxLength: 20),
                        Lastname = c.String(maxLength: 20),
                        IsProductOwner = c.Boolean(nullable: false),
                        IsScrumMaster = c.Boolean(nullable: false),
                        IsDeveloper = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserProjects",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProjectId, t.RoleId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        ProjectDescription = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        ProductBacklogId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.ProductBacklogs", t => t.ProductBacklogId)
                .Index(t => t.ProductBacklogId);
            
            CreateTable(
                "dbo.Sprints",
                c => new
                    {
                        SprintID = c.Int(nullable: false, identity: true),
                        SprintName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        ProjectId = c.Int(nullable: false),
                        SprintBacklogId = c.Int(),
                    })
                .PrimaryKey(t => t.SprintID)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.SprintBacklogs", t => t.SprintBacklogId)
                .Index(t => t.ProjectId)
                .Index(t => t.SprintBacklogId);
            
            CreateTable(
                "dbo.SprintBacklogs",
                c => new
                    {
                        SprintBacklogId = c.Int(nullable: false, identity: true),
                        BacklogTitle = c.String(),
                        SprintID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SprintBacklogId)
                .ForeignKey("dbo.Sprints", t => t.SprintID, cascadeDelete: true)
                .Index(t => t.SprintID);
            
            CreateTable(
                "dbo.UserStories",
                c => new
                    {
                        UserStoryID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Locked = c.Boolean(nullable: false),
                        Priority = c.Int(),
                        ProductBacklogId = c.Int(nullable: false),
                        SprintBacklogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserStoryID)
                .ForeignKey("dbo.ProductBacklogs", t => t.ProductBacklogId, cascadeDelete: true)
                .ForeignKey("dbo.SprintBacklogs", t => t.SprintBacklogId, cascadeDelete: true)
                .Index(t => t.ProductBacklogId)
                .Index(t => t.SprintBacklogId);
            
            CreateTable(
                "dbo.UserSprintRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        SprintId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.SprintId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Sprints", t => t.SprintId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SprintId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductBacklogs", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProductBacklogs", "ProductOwner_UserId", "dbo.Users");
            DropForeignKey("dbo.UserProjects", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserProjects", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.UserSprintRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSprintRoles", "SprintId", "dbo.Sprints");
            DropForeignKey("dbo.UserSprintRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Sprints", "SprintBacklogId", "dbo.SprintBacklogs");
            DropForeignKey("dbo.UserStories", "SprintBacklogId", "dbo.SprintBacklogs");
            DropForeignKey("dbo.UserStories", "ProductBacklogId", "dbo.ProductBacklogs");
            DropForeignKey("dbo.SprintBacklogs", "SprintID", "dbo.Sprints");
            DropForeignKey("dbo.Sprints", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ProductBacklogId", "dbo.ProductBacklogs");
            DropIndex("dbo.UserSprintRoles", new[] { "RoleId" });
            DropIndex("dbo.UserSprintRoles", new[] { "SprintId" });
            DropIndex("dbo.UserSprintRoles", new[] { "UserId" });
            DropIndex("dbo.UserStories", new[] { "SprintBacklogId" });
            DropIndex("dbo.UserStories", new[] { "ProductBacklogId" });
            DropIndex("dbo.SprintBacklogs", new[] { "SprintID" });
            DropIndex("dbo.Sprints", new[] { "SprintBacklogId" });
            DropIndex("dbo.Sprints", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "ProductBacklogId" });
            DropIndex("dbo.UserProjects", new[] { "RoleId" });
            DropIndex("dbo.UserProjects", new[] { "ProjectId" });
            DropIndex("dbo.UserProjects", new[] { "UserId" });
            DropIndex("dbo.ProductBacklogs", new[] { "ProductOwner_UserId" });
            DropIndex("dbo.ProductBacklogs", new[] { "ProjectId" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserSprintRoles");
            DropTable("dbo.UserStories");
            DropTable("dbo.SprintBacklogs");
            DropTable("dbo.Sprints");
            DropTable("dbo.Projects");
            DropTable("dbo.UserProjects");
            DropTable("dbo.Users");
            DropTable("dbo.ProductBacklogs");
        }
    }
}
