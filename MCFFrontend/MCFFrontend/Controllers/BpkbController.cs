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
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7014/api/GetAllBpkb");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var bpkbsObject = JsonConvert.DeserializeObject<RootBpkbs>(responseData);
                return View(bpkbsObject.responseBpkbs.result);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving data from the API.");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string agreementNumber)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7014/api/edit/{agreementNumber}");
            var response2 = await client.GetAsync("https://localhost:7014/api/GetAllData");
            if (response.IsSuccessStatusCode && response2.IsSuccessStatusCode)
            {
                var responseData2 = await response2.Content.ReadAsStringAsync();
                var deserializedData = JsonConvert.DeserializeObject<BpkbResponse>(responseData2);
                var locationList = deserializedData.data.Locations.result;
                var userObject = deserializedData.data.user.userName;
                ViewBag.User = userObject;
                ViewBag.Locations = locationList;
                var responseData = await response.Content.ReadAsStringAsync();
                var bpkbObject = JsonConvert.DeserializeObject<tr_bpkb>(responseData);
                return View(bpkbObject);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving data from the API.");
            }

            return RedirectToAction("Index", "Bpkb");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(tr_bpkb model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"https://localhost:7014/api/edit/{model.AgreementNumber}", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error updating data.");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7014/api/GetAllData");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var deserializedData = JsonConvert.DeserializeObject<BpkbResponse>(responseData);
                var locationList = deserializedData.data.Locations.result;
                var userObject = deserializedData.data.user.userName;
                ViewBag.User = userObject;
                ViewBag.Locations = locationList;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving data from the API.");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(tr_bpkb trBpkb)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Bpkb");
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

            return RedirectToAction("Index", "Bpkb");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string agreementNumber)
        {
            if (string.IsNullOrEmpty(agreementNumber))
            {
                ModelState.AddModelError(string.Empty, "Agreement number is required.");
                return RedirectToAction("Index", "Bpkb");
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7014/api/delete/{agreementNumber}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Bpkb");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error deleting data.");
            }

            return RedirectToAction("Index", "Bpkb");
        }
    }
}
