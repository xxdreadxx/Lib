using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.EF;
using Models.ModelsView;
using Models.Dao.DaoAdmin;

namespace LibDemo.Controllers
{
    public class HomeController : Controller
    {
        DonViDao dv = new DonViDao();
        AnPhamDao ap = new AnPhamDao();
        PhieuMuon_Dao pm = new PhieuMuon_Dao();
        BanDocDao bd = new BanDocDao();
        TacGiaDao tg = new TacGiaDao();
        // GET: Home
        public ActionResult Index()
        {
            string host = System.Web.HttpContext.Current.Request.Url.Host;
            var donvi = dv.getDataByHost(host);
            Session["IDDonVi"] = donvi.ID;
            ViewBag.lstLib = dv.getAllDataView("");
            ViewBag.lstAP8 = ap.getAllData(donvi.ID).OrderByDescending(x=>x.NgayTao).Take(8).ToList();
            ViewBag.LSAP = ap.SLAP();
            ViewBag.SLPM = pm.SLPhieuMuon();
            ViewBag.SLBD = bd.SLBanDoc();
            ViewBag.lstTG4 = tg.get4TacGia();
            return View();
        }
    }
}