namespace Tennis.NET.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FineAmounttoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fines", "Amount", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fines", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
