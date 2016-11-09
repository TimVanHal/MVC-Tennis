namespace Tennis.NET.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        FineDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerNr = c.Int(nullable: false),
                        LastName = c.String(maxLength: 50),
                        FirstName = c.String(maxLength: 50),
                        BirthDay = c.DateTime(nullable: false),
                        YearOfJoin = c.Int(nullable: false),
                        Street = c.String(maxLength: 50),
                        HouseNr = c.Int(nullable: false),
                        MailBox = c.String(),
                        Zipcode = c.String(),
                        City = c.String(),
                        PhoneNr = c.String(),
                        FederationNr = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Player1Id = c.Int(nullable: false),
                        Player2Id = c.Int(nullable: false),
                        Player1Score = c.Int(nullable: false),
                        Player2Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player1Id)
                .ForeignKey("dbo.Players", t => t.Player2Id)
                .Index(t => t.Player1Id)
                .Index(t => t.Player2Id);
            
            CreateTable(
                "dbo.PlayerRoles",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.RoleId })
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamPlayers",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        DivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamId, t.PlayerId, t.DivisionId })
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.PlayerId)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamPlayers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.TeamPlayers", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.PlayerRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PlayerRoles", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Games", "Player2Id", "dbo.Players");
            DropForeignKey("dbo.Games", "Player1Id", "dbo.Players");
            DropForeignKey("dbo.Fines", "PlayerId", "dbo.Players");
            DropIndex("dbo.TeamPlayers", new[] { "DivisionId" });
            DropIndex("dbo.TeamPlayers", new[] { "PlayerId" });
            DropIndex("dbo.TeamPlayers", new[] { "TeamId" });
            DropIndex("dbo.PlayerRoles", new[] { "RoleId" });
            DropIndex("dbo.PlayerRoles", new[] { "PlayerId" });
            DropIndex("dbo.Games", new[] { "Player2Id" });
            DropIndex("dbo.Games", new[] { "Player1Id" });
            DropIndex("dbo.Fines", new[] { "PlayerId" });
            DropTable("dbo.Teams");
            DropTable("dbo.TeamPlayers");
            DropTable("dbo.Roles");
            DropTable("dbo.PlayerRoles");
            DropTable("dbo.Games");
            DropTable("dbo.Players");
            DropTable("dbo.Fines");
            DropTable("dbo.Divisions");
        }
    }
}
