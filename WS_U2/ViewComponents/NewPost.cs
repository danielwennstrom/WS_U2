using Microsoft.AspNetCore.Mvc;
using WS_U2.Models;

namespace WS_U2.ViewComponents
{
    public class NewPostViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Guid topicId)
        {
            Post post = new();
            post.TopicId = topicId;

            return View(post);
        }
    }
}