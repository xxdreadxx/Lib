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
    public class LibrarianController : Controller
    {
        NhanVienDao nv = new NhanVienDao();
        // GET: Admin/Librarian
        public ActionResult Index(string search)
        {
            int IDDonVi = int.Parse(Session["IDDonVi"].ToString());
            if (search == null)
            {
                search = "";
            }
            ViewBag.lsttData = nv.getAllDataView(IDDonVi, search);
            ViewBag.Search = search;
            return View();
        }

        [HttpPost]
        public JsonResult SaveData(FormCollection f)
        {
            HttpFileCollectionBase file = Request.Files;
            bool kt = true;
            int IDDonVi = int.Parse(Session["IDDonVi"].ToString());
            long IDNV = long.Parse(Session["IDNV"].ToString());
            sNhanVien item = new sNhanVien();
            item.ID = int.Parse(f["ID"].ToString());
            item.HoTen = f["HoTen"].ToString();
            item.CMTND = f["CMT"].ToString();
            item.DiaChi = f["DiaChi"].ToString();
            item.Email = f["Email"].ToString();
            item.IDDonVi = IDDonVi;
            item.NgaySinh = f["NgaySinh"].ToString();
            item.SDT = int.Parse(f["SDT"].ToString());
            item.Quyen = byte.Parse(f["Quyen"].ToString());
            item.Username = f["Username"].ToString();
            item.TrangThai = byte.Parse(f["TrangThai"].ToString());
            if (file.Count > 0)
            {
                if (file[0].ContentLength > 0)
                {
                    string pathFolder = "/assets/Images/Avatars/Users/" + IDDonVi.ToString();
                    Directory.CreateDirectory(Server.MapPath(pathFolder));
                    string pathFile = Path.Combine(Server.MapPath(pathFolder), file[0].FileName);
                    file[0].SaveAs(pathFile);
                    item.AnhDaiDien = pathFolder + "/" + file[0].FileName;
                }
            }
            if (item.ID == 0)
            {
                kt = nv.Insert(item, IDNV);
            }
            else
            {
                kt = nv.Update(item, IDNV);
            }
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData(long id)
        {
            sNhanVien item = nv.getDataByID(id);
            return Json(new
            {
                status = true,
                data = item
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = nv.Delete(id, IDNV);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Change(int id, byte status)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            if (status == 1)
            {
                bool kt = nv.ChangeStatus(id, 2, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool kt = nv.ChangeStatus(id, 1, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}