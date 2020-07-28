using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CeltisIT.Model
{
    public partial class Item
    {
        public Item()
        {
            Tabsodata = new HashSet<Tabsodata>();
        }
        [Key]
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UnitType { get; set; }
        public int Rate { get; set; }

        public virtual ICollection<Tabsodata> Tabsodata { get; set; }
    }
}
