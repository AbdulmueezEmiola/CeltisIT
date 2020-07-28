using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeltisITUI.Model.ViewModels
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPage > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (CurrentPage < TotalPages());
            }
        }
        public int itemFirst
        {
            get
            {
                return (CurrentPage - 1) * ItemsPerPage+1;
            }
        }
        public int itemLast
        {
            get
            {
                return HasNextPage?CurrentPage * ItemsPerPage:TotalItems;
            }
        }
        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
