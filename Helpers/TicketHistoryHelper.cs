using Automata_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Web;

namespace Automata_BugTracker.Helpers
{
    public class TicketHistoryHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void RecordTicketChanges(Ticket oldTicket, Ticket newTicket)
        {
            //Compare the oldTicket property values to the newTicket property values
            //If they are different then we add a new TicketHistory record

            if (oldTicket.TicketTypeId != newTicket.TicketTypeId)
            {
                AddTicketHistory(newTicket.Id, "TicketTypeId", oldTicket.TicketTypeId.ToString(), newTicket.TicketTypeId.ToString());
            }
          
            if (oldTicket.TicketPriorityId != newTicket.TicketPriorityId)
            {
                AddTicketHistory(newTicket.Id, "TicketPriorityId", oldTicket.TicketPriorityId.ToString(), newTicket.TicketPriorityId.ToString());
            }

            if (oldTicket.TicketStatusId != newTicket.TicketStatusId)
            {
                AddTicketHistory(newTicket.Id, "TicketStatusId", oldTicket.TicketStatusId.ToString(), newTicket.TicketStatusId.ToString());
            }

            if (oldTicket.OwnerUserId != newTicket.OwnerUserId)
            {
                AddTicketHistory(newTicket.Id, "OwnerUserId", oldTicket.OwnerUserId, newTicket.OwnerUserId);
            }

            if (oldTicket.AssignedToUserId != newTicket.AssignedToUserId)
            {
                AddTicketHistory(newTicket.Id, "AssignedToUserId", oldTicket.AssignedToUserId, newTicket.AssignedToUserId);
            }

            if (oldTicket.Title != newTicket.Title)
            {
                AddTicketHistory(newTicket.Id, "Title", oldTicket.Title, newTicket.Title);
            }
        }

        public void AddTicketHistory(int ticketId, string propertyName, string oldValue, string newValue)
        {
            var newTicketHistory = new TicketHistory
            {
                Property = propertyName,
                OldValue = oldValue,
                NewValue = newValue,
                ChangedDate = DateTime.Now,
                UserId = HttpContext.Current.User.Identity.GetUserId(),
                TicketId = ticketId
            };

            db.TicketHistories.Add(newTicketHistory);
            db.SaveChanges();
        }
    }
}