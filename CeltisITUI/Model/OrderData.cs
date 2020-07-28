using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeltisITUI.Model
{
    public class OrderData
    {
        public decimal Sodataid { get; set; }
        public decimal Sordid { get; set; }
        public int Itemid { get; set; }
        public decimal Itemrate { get; set; }
        public string Dataexst { get; set; }

        public virtual Item Item { get; set; }
        public virtual Order Sord { get; set; }
    }
}
