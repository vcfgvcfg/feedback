using Feedback.Core.Services;
using Feedback.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Data.Services
{
    public class ProjectService: IProjectService
    {
        private IProjectRepository projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public IEnumerable<Core.Models.Project> GetProjectsByUser(string userId)
        {
            return this.projectRepository.Get(filter: project => project.UserId == userId);
        }
    }
}
