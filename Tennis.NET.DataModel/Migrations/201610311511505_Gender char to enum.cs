namespace Tennis.NET.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Genderchartoenum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Gender", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Gender");
        }
    }
}
