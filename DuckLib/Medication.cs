 using System;
using System.Collections.Generic;
using System.Text;

namespace DuckLib
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        // for patient medications:
        public string? Route { get; set; }
        public string? Dose { get; set; }
        public string? Frequency { get; set; }

        public static Medication FromEntity(DuckLib.Models.Medication entity)
        {
            return new Medication
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        public DuckLib.Models.Medication ToEntity()
        {
            return new DuckLib.Models.Medication
            {
                Id = this.Id,
                Name = this.Name,
            };
        }
    }

}
