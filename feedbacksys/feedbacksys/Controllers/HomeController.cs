using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using feedbacksys.Domain;
using feedbacksys.Domain.Repository;

namespace feedbacksys.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var repository = new ImageRepository();
            repository.Create(new Image() {comment = "comment", email = "email", img = "img", title = "title"});
            return View();
        }
        public ActionResult List()
        {
            var repository = new ImageRepository();
            var models = repository.All().ToList();
            return View(models);

        }
        
    }
}
