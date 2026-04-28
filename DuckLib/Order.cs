using System;
using System.Collections.Generic;
using System.Text;

namespace DuckLib
{
    public class Order
    {
        public string Action { get; set; } = "";
        public string Details { get; set; } = "";
        public string Route { get; set; } = "";
        public string Timing { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
