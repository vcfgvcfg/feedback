using Feedback.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Data.EF.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("AppDbConnection")
        {

        }
        public DbSet<Project> Projects { get; set; }
    }
}
