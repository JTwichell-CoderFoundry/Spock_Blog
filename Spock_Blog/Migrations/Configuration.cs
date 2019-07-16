namespace Spock_Blog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Spock_Blog.Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Spock_Blog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Spock_Blog.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            #region roleManager
            var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }
            #endregion

            //I need to create a few users that will eventually occupy the roles of either Admin or Moderator
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "JTwichell@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "JTwichell@Mailinator.com",
                    Email = "JTwichell@Mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Twich"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "JoeSchmo@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "JoeSchmo@Mailinator.com",
                    Email = "JoeSchmo@Mailinator.com",
                    FirstName = "Joe",
                    LastName = "Schmo",
                    DisplayName = "Joey"
                }, "Abc&123!");
            }

            var userId = userManager.FindByEmail("JTwichell@Mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("JoeSchmo@Mailinator.com").Id;
            userManager.AddToRole(userId, "Moderator");

        }
    }
}
