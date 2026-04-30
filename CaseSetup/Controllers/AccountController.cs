using Microsoft.AspNetCore.Mvc;

namespace CaseSetup.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string role, string? password)
        {
            role = role?.Trim().ToLower();

            if (role != "teacher" && role != "student")
            {
                //ModelState.AddModelError("role", "Select a role to log in.");
                return View(); // back to login with error
            }

            if (role == "teacher" && password?.ToLower() != "i love ducks")
            {
                ModelState.AddModelError("password", "Those aren't the magic words!");
                return View(); // back to login with error
            }

            HttpContext.Session.SetString("Role", role.ToLower());

            return RedirectToAction("Index", "SimulationCases");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Role");

            return RedirectToAction("Login");
        }
    }
}
