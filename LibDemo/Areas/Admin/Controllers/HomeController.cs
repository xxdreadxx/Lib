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
            var nhanvien = nv.getDataByID(IDNV);
            var donvi = dv.getDataByID(IDDV);
            Session["User_Ten"] = nhanvien.HoTen;
            Session["User_Quyen"] = nhanvien.Quyen;
            Session["User_NgayTao"] = nhanvien.NgayTao.GetValueOrDefault().ToString("dd/MM/yyyy");
            Session["User_Avatar"] = nhanvien.AnhDaiDien;
            Session["Portal_Ten"] = donvi.TenDonVi;
            return View();
        }
    }
}