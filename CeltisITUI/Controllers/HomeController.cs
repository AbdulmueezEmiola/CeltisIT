using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CeltisITUI.Infrastructure;
using CeltisITUI.Model;
using CeltisITUI.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CeltisITUI.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IActionResult> Index(string searchString, int itemPage = 1, int pageSize = 2)
        {
            List<Item> itemLists = new List<Item>();            
            ViewData["Search"] = searchString;
            string token = configuration.GetSection("Token").Value;
            if(token == null)
            {
                return RedirectToAction("Login", "Login");
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using(var response = await httpClient.GetAsync("https://localhost:44367/api/Items/"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        itemLists = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }
                }
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                itemLists = itemLists.Where(x => x.Title.Contains(searchString) || x.Description.Contains(searchString)).ToList();
            }
            ViewBag.Select = GetDropdownItems();
            return View(new ViewModel
            {
                items = itemLists.OrderBy(p => p.ItemId).Skip((itemPage - 1) * pageSize).Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = itemPage,
                    ItemsPerPage = pageSize,
                    TotalItems = itemLists.Count()
                }
            }); 
        }
        private IEnumerable<SelectListItem> GetDropdownItems()
        {           
            var list = Enumerable.Range(1, 10).ToList().Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() });
            SelectList listItems = new SelectList(list, "Value", "Text");
            return listItems;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Item item)
        {
            string token = configuration.GetSection("Token").Value;
            if (token == null)
            {
                return RedirectToAction("Login", "Login");
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsJsonAsync<Item>("https://localhost:44367/api/Items/",item))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }
                }
            }
        }
    }    
}