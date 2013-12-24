using Feedback.Core.Models;
using Feedback.Core.Services;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Feedback.Web.Infrastructure
{
    /// <summary>
    /// Provides base functionality for MVC controllers.
    /// </summary>
    public class FeedbackControllerBase : Controller
    {
        protected IUserService UserService { get; set; }

        /// <summary>
        /// The currently logged-in user.
        /// </summary>
        protected ApplicationUser AppUser { get; set; }

        /// <summary>
        /// Constructor; loads the currently logged-in user, if needed, and sets the user
        /// information in ViewBag.User.
        /// </summary>
        public FeedbackControllerBase(IUserService userService)
        {
            this.UserService = userService;
            SetUser();
        }

        private void SetUser()
        {
            IPrincipal user = System.Web.HttpContext.Current.User;
            if (user != null)
            {
                string s = user.Identity.Name;
                this.AppUser = this.UserService.FindByName(s);
                ViewBag.AppUser = this.AppUser;
            }
        }
    }
}