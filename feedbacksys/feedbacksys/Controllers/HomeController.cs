using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using feedbacksys.Domain.Repository;

namespace feedbacksys.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult List()
        {
            var repository = new ImageRepository();
            var models = repository.All().ToList();
            return View(models);

        }

        public ActionResult Detail(int id)
        {
            var repository = new ImageRepository();
            var model = repository.Find(r => r.id == id);
            return View(model);
        }
        
    }
}
