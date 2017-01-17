namespace Duty2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userTelegramNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "TelegramNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "TelegramNumber");
        }
    }
}
