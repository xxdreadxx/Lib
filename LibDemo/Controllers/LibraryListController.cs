using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.EF;
using Models.ModelsView;
using Models.Dao.DaoAdmin;

namespace LibDemo.Controllers
{
    public class LibraryListController : Controller
    {
        DonViDao dv = new DonViDao();
        AnPhamDao ap = new AnPhamDao();
        // GET: LibraryList
        public ActionResult Index()
        {
            ViewBag.lstLib = dv.getAllDataView("");
            return View();
        }

        public ActionResult Detail(int id)
        {
            ViewBag.lstAP = ap.getAllData(id).OrderByDescending(x => x.NgayTao).ToList();
            return View();
        }
    }
}