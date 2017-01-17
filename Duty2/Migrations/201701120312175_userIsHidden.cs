namespace Duty2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userIsHidden : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsHidden", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsHidden");
        }
    }
}
