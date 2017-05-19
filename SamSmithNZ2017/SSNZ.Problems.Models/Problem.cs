using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNZ.Problems.Models
{
    public class Problem
    {
        public Problem() { }

        public int ProblemNumber { get; set; }
        public String Description { get; set; }
        public String Notes { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
