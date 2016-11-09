namespace Tennis.NET.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nullableintsdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "BirthDay", c => c.DateTime());
            AlterColumn("dbo.Players", "YearOfJoin", c => c.Int());
            AlterColumn("dbo.Players", "HouseNr", c => c.Int());
            AlterColumn("dbo.Players", "FederationNr", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "FederationNr", c => c.Int(nullable: false));
            AlterColumn("dbo.Players", "HouseNr", c => c.Int(nullable: false));
            AlterColumn("dbo.Players", "YearOfJoin", c => c.Int(nullable: false));
            AlterColumn("dbo.Players", "BirthDay", c => c.DateTime(nullable: false));
        }
    }
}
