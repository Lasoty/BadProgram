namespace BadProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateToWeather : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Weathers", "MeasurmentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Weathers", "MeasurmentDate");
        }
    }
}
