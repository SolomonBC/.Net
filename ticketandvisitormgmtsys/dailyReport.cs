using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticketandvisitormgmtsys
{
    public class DailyReport
    {
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public int TotalCustomer { get; set; }
    }
}
