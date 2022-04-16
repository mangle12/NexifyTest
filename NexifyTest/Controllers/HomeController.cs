using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace NexifyTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly string _baseUrl;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _baseUrl = "https://localhost:44316/api/User/";
        }

        public async Task<ActionResult> Index()
        {
            ViewData["Status"] = Stutus.view.ToString();

            IEnumerable<UserInfos> userInfos = Enumerable.Empty<UserInfos>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
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

            ViewData["Data"] = userInfos;

            return View();
        }

        public async Task<ActionResult> Add()
        {
            ViewData["Status"] = Stutus.modify.ToString();

            IEnumerable<UserInfos> userInfos = Enumerable.Empty<UserInfos>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                var response = await client.GetAsync("Add");

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

            ViewData["Data"] = userInfos;

            return View("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}