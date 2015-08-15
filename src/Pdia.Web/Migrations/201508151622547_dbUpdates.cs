namespace Pdia.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbUpdates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BabyBooks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChildId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Children", t => t.ChildId, cascadeDelete: true)
                .Index(t => t.ChildId);
            
            CreateTable(
                "dbo.Clinics",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        ZipCode = c.String(),
                        PhoneNumber = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Topic = c.String(),
                        File = c.String(),
                        Created = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        PrivacySetting = c.Int(nullable: false),
                        Parent_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            AddColumn("dbo.Children", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserProfiles", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Parent_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.BabyBooks", "ChildId", "dbo.Children");
            DropIndex("dbo.Posts", new[] { "Parent_Id" });
            DropIndex("dbo.BabyBooks", new[] { "ChildId" });
            DropColumn("dbo.UserProfiles", "Deleted");
            DropColumn("dbo.Children", "Deleted");
            DropTable("dbo.Posts");
            DropTable("dbo.Clinics");
            DropTable("dbo.BabyBooks");
        }
    }
}
