using Feedback.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Data.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindAsync(string p1, string p2);

        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string p);

        ApplicationUser Find(string userName, string password);

        ApplicationUser Find(string userName);

        ClaimsIdentity CreateIdentity(ApplicationUser user, string p);

        Core.Utilities.FuncResult Create(ApplicationUser user, string p);
    }
}
