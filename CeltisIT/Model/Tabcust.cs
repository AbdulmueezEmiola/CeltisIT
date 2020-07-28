using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CeltisIT.Model
{
    public partial class Tabcust
    {
        public Tabcust()
        {
            Tabsorder = new HashSet<Tabsorder>();
        }

        [Key]
        public decimal Custid { get; set; }
        public string Custname { get; set; }

        public virtual ICollection<Tabsorder> Tabsorder { get; set; }
    }
}
