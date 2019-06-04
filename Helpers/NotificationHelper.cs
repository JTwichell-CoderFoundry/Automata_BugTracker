using Automata_BugTracker.Models;
using System;

namespace Automata_BugTracker.Helpers
{
    public class NotificationHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void TriggerAssignmentNotifications(int ticketId, string oldDeveloper, string newDeveloper)
        {
            //Setup a few simple variables that tell me which situation is afoot
            var newAssignment = string.IsNullOrEmpty(oldDeveloper) && !string.IsNullOrEmpty(newDeveloper);
            var unAssignment = !string.IsNullOrEmpty(oldDeveloper) && string.IsNullOrEmpty(newDeveloper);
            var reAssignment = !string.IsNullOrEmpty(oldDeveloper) && !string.IsNullOrEmpty(newDeveloper) && oldDeveloper != newDeveloper;

            if(newAssignment)
            {
                AddAssignmentNotification(ticketId, newDeveloper);
            }
            else if(unAssignment)
            {
                AddUnAssignmentNotification(ticketId, oldDeveloper);
            }
            else if(reAssignment)
            {
                AddAssignmentNotification(ticketId, newDeveloper);
                AddUnAssignmentNotification(ticketId, oldDeveloper);
            }          
        }

        private void AddAssignmentNotification(int ticketId, string newDeveloper)
        {
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                TicketId = ticketId,
                UnRead = true,
                UserId = newDeveloper,
                NotificationBody = $"You have been assigned to Ticket: {ticketId}."
            };

            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }

        private void AddUnAssignmentNotification(int ticketId, string oldDeveloper)
        {
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                TicketId = ticketId,
                UnRead = true,
                UserId = oldDeveloper,
                NotificationBody = $"You have been unassigned to Ticket: {ticketId}."
            };

            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }   
    }
}