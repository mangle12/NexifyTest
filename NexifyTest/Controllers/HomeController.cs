using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

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
            ViewData["Status"] = Status.view.ToString();

            List<UserInfos> userInfos = new List<UserInfos>();

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
                    userInfos = JsonConvert.DeserializeObject<IList<UserInfos>>(data).ToList();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }

            return View(userInfos);
        }

        public async Task<ActionResult> Add()
        {            
            ViewData["Status"] = Status.modify.ToString();

            List<UserInfos> userInfos = new List<UserInfos>();

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
                    userInfos = JsonConvert.DeserializeObject<IList<UserInfos>>(data).ToList();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View("Index", userInfos);
        }

        [HttpPost]
        public async Task<ActionResult> Save(List<UserInfos> req)
        {
            ViewData["Status"] = Status.view.ToString();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var json = JsonConvert.SerializeObject(req);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                //HTTP Post
                var response = await client.PostAsync("Save", data);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return RedirectToAction("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}