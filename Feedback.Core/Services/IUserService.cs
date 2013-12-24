using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Feedback.Core.Models;
using Feedback.Core.Utilities;

namespace Feedback.Core.Services
{
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        Task<ApplicationUser> FindAsync(string p1, string p2);

        ClaimsIdentity CreateIdentity(ApplicationUser user, string p);

        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string p);

        ApplicationUser Find(string userName, string password);

        ApplicationUser FindByName(string userName);

        FuncResult Create(ApplicationUser user, string p);
    }
}

