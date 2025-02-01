namespace UnityCareHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "User_UserID", "dbo.Users");
            DropIndex("dbo.Appointments", new[] { "User_UserID" });
            DropColumn("dbo.Appointments", "User_UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "User_UserID", c => c.Int());
            CreateIndex("dbo.Appointments", "User_UserID");
            AddForeignKey("dbo.Appointments", "User_UserID", "dbo.Users", "UserID");
        }
    }
}
