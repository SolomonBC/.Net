using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticketandvisitormgmtsys
{
    public class WeeklyReport
    {
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public int TotalCustomer { get; set; }
        public double TotalIncome { get; set; }
    }
}
