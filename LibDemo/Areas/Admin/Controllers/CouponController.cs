using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Dao.DaoAdmin;
using Models.EF;
using Models.ModelsView;

namespace LibDemo.Areas.Admin.Controllers
{
    public class CouponController : Controller
    {
        PhieuMuon_Dao pm = new PhieuMuon_Dao();
        // GET: Admin/Coupon
        public ActionResult Index()
        {
            int IDDonVi = int.Parse(Session["IDDonVi"].ToString());
            List<cPhieuMuonView> lstpm = pm.getAllDataView(IDDonVi, "");
            ViewBag.lstPM = lstpm;
            return View();
        }

        public JsonResult Delete(int id)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = pm.Delete(id, IDNV);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateDate(long id)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = pm.UpdateDate(id, IDNV);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Confirm(long id, byte type)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = pm.ChangeStatus(id, type, IDNV);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }
    }
}