using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeltisITUI.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UnitType { get; set; }
        public int Rate { get; set; }
    }
}
