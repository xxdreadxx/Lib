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
    public class AuthorController : Controller
    {
        TacGiaDao tg = new TacGiaDao();
        // GET: Admin/Author
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                search = "";
            }
            ViewBag.lsttData = tg.getAllDataView(search);
            ViewBag.Search = search;
            return View();
        }

        [HttpPost]
        public JsonResult SaveData(FormCollection f)
        {
            HttpFileCollectionBase file = Request.Files;
            bool kt = true;
            long IDNV = long.Parse(Session["IDNV"].ToString());
            aTacGia item = new aTacGia();
            item.ID = int.Parse(f["ID"].ToString());
            item.HoTen = f["HoTen"].ToString();
            item.DiaChi = f["DiaChi"].ToString();
            item.GoiThieu = f["GioiThieu"].ToString();
            item.MaTG = f["MaTG"].ToString();
            item.NgaySinh = f["NgaySinh"].ToString();
            item.NgayMat = f["NgayMat"].ToString();
            if (file.Count > 0)
            {
                if (file[0].ContentLength > 0)
                {
                    string pathFolder = "/assets/Images/Avatars/Author/";
                    Directory.CreateDirectory(Server.MapPath(pathFolder));
                    string pathFile = Path.Combine(Server.MapPath(pathFolder), file[0].FileName);
                    file[0].SaveAs(pathFile);
                    item.AnhDaiDien = pathFolder + "/" + file[0].FileName;
                }
            }
            item.TrangThai = byte.Parse(f["TrangThai"].ToString());
            if (item.ID == 0)
            {
                kt = tg.Insert(item, IDNV);
            }
            else
            {
                kt = tg.Update(item, IDNV);
            }
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData(int id)
        {
            aTacGia item = tg.getDataByID(id);
            return Json(new
            {
                status = true,
                data = item
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = tg.Delete(id, IDNV);
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
                bool kt = tg.ChangeStatus(id, 2, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool kt = tg.ChangeStatus(id, 1, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}