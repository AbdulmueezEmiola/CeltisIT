using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CeltisITUI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CeltisITUI.Controllers
{
    public class OrderController : Controller
    {
        private IConfiguration configuration;

        public OrderController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            List<Order> orderLists = new List<Order>();
            string token = configuration.GetSection("Token").Value;
            if (token == null)
            {
                return RedirectToAction("Login", "Login");
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync("https://localhost:44367/api/Orders/"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        orderLists = JsonConvert.DeserializeObject<List<Order>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }
                }
            }
            return View(orderLists);
        }
        [Route("Order/{id}")]
        public async Task<IActionResult> Specific(double id)
        {
            Order order;
            string token = configuration.GetSection("Token").Value;
            if (token == null)
            {
                return RedirectToAction("Login", "Login");
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync("https://localhost:44367/api/Orders/" + id.ToString())) 
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        order = JsonConvert.DeserializeObject<Order>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }
                }
            }
            return View("Specific", order);
        }
    }
}