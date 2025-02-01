namespace UnityCareHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "Status", c => c.String());
        }
    }
}
