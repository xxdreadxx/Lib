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
    public class ReaderController : Controller
    {
        BanDocDao bd = new BanDocDao();
        // GET: Admin/Author
        public ActionResult Index(string search)
        {
            int IDDonVi = int.Parse(Session["IDDonVi"].ToString());
            if (search == null)
            {
                search = "";
            }
            ViewBag.lsttData = bd.getAllDataView(IDDonVi,search);
            ViewBag.Search = search;
            return View();
        }

        public JsonResult UpdateDate(long id)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = bd.UpdateDate(id, IDNV);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult SaveData(FormCollection f)
        //{
        //    int IDDonVi = int.Parse(Session["IDDonVi"].ToString());
        //    HttpFileCollectionBase file = Request.Files;
        //    bool kt = true;
        //    long IDNV = long.Parse(Session["IDNV"].ToString());
        //    cBanDoc item = new cBanDoc();
        //    item.ID = int.Parse(f["ID"].ToString());
        //    item.IDDonVi = IDDonVi;
        //    item.HoTen = f["HoTen"].ToString();
        //    item.DiaChi = f["DiaChi"].ToString();
        //    item.CMTND = f["GioiThieu"].ToString();
        //    item.Email = f["MaTG"].ToString();
        //    item.NgaySinh = f["NgaySinh"].ToString();
        //    item. = f["NgayMat"].ToString();
        //    if (file.Count > 0)
        //    {
        //        if (file[0].ContentLength > 0)
        //        {
        //            string pathFolder = "/assets/Images/Avatars/Author/";
        //            Directory.CreateDirectory(Server.MapPath(pathFolder));
        //            string pathFile = Path.Combine(Server.MapPath(pathFolder), file[0].FileName);
        //            file[0].SaveAs(pathFile);
        //            item.AnhDaiDien = pathFolder + "/" + file[0].FileName;
        //        }
        //    }
        //    item.TrangThai = byte.Parse(f["TrangThai"].ToString());
        //    if (item.ID == 0)
        //    {
        //        kt = bd.Insert(item, IDNV);
        //    }
        //    else
        //    {
        //        kt = bd.Update(item, IDNV);
        //    }
        //    return Json(new
        //    {
        //        status = kt
        //    }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetData(int id)
        {
            cBanDoc item = bd.getDataByID(id);
            return Json(new
            {
                status = true,
                data = item
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = bd.Delete(id, IDNV);
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
                bool kt = bd.ChangeStatus(id, 2, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool kt = bd.ChangeStatus(id, 1, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}