using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeltisITUI.Model
{
    public class Customer
    {
        public Customer()
        {
            Tabsorder = new HashSet<Order>();
        }
        public decimal Custid { get; set; }
        public string Custname { get; set; }

        public virtual ICollection<Order> Tabsorder { get; set; }
    }
}
