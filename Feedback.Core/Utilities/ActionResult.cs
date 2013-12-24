using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Core.Utilities
{
    public class FuncResult
    {
        public IEnumerable<string> Errors { get; set; }
        public bool Succeeded { get; set; }
    }
}
