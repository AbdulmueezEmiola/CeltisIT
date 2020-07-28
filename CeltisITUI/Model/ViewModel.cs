using CeltisITUI.Model.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeltisITUI.Model
{
    public class ViewModel
    {
        public IEnumerable<Item> items { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
