using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost]
        public JsonResult UpdateInfo(FormCollection f)
        {
            sNhanVien item = new sNhanVien();
            HttpFileCollectionBase file = Request.Files;
            item.ID = long.Parse(f["ID"].ToString());
            item.HoTen  = f["HoTen"].ToString();
            item.NgaySinh = f["NgaySinh"].ToString();
            item.CMTND = f["CMT"].ToString();
            item.DiaChi = f["DiaChi"].ToString();
            item.SDT = int.Parse(f["SDT"].ToString());
            item.Email = f["Email"].ToString();
            if (file != null)
            {
                if (file[0].ContentLength > 0)
                {
                    string pathFolder = "/assets/Images/Avatars/Users/" + Session["IDDonVi"].ToString();
                    Directory.CreateDirectory(Server.MapPath(pathFolder));
                    string pathFile = Path.Combine(Server.MapPath(pathFolder), file[0].FileName);
                    file[0].SaveAs(pathFile);
                    item.AnhDaiDien = pathFolder + "/" + file[0].FileName;
                }
            }
            bool kt = nv.Update(item, item.ID);
            if (kt == true)
            {
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdatePass(FormCollection f)
        {
            long ID = long.Parse(f["ID"].ToString());
            string Password = f["NewPass"].ToString().Trim();
            byte kt = nv.UpdatePass(Password, ID);
            if (kt == 0)
            {
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = false,
                    type = kt
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}