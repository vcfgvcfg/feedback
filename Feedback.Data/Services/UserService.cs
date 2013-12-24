using Feedback.Core.Services;
using Feedback.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feedback.Core.Models;
using System.Security.Claims;

namespace Feedback.Data.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Task<ApplicationUser> FindAsync(string p1, string p2)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsIdentity> CreateIdentityAsync(Core.Models.ApplicationUser user, string p)
        {
            var identity = userRepository.CreateIdentityAsync(user, p);

            return identity;
        }

        public ApplicationUser Find(string userName, string password)
        {
            return this.userRepository.Find(userName, password);
        }

        public ApplicationUser FindByName(string userName)
        {
            return this.userRepository.Find(userName);
        }

        public ClaimsIdentity CreateIdentity(ApplicationUser user, string p)
        {
            var identity = userRepository.CreateIdentity(user, p);

            return identity;
        }


        public Core.Utilities.FuncResult Create(ApplicationUser user, string p)
        {
            return userRepository.Create(user, p);
        }
    }
}
