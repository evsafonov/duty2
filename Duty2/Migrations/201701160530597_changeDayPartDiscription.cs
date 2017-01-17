namespace Duty2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDayPartDiscription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartOfDays", "TimeFrom", c => c.String());
            AddColumn("dbo.PartOfDays", "TimeTo", c => c.String());
            DropColumn("dbo.PartOfDays", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PartOfDays", "Description", c => c.String());
            DropColumn("dbo.PartOfDays", "TimeTo");
            DropColumn("dbo.PartOfDays", "TimeFrom");
        }
    }
}
