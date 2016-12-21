namespace Duty2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Sortpos = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Duties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        PartOfDay_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartOfDays", t => t.PartOfDay_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.PartOfDay_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.PartOfDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Sortpos = c.Int(nullable: false),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Isadmin = c.Boolean(nullable: false),
                        Name = c.String(),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Duties", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Duties", "PartOfDay_Id", "dbo.PartOfDays");
            DropForeignKey("dbo.PartOfDays", "Group_Id", "dbo.Groups");
            DropIndex("dbo.Users", new[] { "Group_Id" });
            DropIndex("dbo.PartOfDays", new[] { "Group_Id" });
            DropIndex("dbo.Duties", new[] { "User_Id" });
            DropIndex("dbo.Duties", new[] { "PartOfDay_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.PartOfDays");
            DropTable("dbo.Duties");
            DropTable("dbo.Groups");
        }
    }
}
