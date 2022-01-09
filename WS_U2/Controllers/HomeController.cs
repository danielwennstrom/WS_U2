using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WS_U2.Models;

namespace WS_U2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Topic> topicList = new();
            List<Topic> orderedList = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44375/api/topics"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    topicList = JsonConvert.DeserializeObject<List<Topic>>(apiResponse);
                }
            }

            orderedList = topicList.OrderByDescending(x => x.LastPostDateTime).ToList();

            return View(orderedList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}