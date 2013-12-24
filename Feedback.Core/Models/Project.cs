using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Core.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ProjectToken { get; set; }
        public string Name { get; set; }
    }
}
