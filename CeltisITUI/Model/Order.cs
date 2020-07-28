using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeltisITUI.Model
{
    public class Order
    {
        public Order()
        {
            Tabsodata = new HashSet<OrderData>();
        }
        public decimal Sordid { get; set; }
        public DateTime Sorddate { get; set; }
        public decimal Custid { get; set; }
        public decimal Sordamnt { get; set; }
        public string Dataexst { get; set; }
        public string Date
        {
            get
            {
                return Sorddate.ToShortDateString();
            }
        }
        public virtual Customer Cust { get; set; }
        public virtual ICollection<OrderData> Tabsodata { get; set; }
    }
}
