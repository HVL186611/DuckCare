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
            List<SimulationCase> cases = _caseService.GetAll();

            return View(cases);
        }

        public IActionResult Details(int id)
        {
            SimulationCase? simulationCase = _caseService.GetById(id);
            
            if (simulationCase == null) return NotFound();

            return View(simulationCase);

        }

        public IActionResult Create()
        {
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

            // add case to database
            _caseService.Add(simulationCase);

            // return to case list
            return RedirectToAction(nameof(Index));
        }
    }
}
