using Automata_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automata_BugTracker.ViewModels
{
    public class ProjectTicketsViewModel
    {
        public List<Project> Projects { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<UserViewModel> Users { get; set; }
    }
}