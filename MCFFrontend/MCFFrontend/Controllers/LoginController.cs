using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using MCFFrontend.Models;
using System.Net.Http;

namespace MCFFrontend.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string user_name, string password)
        {
            var client = _httpClientFactory.CreateClient();
            var loginRequest = new LoginRequest
            {
                user_name = user_name,
                password = password
            };

            var response = await client.PostAsJsonAsync("https://localhost:7014/api/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Bpkb");
            }

            ViewBag.Error = "User not found!";
            return View();
        }
    }
}
