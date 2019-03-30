namespace Milty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        User = c.String(),
                        Tag = c.String(),
                        Repository = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.AspNetUsers", "AccessLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AccessLevel", c => c.String());
            DropTable("dbo.UserTasks");
        }
    }
}
