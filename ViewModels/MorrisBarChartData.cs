using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automata_BugTracker.ViewModels
{

    //public class MorrisBarChartDataSeries
    //{
    //    public List<MorrisBarChartData> Data { get; set; }

    //    public MorrisBarChartDataSeries()
    //    {
    //        Data = new List<MorrisBarChartData>();
    //    }
    //}

    public class MorrisBarChartData
    {
        public string TicketType { get; set; }
        public int Count { get; set; }
    }

    public class ChartData
    {
        public string Label { get; set; }
        public int Count { get; set; }
    }

}