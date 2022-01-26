using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibDemo.Controllers
{
    public class LibraryListController : Controller
    {
        // GET: LibraryList
        public ActionResult Index()
        {
            return View();
        }
    }
}