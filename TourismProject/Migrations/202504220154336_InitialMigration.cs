namespace TourismProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agencies",
                c => new
                    {
                        AgencyId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        AgencyName = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 300),
                        ServicesOffered = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.AgencyId);
            
            CreateTable(
                "dbo.TourPackages",
                c => new
                    {
                        TourPackageId = c.Int(nullable: false, identity: true),
                        AgencyId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(),
                        DurationInDays = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxGroupSize = c.Int(nullable: false),
                        AvailableDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.TourPackageId)
                .ForeignKey("dbo.Agencies", t => t.AgencyId, cascadeDelete: true)
                .Index(t => t.AgencyId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        TouristId = c.Int(nullable: false),
                        TourPackageId = c.Int(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        Status = c.String(nullable: false),
                        PaymentCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Tourists", t => t.TouristId, cascadeDelete: true)
                .ForeignKey("dbo.TourPackages", t => t.TourPackageId, cascadeDelete: true)
                .Index(t => t.TouristId)
                .Index(t => t.TourPackageId);
            
            CreateTable(
                "dbo.Tourists",
                c => new
                    {
                        TouristId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.TouristId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        TourPackageId = c.Int(nullable: false),
                        TouristId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(),
                        SubmittedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackId)
                .ForeignKey("dbo.Tourists", t => t.TouristId, cascadeDelete: true)
                .ForeignKey("dbo.TourPackages", t => t.TourPackageId, cascadeDelete: true)
                .Index(t => t.TourPackageId)
                .Index(t => t.TouristId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "TourPackageId", "dbo.TourPackages");
            DropForeignKey("dbo.Feedbacks", "TouristId", "dbo.Tourists");
            DropForeignKey("dbo.Bookings", "TourPackageId", "dbo.TourPackages");
            DropForeignKey("dbo.Bookings", "TouristId", "dbo.Tourists");
            DropForeignKey("dbo.TourPackages", "AgencyId", "dbo.Agencies");
            DropIndex("dbo.Feedbacks", new[] { "TouristId" });
            DropIndex("dbo.Feedbacks", new[] { "TourPackageId" });
            DropIndex("dbo.Bookings", new[] { "TourPackageId" });
            DropIndex("dbo.Bookings", new[] { "TouristId" });
            DropIndex("dbo.TourPackages", new[] { "AgencyId" });
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Tourists");
            DropTable("dbo.Bookings");
            DropTable("dbo.TourPackages");
            DropTable("dbo.Agencies");
        }
    }
}
