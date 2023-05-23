using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers
{
    public class HomeController : ControllerBase
    {
        //get
        public IActionResult index()
        {
            return Redirect("~/swagger");
        }
    }
}