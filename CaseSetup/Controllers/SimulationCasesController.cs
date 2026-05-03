using Microsoft.AspNetCore.Mvc;
using DuckLib;
using CaseSetup.Services;

namespace CaseSetup.Controllers
{
    public class SimulationCasesController : Controller
    {
        private readonly SimulationCaseService _caseService;
        private bool requireLogin = false; // todo: change to true when testing is done

        public SimulationCasesController(SimulationCaseService caseService)
        {
            _caseService = caseService;
        }

        private bool shouldLogin()
        {
            return requireLogin && string.IsNullOrEmpty(HttpContext.Session.GetString("Role"));
        }

        private void fillViewBag()
        {
            ViewBag.Medications = _caseService.GetMedicationNames();
        }

        public IActionResult Index()
        {
            if (shouldLogin()) return RedirectToAction("Login", "Account");

            List<SimulationCase> cases = _caseService.GetAllCases();

            return View(cases);
        }

        public IActionResult Details(int id)
        {
            if (shouldLogin()) return RedirectToAction("Login", "Account");

            SimulationCase? simulationCase = _caseService.GetById(id);
            
            if (simulationCase == null) return NotFound();

            return View(simulationCase);

        }

        public IActionResult Create()
        {
            if (shouldLogin()) return RedirectToAction("Login", "Account");

            fillViewBag();

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
            if (!ModelState.IsValid) { 
                fillViewBag();
                return View(simulationCase); 
            }
            fillViewBag();

            // allowing anyone to create as student if not logged in
            // you shouldn't get to this page without logging in, but just in case...
            if (simulationCase.CreatedByRole == null) 
                simulationCase.CreatedByRole = HttpContext.Session.GetString("Role") ?? "student";

            simulationCase.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            simulationCase.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _caseService.Add(simulationCase);
            /*
            // add/update case database
            if (simulationCase.Id == 0)  // new cases have Id=0
                _caseService.Add(simulationCase);
            else
            {
                if (!(simulationCase.StudentEditable == 1) && HttpContext.Session.GetString("Role") != "teacher")
                {
                    ModelState.AddModelError("", "Only teachers can edit this case.");  // i don't think this works, but it shouldnt be possible for students to get to this page anyways
                    return View(simulationCase);
                }
                _caseService.Update(simulationCase);
            }
            // -----------
            //*/

            // return to case list
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SimulationCase simulationCase)
        {
            if (shouldLogin()) return RedirectToAction("Login", "Account");

            fillViewBag();

            if (id != simulationCase.Id) return BadRequest();
            simulationCase.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            SimulationCase? existing = _caseService.GetById(id);
            if (existing == null) return NotFound();

            if (existing.StudentEditable != 1 && HttpContext.Session.GetString("Role") != "teacher")
            {
                ModelState.AddModelError("", "Only teachers can edit this case.");
                return View("Create", simulationCase);
            }

            if (!ModelState.IsValid) return View("Create", simulationCase);

            _caseService.Update(simulationCase);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (shouldLogin()) return RedirectToAction("Login", "Account");
            fillViewBag();
            SimulationCase? simulationCase = _caseService.GetById(id);
            if (simulationCase == null) return NotFound();

            // reusing create view for updates
            return View("Create", simulationCase);
        }
    }
}
