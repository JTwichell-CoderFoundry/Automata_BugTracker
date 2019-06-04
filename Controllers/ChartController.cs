using Automata_BugTracker.Models;
using Automata_BugTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automata_BugTracker.Controllers
{
    public class ChartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            var recentTickets = db.Tickets.Include("AssignedToUser").Include("OwnerUser").OrderByDescending(t => t.Created).Take(5).ToList();
            return View(recentTickets);
        }

        public JsonResult GetTicketTypeData()
        {
            var ticketTypes = db.TicketTypes.ToList();  
            var chartData = new List<MorrisBarChartData>();

            foreach (var type in ticketTypes)
            {
                chartData.Add(new MorrisBarChartData()
                {
                    TicketType = type.Name,
                    Count = db.Tickets.AsNoTracking().Where(t => t.TicketType.Name == type.Name).Count()
                });
            }
            return Json(chartData);
        }

        public JsonResult GetTicketPriorityData()
        {
            var ticketPriorities = db.TicketPriorities.ToList();
            var chartData = new List<ChartData>();

            foreach (var type in ticketPriorities)
            {
                chartData.Add(new ChartData()
                {
                    Label = type.Name,
                    Count = db.Tickets.AsNoTracking().Where(t => t.TicketPriority.Name == type.Name).Count()
                });
            }
            return Json(chartData);
        }

        public JsonResult GetBarFlotChartData()
        {
            //data:[["João Santos", 5],["Ana Torres", 4],["Luis Silva", 3],["Perdro Alves", 6],["Beatriz Teixeira", 5]] 

            var chartData = new List<BarFlotChartData>();

            var series1 = new BarFlotChartData();
            series1.data.Add("Project 1", 3);
            series1.data.Add("Project 2", 3);
            series1.data.Add("Project 3", 3);
            series1.data.Add("Project 4", 3);
            chartData.Add(series1);

            var series2 = new BarFlotChartData();
            series2.data.Add("Project 1", 4);
            series2.data.Add("Project 2", 4);
            series2.data.Add("Project 3", 4);
            series2.data.Add("Project 4", 4);
            chartData.Add(series2);

            return Json(chartData);
        }
        



    }
}