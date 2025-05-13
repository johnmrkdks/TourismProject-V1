namespace TourismProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAvailableDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TourPackages", "AvailableDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TourPackages", "AvailableDate", c => c.DateTime(nullable: false));
        }
    }
}
