namespace TourismProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToAgency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agencies", "Email", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agencies", "Email");
        }
    }
}
