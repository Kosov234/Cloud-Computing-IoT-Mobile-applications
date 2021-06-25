using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public string Username { get; set; }
        public double TotalCost { get; set; }
        public string ReceiptId { get; set; }
        public string Location { get; set; }
    }
}
