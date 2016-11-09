namespace Tennis.NET.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FineAmountmaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fines", "Amount", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fines", "Amount", c => c.String(nullable: false));
        }
    }
}
