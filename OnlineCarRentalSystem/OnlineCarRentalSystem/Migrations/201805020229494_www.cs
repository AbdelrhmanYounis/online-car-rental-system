namespace OnlineCarRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class www : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "SNN");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "SNN", c => c.Int(nullable: false));
        }
    }
}
