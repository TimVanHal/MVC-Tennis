namespace Tennis.NET.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerRoletolists : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerRoles", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.PlayerRoles", new[] { "PlayerId" });
            DropIndex("dbo.PlayerRoles", new[] { "RoleId" });
            CreateTable(
                "dbo.RolePlayers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        Player_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Player_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Player_Id);
            
            DropTable("dbo.PlayerRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlayerRoles",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.RoleId });
            
            DropForeignKey("dbo.RolePlayers", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.RolePlayers", "Role_Id", "dbo.Roles");
            DropIndex("dbo.RolePlayers", new[] { "Player_Id" });
            DropIndex("dbo.RolePlayers", new[] { "Role_Id" });
            DropTable("dbo.RolePlayers");
            CreateIndex("dbo.PlayerRoles", "RoleId");
            CreateIndex("dbo.PlayerRoles", "PlayerId");
            AddForeignKey("dbo.PlayerRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlayerRoles", "PlayerId", "dbo.Players", "Id", cascadeDelete: true);
        }
    }
}
