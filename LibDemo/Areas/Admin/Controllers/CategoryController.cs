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
    public class CategoryController : Controller
    {
        PhanLoaiDao pl = new PhanLoaiDao();
        // GET: Admin/Category
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                search = "";
            }
            ViewBag.lsttData = pl.getAllDataView(search);
            ViewBag.Search = search;
            return View();
        }

        [HttpPost]
        public JsonResult SaveData(FormCollection f)
        {
            bool kt = true;
            long IDNV = long.Parse(Session["IDNV"].ToString());
            aPhanLoaiAP item = new aPhanLoaiAP();
            item.ID = int.Parse(f["ID"].ToString());
            item.TenPhanLoaiAP = f["TenPL"].ToString();
            item.TrangThai = byte.Parse(f["TrangThai"].ToString());
            if (item.ID == 0)
            {
                kt = pl.Insert(item, IDNV);
            }
            else
            {
                kt = pl.Update(item, IDNV);
            }
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData(int id)
        {
            aPhanLoaiAP item = pl.getDataByID(id);
            return Json(new
            {
                status = true,
                data = item
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = pl.Delete(id, IDNV);
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
                bool kt = pl.ChangeStatus(id, 2, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool kt = pl.ChangeStatus(id, 1, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}