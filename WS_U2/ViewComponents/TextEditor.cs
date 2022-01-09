using Microsoft.AspNetCore.Mvc;

namespace WS_U2.ViewComponents
{
    public class TextEditorViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string postbody)
        {
            return View();
        }
    }
}