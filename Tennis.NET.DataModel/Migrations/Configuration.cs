namespace Tennis.NET.DataModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Tennis.NET.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Tennis.NET.DataModel.TennisContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tennis.NET.DataModel.TennisContext context)
        {
            
        }
    }
}
