namespace Automata_BugTracker.Migrations
{
    using Automata_BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Automata_BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {            
            var roleManager = new RoleManager<IdentityRole>(
                   new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            //Add a new user to act as a Admin
            var userManager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "JasonTwichell@Mailinator.com"))
            {     
                userManager.Create(new ApplicationUser
                {
                    Email = "JasonTwichell@Mailinator.com",
                    UserName = "JasonTwichell@Mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Twich"
                }, "Abc&123!");
            }

            var userId = userManager.FindByEmail("JasonTwichell@Mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            //Add a new user to act as a Project Manager
            if (!context.Users.Any(u => u.Email == "ProjectManager@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "ProjectManager@Mailinator.com",
                    UserName = "ProjectManager@Mailinator.com",
                    FirstName = "Project",
                    LastName = "Manager",
                    DisplayName = "Im A PM"
                }, "Abc&123!");
            }

            userId = userManager.FindByEmail("ProjectManager@Mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            //Add a new user to act as a Developer
            if (!context.Users.Any(u => u.Email == "SeededDeveloper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "SeededDeveloper@Mailinator.com",
                    UserName = "SeededDeveloper@Mailinator.com",
                    FirstName = "Seeded",
                    LastName = "Developer",
                    DisplayName = "Im A Developer"
                }, "Abc&123!");
            }

            userId = userManager.FindByEmail("SeededDeveloper@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            //Add a new user to act as a Submitter
            if (!context.Users.Any(u => u.Email == "SeededSubmitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "SeededSubmitter@Mailinator.com",
                    UserName = "SeededSubmitter@Mailinator.com",
                    FirstName = "Seeded",
                    LastName = "Submitter",
                    DisplayName = "Im A Submitter"
                }, "Abc&123!");
            }

            userId = userManager.FindByEmail("SeededSubmitter@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            //Add records into the TicketStatus table
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                    new TicketPriority { Name = "Immediate", Description = "The highest possible priority" },
                    new TicketPriority { Name = "High", Description = "The 2nd highest possible priority" },
                    new TicketPriority { Name = "Medium", Description = "A mid-level priority used to denote average importance" },
                    new TicketPriority { Name = "Low", Description = "The 2nd lowest possible priority" },
                    new TicketPriority { Name = "None", Description = "The lowest possible priority" }
            );

            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                    new TicketType { Name = "Defect", Description = "A reported defect in a supported project or application" },
                    new TicketType { Name = "New functionality request", Description = "A new request for functionality in a supported project or application" },
                    new TicketType { Name = "Call for documentation", Description = "A new request for documentation in a supported project or application" }
            );

            context.TicketStatuses.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Name = "New/Unassigned", Description = "This will be the status of all newly created Tickets"},
                    new TicketStatus { Name = "Assigned", Description = "This will be the status of Tickets that are assigned to a Developer" },
                    new TicketStatus { Name = "Completed", Description = "This will be the status of all completed but un-archived Tickets" },
                    new TicketStatus { Name = "Archived", Description = "This will be the status of all completed and archived Tickets" }
            );   
            
        }
    }
}
