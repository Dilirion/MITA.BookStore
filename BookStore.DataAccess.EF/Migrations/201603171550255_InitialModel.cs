namespace BookStore.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Birthday = c.DateTime(),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationDate = c.DateTime(nullable: false),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Birthday = c.DateTime(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        FireDate = c.DateTime(),
                        BranchId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "BranchId", "dbo.Branches");
            DropIndex("dbo.Users", new[] { "Id" });
            DropIndex("dbo.Employees", new[] { "BranchId" });
            DropTable("dbo.Users");
            DropTable("dbo.Employees");
            DropTable("dbo.Clients");
            DropTable("dbo.Branches");
            DropTable("dbo.Authors");
        }
    }
}
