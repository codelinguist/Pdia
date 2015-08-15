namespace Pdia.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pediatricianUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pediatricians", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pediatricians", "Deleted");
        }
    }
}
