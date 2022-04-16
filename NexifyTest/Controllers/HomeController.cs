using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NexifyTest.Models;
using System.Diagnostics;
using System.Net;

namespace NexifyTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<UserInfos> userInfos = Enumerable.Empty<UserInfos>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44316/api/User/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                var response = await client.GetAsync("UserList");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    userInfos = JsonConvert.DeserializeObject<IList<UserInfos>>(data);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }

            return View(userInfos);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}