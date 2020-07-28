using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CeltisIT.Model
{
    public partial class User
    {
        [Key]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
}
