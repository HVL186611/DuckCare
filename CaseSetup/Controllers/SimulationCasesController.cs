using Microsoft.AspNetCore.Mvc;
using DuckLib;
using CaseSetup.Services;

namespace CaseSetup.Controllers
{
    public class SimulationCasesController : Controller
    {
        private readonly SimulationCaseService _caseService;

        public SimulationCasesController(SimulationCaseService caseService)
        {
            _caseService = caseService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") == null) return RedirectToAction("Login", "Account");

            List<SimulationCase> cases = _caseService.GetAll();

            return View(cases);
        }

        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("Role") == null) return RedirectToAction("Login", "Account");

            SimulationCase? simulationCase = _caseService.GetById(id);
            
            if (simulationCase == null) return NotFound();

            return View(simulationCase);

        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") == null) return RedirectToAction("Login", "Account");

            SimulationCase simulationCase = new SimulationCase
            {
                Patient = new(),
                StartVitals = new()
            };
            return View(simulationCase);
        }

        [HttpPost]
        public IActionResult Create(SimulationCase simulationCase)
        {
            // (hopefully) sends form back to Create view to be filled in
            if (!ModelState.IsValid) return View(simulationCase);

            // allowing anyone to create as student if not logged in
            // you shouldn't get to this page without logging in, but just in case...
            simulationCase.CreatedByRole = HttpContext.Session.GetString("Role") ?? "student";

            // add case to database
            _caseService.Add(simulationCase);

            // return to case list
            return RedirectToAction(nameof(Index));
        }
    }
}
