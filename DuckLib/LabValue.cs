using System;
using System.Collections.Generic;
using System.Text;

namespace DuckLib
{
    public class LabValue
    {
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
        public string Reference { get; set; } = "";
        public string Interpretation { get; set; } = "";

        public int Id = -1; // for conversion. if Id==-1, assign Id before saving to database

        internal static List<LabValue> FromEntity(ICollection<Models.LabValue> entities)
        {
            List<LabValue> result = new();
            foreach (var e in entities)
            {
                result.Add(new LabValue
                {
                    Id = e.Id,
                    Value = e.Value,
                    Reference = e.Reference,
                    Interpretation = e.Interpretation
                });
            }
            return result;
        }

        internal DuckLib.Models.LabValue ToEntity(int SimulationId)
        {
            return new DuckLib.Models.LabValue
            {
                Id = Id,
                SimulationId = SimulationId,
                Value = Value,
                Reference = Reference,
                Interpretation = Interpretation
            };
        }
    }
}
