namespace BadProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Coord_Lon = c.Double(nullable: false),
                        Coord_Lat = c.Double(nullable: false),
                        Base = c.String(),
                        Main_Temp = c.Double(nullable: false),
                        Main_Pressure = c.Long(nullable: false),
                        Main_Humidity = c.Long(nullable: false),
                        Main_TempMin = c.Double(nullable: false),
                        Main_TempMax = c.Double(nullable: false),
                        Visibility = c.Long(nullable: false),
                        Wind_Speed = c.Double(nullable: false),
                        Wind_Deg = c.Long(nullable: false),
                        Clouds_All = c.Long(nullable: false),
                        Dt = c.Long(nullable: false),
                        Name = c.String(),
                        Cod = c.Long(nullable: false),
                        Sys_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sys", t => t.Sys_Id)
                .Index(t => t.Sys_Id);
            
            CreateTable(
                "dbo.Sys",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.Long(nullable: false),
                        Message = c.Double(nullable: false),
                        Country = c.String(),
                        Sunrise = c.Long(nullable: false),
                        Sunset = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WeatherElements",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Main = c.String(),
                        Description = c.String(),
                        Icon = c.String(),
                        Weather_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Weathers", t => t.Weather_Id)
                .Index(t => t.Weather_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeatherElements", "Weather_Id", "dbo.Weathers");
            DropForeignKey("dbo.Weathers", "Sys_Id", "dbo.Sys");
            DropIndex("dbo.WeatherElements", new[] { "Weather_Id" });
            DropIndex("dbo.Weathers", new[] { "Sys_Id" });
            DropTable("dbo.WeatherElements");
            DropTable("dbo.Sys");
            DropTable("dbo.Weathers");
        }
    }
}
