namespace Milty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Repository",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id);
            DropColumn("dbo.AspNetUsers", "AccessLevel");
        }

        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AccessLevel", c => c.String());
            DropTable("dbo.Repository");
        }
    }
}
