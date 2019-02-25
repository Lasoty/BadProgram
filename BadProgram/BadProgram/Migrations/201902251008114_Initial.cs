namespace BadProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Lon = c.Double(nullable: false),
                        Lat = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mains",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Temp = c.Double(nullable: false),
                        Pressure = c.Long(nullable: false),
                        Humidity = c.Long(nullable: false),
                        TempMin = c.Double(nullable: false),
                        TempMax = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Winds",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Speed = c.Double(nullable: false),
                        Deg = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Weathers", "Coord_Id", c => c.Long());
            AddColumn("dbo.Weathers", "Main_Id", c => c.Long());
            AddColumn("dbo.Weathers", "Wind_Id", c => c.Long());
            CreateIndex("dbo.Weathers", "Coord_Id");
            CreateIndex("dbo.Weathers", "Main_Id");
            CreateIndex("dbo.Weathers", "Wind_Id");
            AddForeignKey("dbo.Weathers", "Coord_Id", "dbo.Coords", "Id");
            AddForeignKey("dbo.Weathers", "Main_Id", "dbo.Mains", "Id");
            AddForeignKey("dbo.Weathers", "Wind_Id", "dbo.Winds", "Id");
            DropColumn("dbo.Weathers", "Coord_Lon");
            DropColumn("dbo.Weathers", "Coord_Lat");
            DropColumn("dbo.Weathers", "Main_Temp");
            DropColumn("dbo.Weathers", "Main_Pressure");
            DropColumn("dbo.Weathers", "Main_Humidity");
            DropColumn("dbo.Weathers", "Main_TempMin");
            DropColumn("dbo.Weathers", "Main_TempMax");
            DropColumn("dbo.Weathers", "Wind_Speed");
            DropColumn("dbo.Weathers", "Wind_Deg");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Weathers", "Wind_Deg", c => c.Long(nullable: false));
            AddColumn("dbo.Weathers", "Wind_Speed", c => c.Double(nullable: false));
            AddColumn("dbo.Weathers", "Main_TempMax", c => c.Double(nullable: false));
            AddColumn("dbo.Weathers", "Main_TempMin", c => c.Double(nullable: false));
            AddColumn("dbo.Weathers", "Main_Humidity", c => c.Long(nullable: false));
            AddColumn("dbo.Weathers", "Main_Pressure", c => c.Long(nullable: false));
            AddColumn("dbo.Weathers", "Main_Temp", c => c.Double(nullable: false));
            AddColumn("dbo.Weathers", "Coord_Lat", c => c.Double(nullable: false));
            AddColumn("dbo.Weathers", "Coord_Lon", c => c.Double(nullable: false));
            DropForeignKey("dbo.Weathers", "Wind_Id", "dbo.Winds");
            DropForeignKey("dbo.Weathers", "Main_Id", "dbo.Mains");
            DropForeignKey("dbo.Weathers", "Coord_Id", "dbo.Coords");
            DropIndex("dbo.Weathers", new[] { "Wind_Id" });
            DropIndex("dbo.Weathers", new[] { "Main_Id" });
            DropIndex("dbo.Weathers", new[] { "Coord_Id" });
            DropColumn("dbo.Weathers", "Wind_Id");
            DropColumn("dbo.Weathers", "Main_Id");
            DropColumn("dbo.Weathers", "Coord_Id");
            DropTable("dbo.Winds");
            DropTable("dbo.Mains");
            DropTable("dbo.Coords");
        }
    }
}
