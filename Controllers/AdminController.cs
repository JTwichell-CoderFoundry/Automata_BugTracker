using Automata_BugTracker.Helpers;
using Automata_BugTracker.Models;
using Automata_BugTracker.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automata_BugTracker.Controllers
{
    public class AdminController : Controller
    {
        //I want to add a class level property that can access the DB
        private ApplicationDbContext db = new ApplicationDbContext();

        //I want to add a class level property that can help me manage Roles
        private RoleHelper roleHelper = new RoleHelper();

        // GET: Admin
        [Authorize(Roles = "Admin,ProjectManager")]
        public ActionResult ManageRoles()
        {           
            ViewBag.Users = new SelectList(db.Users.ToList(), "Id", "Email");
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(string users, string roles)
        {
            //I want to ensure that the person I selected occupies one and only 1 role
            //Therefore the first thing I am going to do is remove the user from any role
            //they currently occupy

            //Step 1: Remove any roles currently occupied by the user. We can do this by
            // looping over the roles currently occupied by the user using the roleHelper
            foreach(var role in roleHelper.ListUserRoles(users))
            {
                roleHelper.RemoveUserFromRole(users, role);
            }

            //Step 2: Add the newly selected role to the user
            roleHelper.AddUserToRole(users, roles);

            return RedirectToAction("UserIndex", "Admin");
        }

        [Authorize]
        public ActionResult ManageMyRole(string email)
        {           
            //I want to determine if I am already in a Role...        
            var myCurrentRole = roleHelper.ListEmailRoles(email).FirstOrDefault();
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name", myCurrentRole);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageMyRole(string email, string roles)
        {
            var userId = db.Users.FirstOrDefault(u => u.Email == email).Id;

            foreach (var role in roleHelper.ListUserRoles(userId))
            {
                roleHelper.RemoveUserFromRole(userId, role);
            }

            //Step 2: Add the newly selected role to the user
            roleHelper.AddUserToRole(userId, roles);

            return RedirectToAction("UserIndex", "Admin");
        }



        public ActionResult UserIndex()
        {
            var users = db.Users.Select(u => new UserViewModel
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                DisplayName = u.DisplayName,
                Email = u.Email
            }).ToList();
            
            return View(users);
        }
    }
}