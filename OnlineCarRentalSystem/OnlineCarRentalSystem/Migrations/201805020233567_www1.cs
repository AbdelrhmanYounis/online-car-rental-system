namespace OnlineCarRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class www1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "SNN", c => c.String(nullable:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "SNN");
        }
    }
}
