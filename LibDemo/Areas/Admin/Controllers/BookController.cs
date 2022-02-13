using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Dao.DaoAdmin;
using Models.EF;

namespace LibDemo.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        AnPhamDao ap = new AnPhamDao();
        TacGiaDao tg = new TacGiaDao();
        NXBDao nxb = new NXBDao();
        PhanLoaiDao plap = new PhanLoaiDao();
        MCBDao mcb = new MCBDao();

        private static List<cMCB> excel = new List<cMCB>();
        // GET: Admin/Author
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                search = "";
            }
            ViewBag.lsttData = ap.getAllDataView(search);
            ViewBag.lstTG = tg.getAllDataView("");
            ViewBag.lstPLAP = plap.getAllDataView("");
            ViewBag.lstNXB = nxb.getAllDataView("");
            ViewBag.Search = search;
            return View();
        }

        [HttpPost]
        public JsonResult SaveData(FormCollection f)
        {
            HttpFileCollectionBase file = Request.Files;
            bool kt = true;
            long IDNV = long.Parse(Session["IDNV"].ToString());
            cAnPham item = new cAnPham();
            item.ID = int.Parse(f["ID"].ToString());
            item.DongTacGia = f["DongTG"].ToString();
            item.IDNXB = int.Parse(f["NXB"].ToString());
            item.GioiThieu = f["GioiThieu"].ToString();
            item.IDPLAP = int.Parse(f["PLAP"].ToString());
            item.IDTacGia = int.Parse(f["TG"].ToString());
            item.LKieuAP = byte.Parse(f["KieuAP"].ToString());
            item.MaAnPham = f["MaAP"].ToString();
            if(f["NgayXB"].ToString().Trim() != "")
            {
                item.NgayXuatBan = DateTime.ParseExact(f["NgayXB"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            item.So =f["So"].ToString();
            item.NhanDe = f["NhanDe"].ToString();
            if (file.Count > 0)
            {
                if (file[0].ContentLength > 0)
                {
                    string pathFolder = "/assets/Images/Books/";
                    Directory.CreateDirectory(Server.MapPath(pathFolder));
                    string pathFile = Path.Combine(Server.MapPath(pathFolder), file[0].FileName);
                    file[0].SaveAs(pathFile);
                    item.HinhAnh = pathFolder + "/" + file[0].FileName;
                }
            }
            item.TrangThai = byte.Parse(f["TrangThai"].ToString());
            if (item.ID == 0)
            {
                kt = ap.Insert(item, IDNV);
            }
            else
            {
                kt = ap.Update(item, IDNV);
            }
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData(int id)
        {
            cAnPham item = ap.getDataByID(id);
            var dataNXB = item.NgayXuatBan.GetValueOrDefault().ToString("dd/MM/yyyy");
            return Json(new
            {
                status = true,
                data = item,
                dataNXB =dataNXB
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = ap.Delete(id, IDNV);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCount(int id)
        {
            long kt = ap.GetCountMCB(id);
            return Json(new
            {
                status = true,
                data=kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Change(int id, byte status)
        {
            long IDNV = long.Parse(Session["IDNV"].ToString());
            if (status == 1)
            {
                bool kt = ap.ChangeStatus(id, 2, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool kt = ap.ChangeStatus(id, 1, IDNV);
                return Json(new
                {
                    status = kt
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMCB(long id)
        {
            int IDDonVi = int.Parse(Session["IDDonVi"].ToString());
            var lst = mcb.getAllDataView(id, "");
            lst = lst.Where(x => x.IDDonVi == IDDonVi || x.IDDonVi_HienTai == IDDonVi).ToList();
            return Json(new
            {
                status = true,
                data = lst
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddMCB(long id, int sl)
        {
            int IDDonVi = int.Parse(Session["IDDonVi"].ToString());
            long IDNV = long.Parse(Session["IDNV"].ToString());
            bool kt = mcb.Insert(id, IDNV, IDDonVi, sl);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }
    }
}