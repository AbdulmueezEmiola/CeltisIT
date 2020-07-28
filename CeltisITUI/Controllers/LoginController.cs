using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CeltisITUI.Model;
using Microsoft.AspNetCore.Mvc;
using CeltisITUI.Infrastructure;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace CeltisITUI.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration configuration;
        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public  IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            TokenContainer token = new TokenContainer();
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync<User>("https://localhost:44367/api/Login/", user);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    token = JsonConvert.DeserializeObject<TokenContainer>(apiResponse);
                    configuration.GetSection("Token").Value = token.Token;
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
        }
    }
}