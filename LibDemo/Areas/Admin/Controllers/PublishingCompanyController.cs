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
    public class PublishingCompanyController : Controller
    {
        NXBDao nxb = new NXBDao();
        // GET: Admin/PublishingCompany
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                search = "";
            }
            ViewBag.lsttData = nxb.getAllDataView(search);
            ViewBag.Search = search;
            return View();
        }

        [HttpPost]
        public JsonResult SaveData(FormCollection f)
        {
            bool kt = true;
            long IDNV = long.Parse(Session["IDNV"].ToString());
            aNXB item = new aNXB();
            item.ID = int.Parse(f["ID"].ToString());
            item.TenNXB = f["NXB"].ToString();
            item.TrangThai = byte.Parse(f["TrangThai"].ToString());
            if (item.ID == 0)
            {
                kt = nxb.Insert(item, IDNV);
            }
            else
            {
                kt = nxb.Update(item, IDNV);
            }
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData(int id)
        {
            aNXB item = nxb.getDataByID(id);
            return Json(new
            {
                status = true,
                data = item
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = nxb.Delete(id, IDNV);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCount(int id)
        {
            long kt = nxb.getCountMCB(id);
            return Json(new
            {
                status = true,
                count = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Change(int id, byte status)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            if (status == 1)
            {
                bool kt = nxb.ChangeStatus(id, 2, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool kt = nxb.ChangeStatus(id, 1, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}