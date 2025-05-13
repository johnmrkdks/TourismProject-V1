namespace TourismProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertTourDateChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourPackages", "AvailableDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourPackages", "AvailableDate");
        }
    }
}
