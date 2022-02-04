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
    public class BookController : Controller
    {
        AnPhamDao ap = new AnPhamDao();
        DonViDao dv = new DonViDao();
        PhanLoaiDao pl = new PhanLoaiDao();
        TacGiaDao tg = new TacGiaDao();
        // GET: Book
        public ActionResult Index(string search, int page = 1, long IDTG = 0, int IDPL=0)
        {
            if (search == null)
            {
                search = "";
            }
            var lsrAP = ap.getAll(search, IDTG, IDPL).ToList();
            int slAP = lsrAP.Count();
            int pageCount = slAP % 12 == 0 ? slAP / 12 : (slAP / 12) + 1;
            ViewBag.lstAP = lsrAP.Skip(12 * (page - 1)).Take(12).ToList();
            ViewBag.lstTG = tg.getAllDataView("");
            ViewBag.lstPL = pl.getAllDataView("");
            Session["IDPL"] = IDPL;
            Session["search"] = search;
            Session["IDTG"] = IDTG;
            Session["page"] = page;
            Session["pageCount"] = pageCount;
            return View();
        }

        public ActionResult Detail(long id)
        {
            return View();
        }
    }
}