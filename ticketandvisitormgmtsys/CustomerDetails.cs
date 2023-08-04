using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticketandvisitormgmtsys
{
    public class CustomerDetails
    {
        public string customerId { get; set; }
        public DateTime Date { get; set; }
        public string customerName { get; set; }
        public string mobileNo { get; set; }
        public string checkInTime { get; set; }
        public string checkOutTime { get; set; }
        public string ticketDetailsId { get; set; }
        public string Day { get; set; }
        public string duration { get; set; }
        public string totalPrice { get; set; }

        public string totalpeople { get; set; }
    }
}
