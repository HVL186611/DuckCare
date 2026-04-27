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
    }
}
