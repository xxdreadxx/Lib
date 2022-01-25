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
    public class HomeController : Controller
    {
        DonViDao dv = new DonViDao();
        NhanVienDao nv = new NhanVienDao();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["IDNV"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            long IDNV = long.Parse(Session["IDNV"].ToString());
            int IDDV = int.Parse(Session["IDDonVi"].ToString());
            ViewBag.UserInfo = nv.getDataByID(IDNV);
            ViewBag.DonViInfo = dv.getDataByID(IDDV);
            return View();
        }
    }
}