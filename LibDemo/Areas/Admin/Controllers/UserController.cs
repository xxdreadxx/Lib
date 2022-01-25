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
    public class UserController : Controller
    {
        NhanVienDao nv = new NhanVienDao();
        // GET: Admin/User
        public ActionResult Index()
        {
            if (Session["IDNV"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                long IDNV = long.Parse(Session["IDNV"].ToString());
                ViewBag.UserInfo = nv.getDataByID(IDNV);
                return View();
            }
        }
    }
}