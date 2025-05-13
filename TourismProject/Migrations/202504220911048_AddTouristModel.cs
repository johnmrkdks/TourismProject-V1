namespace TourismProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTouristModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tourists", "DateOfBirth", c => c.DateTime());
            AddColumn("dbo.Tourists", "PhoneNumber", c => c.String(maxLength: 20));
            AddColumn("dbo.Tourists", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tourists", "LastUpdated", c => c.DateTime());
            AlterColumn("dbo.Tourists", "UserId", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.Tourists", "Email", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tourists", "Email", c => c.String());
            AlterColumn("dbo.Tourists", "UserId", c => c.String(nullable: false));
            DropColumn("dbo.Tourists", "LastUpdated");
            DropColumn("dbo.Tourists", "CreatedDate");
            DropColumn("dbo.Tourists", "PhoneNumber");
            DropColumn("dbo.Tourists", "DateOfBirth");
        }
    }
}
