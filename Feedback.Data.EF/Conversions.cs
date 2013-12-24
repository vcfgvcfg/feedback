using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = Feedback.Core.Models;
using DBContext = Feedback.Data.EF.DBContext;

namespace Feedback.Data.EF
{
    public static class Conversions
    {
        //public static DBContext.EFUser ToEFUser(this Model.ApplicationUser user)
        //{
        //    Feedback.Data.EF.DBContext.EFUser efUser = new DBContext.EFUser() 
        //    {
                
        //    };

        //    return efUser;
        //}

        public static Feedback.Core.Models.ApplicationUser ToAppUser(this Feedback.Data.EF.DBContext.EFUser user)
        {
            Model.ApplicationUser appUser = new Model.ApplicationUser()
            {
                Id = user.Id,
                UserName = user.UserName
            };

            return appUser;
        }
    }
}
