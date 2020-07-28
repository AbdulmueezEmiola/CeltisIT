using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CeltisIT.Model
{
    public partial class Tabsorder
    {
        public Tabsorder()
        {
            Tabsodata = new HashSet<Tabsodata>();
        }
        [Key]
        public decimal Sordid { get; set; }
        public DateTime Sorddate { get; set; }
        public decimal Custid { get; set; }
        public decimal Sordamnt { get; set; }
        public string Dataexst { get; set; }

        public virtual Tabcust Cust { get; set; }
        public virtual ICollection<Tabsodata> Tabsodata { get; set; }
    }
}
