namespace Pdia.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userProfileAndPediatrician : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parentings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pediatricians",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserProfileId = c.Guid(nullable: false),
                        LicenseNo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.UserProfileParentings",
                c => new
                    {
                        UserProfile_Id = c.Guid(nullable: false),
                        Parenting_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserProfile_Id, t.Parenting_Id })
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parentings", t => t.Parenting_Id, cascadeDelete: true)
                .Index(t => t.UserProfile_Id)
                .Index(t => t.Parenting_Id);
            
            AddColumn("dbo.Children", "ParentingId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Children", "ParentingId");
            AddForeignKey("dbo.Children", "ParentingId", "dbo.Parentings", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pediatricians", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserProfileParentings", "Parenting_Id", "dbo.Parentings");
            DropForeignKey("dbo.UserProfileParentings", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.Children", "ParentingId", "dbo.Parentings");
            DropIndex("dbo.UserProfileParentings", new[] { "Parenting_Id" });
            DropIndex("dbo.UserProfileParentings", new[] { "UserProfile_Id" });
            DropIndex("dbo.Pediatricians", new[] { "UserProfileId" });
            DropIndex("dbo.Children", new[] { "ParentingId" });
            DropColumn("dbo.Children", "ParentingId");
            DropTable("dbo.UserProfileParentings");
            DropTable("dbo.Pediatricians");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Parentings");
        }
    }
}
