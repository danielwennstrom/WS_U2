using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WS_U2.Models;
using WS_U2.Services;

namespace WS_U2.Controllers
{
    public class PostController : Controller
    {
        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("TopicId, PostBody")] Post post)
        {
            AESService aesService = new();
            Topic topic = new();

            using (var httpClient = new HttpClient())
            {
                post.PostedBy = aesService.GenerateEncryptedUsername(9);
                post.PostedDateTime = DateTime.Now;

                topic.LastPostDateTime = DateTime.Now;
                topic.TopicId = post.TopicId;

                var postPayload = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
                var topicPayload = new StringContent(JsonConvert.SerializeObject(topic), Encoding.UTF8, "application/json");
                await httpClient.PostAsync("https://localhost:44375/api/posts", postPayload);
                await httpClient.PatchAsync("https://localhost:44375/api/topics/" + post.TopicId, topicPayload);
            }

            return Redirect("/Home/Index");
        }
    }
}