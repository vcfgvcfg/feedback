using Feedback.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Data.EF.DBContext
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class EFUser : IdentityUser
    {

    }

    public class UserDbContext : IdentityDbContext<EFUser>
    {
        public UserDbContext()
            : base("UserDbConnection")
        {
        }
    }
}
