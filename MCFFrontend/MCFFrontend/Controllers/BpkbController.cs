using MCFFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace MCFFrontend.Controllers
{
    public class BpkbController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BpkbController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Index(string locations, string user)
        {
            var locationList = JsonConvert.DeserializeObject<List<ms_location>>(locations);
            var userObject = JsonConvert.DeserializeObject<user>(user);

            ViewBag.User = userObject;
            ViewBag.Locations = locationList;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(tr_bpkb trBpkb)
        {
            if (!ModelState.IsValid)
            {
                return View(trBpkb);
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:7014/api/TrBpkb", trBpkb);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<ApiResponse>();

                if (responseData != null && responseData.Success)
                {
                    return RedirectToAction("Index", "Bpkb");
                }
                else
                {
                    ViewBag.Error = responseData?.Message ?? "Unexpected error occurred!";
                }
            }
            else
            {
                ViewBag.Error = "Error inserting record!";
            }

            return View(trBpkb);
        }
    }
}
