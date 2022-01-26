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
    public class PortalLibController : Controller
    {
        DonViDao dv = new DonViDao();
        // GET: Admin/PortalLib
        public ActionResult Index()
        {
            ViewBag.lsttData = dv.getAllDataView();
            return View();
        }

        [HttpPost]
        public JsonResult SaveData(FormCollection f)
        {
            bool kt = true;
            long IDNV = long.Parse(Session["IDNV"].ToString());
            sDonVi item = new sDonVi();
            item.ID = int.Parse(f["ID"].ToString());
            item.TenDonVi = f["MaDonVi"].ToString();
            item.MaDonVi = f["TenDonVi"].ToString();
            item.Url = f["Url"].ToString();
            item.TrangThai = byte.Parse(f["check"].ToString());
            if (item.ID == 0)
            {
                kt = dv.InsertDonVi(item);
            }
            else
            {
                kt = dv.UpdateDonVi(item, IDNV);
            }
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData(int id)
        {
            sDonVi item = dv.getDataByID(id);
            return Json(new
            {
                status = true,
                data = item
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = dv.DeleteDonVi(id, IDNV);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Change(int id, byte status)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = dv.ChangeStatus(id, status, IDNV);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }
    }
}