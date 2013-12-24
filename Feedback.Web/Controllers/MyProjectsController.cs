using Feedback.Core.Services;
using Feedback.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Feedback.Web.Controllers
{
    [Authorize]
    public class MyProjectsController : FeedbackControllerBase
    {
        private IProjectService ProjectService;

        public MyProjectsController(IUserService userService, IProjectService projectService)
            : base(userService)
        {
            ProjectService = projectService;
        }

        //
        // GET: /MyProjects/
        public ActionResult Index()
        {
            return View(ProjectService.GetProjectsByUser(AppUser.Id));
        }

        //
        // GET: /MyProjects/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MyProjects/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MyProjects/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MyProjects/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MyProjects/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MyProjects/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MyProjects/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
