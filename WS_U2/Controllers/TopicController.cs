using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WS_U2.Models;
using WS_U2.Services;

namespace WS_U2.Controllers
{
    public class TopicController : Controller
    {
        // GET: TopicController/Details/5
        public async Task<ActionResult> View(int id)
        {
            Topic topic = new();
            List<Post> posts;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44375/api/topics/" + id))
                {
                    string topicResponse = await response.Content.ReadAsStringAsync();
                    topic = JsonConvert.DeserializeObject<Topic>(topicResponse);
                }

                using (var response = await httpClient.GetAsync("https://localhost:44375/api/posts/" + topic.TopicId))
                {
                    string postsResponse = await response.Content.ReadAsStringAsync();
                    posts = JsonConvert.DeserializeObject<List<Post>>(postsResponse);
                }
            }
            ViewBag.Posts = posts;
            return View(topic);
        }

        public ActionResult Cancel()
        {
            return Redirect("/Home/Index");
        }

        // GET: TopicController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TopicController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Title, PostBody")] Topic topic)
        {
            AESService aesService = new();
            topic.CreatedBy = aesService.GenerateEncryptedUsername(9);
            topic.CreatedDateTime = DateTime.Now;
            topic.LastPostDateTime = DateTime.Now;
            topic.TopicId = Guid.NewGuid();

            using (var httpClient = new HttpClient())
            {
                var topicPayload = new StringContent(JsonConvert.SerializeObject(topic), Encoding.UTF8, "application/json");
                await httpClient.PostAsync("https://localhost:44375/api/topics", topicPayload);
            }

            return Redirect("/Home/Index");
        }
    }
}