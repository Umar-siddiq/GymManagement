using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Web.Areas.Admin.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
