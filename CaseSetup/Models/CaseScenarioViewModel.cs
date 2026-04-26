

using DuckLib;

namespace CaseSetup.Models
{
    public class CaseScenarioViewModel
    {
        public int Id { get; set; }

        public string CaseTitle { get; set; } = "";
        public Patient Patient { get; set; } = new();  // oooo new() 


        public bool StudentEditable { get; set; } = true;
        bool IsActive { get; set; }
    }
}
