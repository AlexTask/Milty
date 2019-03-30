namespace Milty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "AccessLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AccessLevel", c => c.String());
        }
    }
}
