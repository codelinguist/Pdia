namespace Pdia.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appClaim : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppClaims",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Expires = c.DateTime(nullable: false),
                        Token = c.String(),
                        Revoked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PediaId = c.Guid(nullable: false),
                        ChildId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Children", t => t.ChildId, cascadeDelete: true)
                .ForeignKey("dbo.Pediatricians", t => t.PediaId, cascadeDelete: true)
                .Index(t => t.PediaId)
                .Index(t => t.ChildId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "PediaId", "dbo.Pediatricians");
            DropForeignKey("dbo.Patients", "ChildId", "dbo.Children");
            DropIndex("dbo.Patients", new[] { "ChildId" });
            DropIndex("dbo.Patients", new[] { "PediaId" });
            DropTable("dbo.Patients");
            DropTable("dbo.AppClaims");
        }
    }
}
