using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticketandvisitormgmtsys
{
    public class ticketDetails
    {
        public string ticketId {get; set;}
        public string category { get; set; }
        public string totalPeople { get; set; }
        public string day { get; set; }
        public string duration { get; set; }
        public string price { get; set; }
        public string sortBy { get; set; }
    }
}
