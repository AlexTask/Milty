namespace Milty.Migrations
{
  
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using Milty.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Milty.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Milty.Models.ApplicationDbContext";
        }

        protected  override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));
            if (!roleManager.RoleExists("Teacher"))
                roleManager.Create(new IdentityRole("Teacher"));
            if (!roleManager.RoleExists("User"))
                roleManager.Create(new IdentityRole("User"));

            //context.Roles.AddOrUpdate(new IdentityRole { Name = "Admin" });
            //context.Roles.AddOrUpdate(new IdentityRole { Name = "Teacher" });
            //context.Roles.AddOrUpdate(new IdentityRole { Name = "User" });
            //context.SaveChanges();
            

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var adminUser = userManager.FindByName("admin@milty.com");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = "admin@milty.com", Email = "admin@milty.com" };
                userManager.Create(adminUser, "Admin!2345");
            }

            if (!userManager.IsInRole(adminUser.Id, "Admin"))
                userManager.AddToRole(adminUser.Id, "Admin");

            userManager.AddToRole(adminUser.Id, "Admin");
        }
    }
}
