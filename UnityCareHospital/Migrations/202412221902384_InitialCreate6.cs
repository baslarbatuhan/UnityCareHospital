namespace UnityCareHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ImgID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ImgID");
        }
    }
}
