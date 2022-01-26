using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibDemo.Controllers
{
    public class NewspaperController : Controller
    {
        // GET: Newspaper
        public ActionResult Index()
        {
            return View();
        }
    }
}