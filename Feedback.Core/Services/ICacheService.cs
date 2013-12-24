using Feedback.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Core.Services
{
    public interface ICacheService
    {
        Project GetProject(int id);
        void SetProject(int id, Project p);
    }
}
