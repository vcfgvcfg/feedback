using System;
using System.Web.WebPages;
using LiteReg.Core.Domain.Managers;
using LiteReg.Core.Services;
using Domain = LiteReg.Core.Domain;
using Data = LiteReg.Core.Data;
using System.Security.Principal;
using System.Web.Http;

namespace LiteReg.Web.Infrastructure
{
    /// <summary>
    /// Provides base functionality for API controllers.
    /// </summary>
    public class ApiControllerBase : ApiController
    {
        protected Data.IUserRepository UserRepository { get; set; }

        /// <summary>
        /// The currently logged-in user.
        /// </summary>
        protected new Domain.User User { get; set; }

        /// <summary>
        /// Provides globalization functionality.
        /// </summary>
        public IGlobalizationManager GlobalizationManager { get; set; }

        /// <summary>
        /// Constructor; loads the currently logged-in user, if needed.
        /// </summary>
        public ApiControllerBase(IPrincipal user, Data.IUserRepository userRepository,
            IGlobalizationManager globalizationManager, Domain.User preLoadedUser = null)
        {
            UserRepository = userRepository;
            GlobalizationManager = globalizationManager;

            // load logged-in user, if needed
            if (preLoadedUser != null && preLoadedUser.ID > 0)
            {
                User = preLoadedUser;
            }
            else
            {
                User = GetAuthenticatedUser(user, UserRepository);
            }
        }

        /// <summary>
        /// Returns the currently logged-in user corresponding to the given IPrincipal;
        /// returns null if user is not found or is not authenticated.
        /// </summary>
        public static Domain.User GetAuthenticatedUser(IPrincipal user,
            Data.IUserRepository userRepository)
        {
            Domain.User u = null;
            if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
            {
                int userID;
                var username = user.Identity.Name.Split('-');
                if (username.Length > 1)
                {
                    u = Domain.User.GetImpersonateUser(user.Identity.Name, userRepository);
                    u.AdminID = Int32.Parse(username[1]);
                }
                else if (int.TryParse(username[0], out userID))
                {
                    u = Domain.User.Get(userID, userRepository);
                }

            }
            return u;
        }

        /// <summary>
        /// Get the current client ip address
        /// </summary>
        protected string ClientIPAddress
        {
            get
            {
                //the request is not initialized when the api controller is created manually.
                if (Request != null)
                {
                    var httpContext = Request.Properties["MS_HttpContext"] as System.Web.HttpContextWrapper;
                    if (httpContext != null)
                    {
                        return IPAddressChecker.GetClientIPAddress(httpContext.Request);
                    }
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Checks if the Request is from a mobile device
        /// </summary>
        protected bool IsMobile
        {
            get
            {
                //the request is not initialized when the api controller is created manually.
                if (Request != null)
                {
                    var httpContext = Request.Properties["MS_HttpContext"] as System.Web.HttpContextWrapper;
                    if (httpContext != null)
                    {
                        return httpContext.GetOverriddenBrowser().IsMobileDevice;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Returns a ModelValidator for the current context.
        /// </summary>
        protected ModelValidator GetModelValidator()
        {
            // call default model validator's Validate method
            var resolver = Configuration.Services;
            var validator = resolver.GetBodyModelValidator();
            var modelMetadataProvider = resolver.GetModelMetadataProvider();
            var controllerDescriptor = ControllerContext.ControllerDescriptor;
            var actionDescriptor = controllerDescriptor.HttpActionSelector.SelectAction(ControllerContext);
            var actionContext = new System.Web.Http.Controllers.HttpActionContext(ControllerContext, actionDescriptor);
            return new ModelValidator(validator, modelMetadataProvider, actionContext);
        }
    }
}