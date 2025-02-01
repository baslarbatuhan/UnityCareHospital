namespace UnityCareHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contacts", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "Date", c => c.DateTime(nullable: false));
        }
    }
}
