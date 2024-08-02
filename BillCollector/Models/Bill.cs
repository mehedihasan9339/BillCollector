using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillCollector.Models
{
    public class Bill
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime BillDate { get; set; }
        public decimal Amount { get; set; }
    }
}
