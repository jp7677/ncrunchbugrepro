using Microsoft.AspNetCore.Mvc;

namespace NCrunchBugRepro
{
    public class TestController : Controller
    {
        [HttpGet("/")]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
