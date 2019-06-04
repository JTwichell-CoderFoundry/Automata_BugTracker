﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Automata_BugTracker.ViewModels
{
    public class UserViewModel
    {
       
        public string Id { get; set; }

        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string ProfilePic { get; set; }
   
    }
}