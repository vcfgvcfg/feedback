using Feedback.Data.EF.DBContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Data.EF
{
    public class UserRepository : Repositories.IUserRepository
    {
        private UserManager<EFUser> identityUserManager;
        
        public UserRepository()
        {
            identityUserManager = new UserManager<EFUser>(new UserStore<EFUser>(new UserDbContext()));
        }

        public Task<Core.Models.ApplicationUser> FindAsync(string userName, string password)
        {
            Task<EFUser> efUser = identityUserManager.FindAsync(userName, password);
            efUser.ContinueWith(ef => ef.Result);
            TaskCompletionSource<Core.Models.ApplicationUser> tcs = new TaskCompletionSource<Core.Models.ApplicationUser>();
            efUser.ContinueWith(t => tcs.SetResult(new Core.Models.ApplicationUser() { Id = t.Result.Id, UserName = t.Result.UserName }), TaskContinuationOptions.OnlyOnRanToCompletion);
            efUser.ContinueWith(t => tcs.SetException(t.Exception.InnerExceptions), TaskContinuationOptions.OnlyOnFaulted);
            efUser.ContinueWith(t => tcs.SetCanceled(), TaskContinuationOptions.OnlyOnCanceled);
            return tcs.Task;
        }

        public Task<System.Security.Claims.ClaimsIdentity> CreateIdentityAsync(Core.Models.ApplicationUser user, string p)
        {
            throw new NotImplementedException();
        }

        public Core.Models.ApplicationUser Find(string userName, string password)
        {
            EFUser efUser = this.identityUserManager.Find(userName, password);
            if (efUser == null) 
            {
                return null;
            }
            return efUser.ToAppUser();
        }

        public Core.Models.ApplicationUser Find(string userName)
        {
            EFUser efUser = this.identityUserManager.FindByName(userName);
            if (efUser == null)
            {
                return null;
            }
            return efUser.ToAppUser();
        }

        public System.Security.Claims.ClaimsIdentity CreateIdentity(Core.Models.ApplicationUser user, string p)
        {
            return this.identityUserManager.CreateIdentity(new EFUser() { Id = user.Id, UserName = user.UserName }, p);
        }

        public Core.Utilities.FuncResult Create(Core.Models.ApplicationUser user, string p)
        {
            var efUser = new EFUser() { UserName = user.UserName };
            var identityResult = this.identityUserManager.Create(efUser, p);
            user.Id = efUser.Id;
            return new Core.Utilities.FuncResult() { Succeeded = identityResult.Succeeded, Errors = identityResult.Errors };
        }
    }
}
