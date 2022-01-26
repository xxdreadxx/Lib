using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Dao.DaoAdmin;
using Models.EF;

namespace LibDemo.Areas.Admin.Controllers
{
    public class PortalLibController : Controller
    {
        DonViDao dv = new DonViDao();
        // GET: Admin/PortalLib
        public ActionResult Index()
        {
            ViewBag.lsttData = dv.getAllDataView();
            return View();
        }
    }
}