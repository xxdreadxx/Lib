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
    public class MagazineController : Controller
    {
        AnPhamDao ap = new AnPhamDao();
        DonViDao dv = new DonViDao();
        NXBDao nxb = new NXBDao();
        MCBDao mcb = new MCBDao();
        // GET: Magazine
        public ActionResult Index(string search, string fromdate, string todate, int IDNXB = 0, int page = 1)
        {
            if (search == null)
            {
                search = "";
            }
            if (fromdate == null)
            {
                fromdate = "";
            }
            if (todate == null)
            {
                todate = "";
            }
            var lsrAP = ap.getAll1(3, IDNXB, search, fromdate, todate).ToList();
            int slAP = lsrAP.Count();
            int pageCount = slAP % 12 == 0 ? slAP / 12 : (slAP / 12) + 1;
            ViewBag.lstAP = lsrAP.Skip(12 * (page - 1)).Take(12).ToList();
            ViewBag.lstNXB = nxb.getAllDataView("");
            Session["fromdate"] = fromdate;
            Session["search"] = search;
            Session["todate"] = todate;
            Session["IDNXB"] = IDNXB;
            Session["page"] = page;
            Session["pageCount"] = pageCount;
            return View();
        }

        public ActionResult Detail(long id)
        {
            ViewBag.Item = ap.getDataViewByID1(id);
            ViewBag.LstMCB = mcb.getAllDataViewClient(id).OrderByDescending(x => x.DonVi).ToList();
            return View();
        }
    }
}