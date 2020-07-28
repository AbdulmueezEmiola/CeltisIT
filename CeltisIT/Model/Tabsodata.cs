using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CeltisIT.Model
{
    public partial class Tabsodata
    {
        [Key]
        public decimal Sodataid { get; set; }
        public decimal Sordid { get; set; }
        public int Itemid { get; set; }
        public decimal Itemrate { get; set; }
        public string Dataexst { get; set; }

        public virtual Item Item { get; set; }
        public virtual Tabsorder Sord { get; set; }
    }
}
