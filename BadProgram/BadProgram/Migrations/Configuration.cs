namespace BadProgram.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Baza>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BadProgram.Baza";
        }

        protected override void Seed(Baza context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
