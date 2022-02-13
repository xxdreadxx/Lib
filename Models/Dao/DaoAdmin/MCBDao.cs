using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Models.ModelsView;

namespace Models.Dao.DaoAdmin
{
    public class MCBDao
    {
        LibDbContext db = new LibDbContext();

        public cMCB getDataByID(long ID)
        {
            return db.cMCBs.FirstOrDefault(x => x.ID == ID);
        }

        public cMCBView getDataViewByID(long ID)
        {
            cMCBView item = new cMCBView();
            item = (from ds in db.cMCBs
                    join ap in db.cAnPhams on ds.IDAnPam equals ap.ID
                    join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                    where ds.ID == ID
                    select new cMCBView
                    {
                        ID = ds.ID,
                        DonVi = dv.TenDonVi,
                        DonViHienTai = "",
                        NhanDe = ap.NhanDe,
                        IDAnPam = ds.IDAnPam,
                        IDDonVi = ds.IDDonVi,
                        IDDonVi_HienTai = ds.IDDonVi_HienTai,
                        MCB = ds.MCB
                    }).FirstOrDefault();
            return item;
        }

        public List<cMCBView> getAllDataView(long IDAP, string search)
        {
            List<cMCBView> item = new List<cMCBView>();
            if (search == "")
            {
                item = (from ds in db.cMCBs
                        join ap in db.cAnPhams on ds.IDAnPam equals ap.ID
                        join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                        join dvht in db.sDonVis on ds.IDDonVi_HienTai equals dvht.ID
                        where ds.TrangThai != 10 && ap.ID == IDAP
                        select new cMCBView
                        {
                            ID = ds.ID,
                            DonVi = dv.TenDonVi,
                            DonViHienTai = dvht.TenDonVi,
                            NhanDe = ap.NhanDe,
                            IDAnPam = ds.IDAnPam,
                            IDDonVi = ds.IDDonVi,
                            IDDonVi_HienTai = ds.IDDonVi_HienTai,
                            MCB = ds.MCB
                        }).ToList();
            }
            else
            {
                item = (from ds in db.cMCBs
                        join ap in db.cAnPhams on ds.IDAnPam equals ap.ID
                        join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                        join dvht in db.sDonVis on ds.IDDonVi_HienTai equals dvht.ID
                        where ds.TrangThai != 10 && ds.MCB.Contains(search) && ap.ID == IDAP
                        select new cMCBView
                        {
                            ID = ds.ID,
                            DonVi = dv.TenDonVi,
                            DonViHienTai = dvht.TenDonVi,
                            NhanDe = ap.NhanDe,
                            IDAnPam = ds.IDAnPam,
                            IDDonVi = ds.IDDonVi,
                            IDDonVi_HienTai = ds.IDDonVi_HienTai,
                            MCB = ds.MCB
                        }).ToList();
            }
            return item;
        }

        public List<cMCBView> getAllDataViewClient(long IDAP)
        {
            List<cMCBView> item = new List<cMCBView>();
            item = (from ds in db.cMCBs
                    join ap in db.cAnPhams on ds.IDAnPam equals ap.ID
                    join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                    join dvht in db.sDonVis on ds.IDDonVi_HienTai equals dvht.ID
                    where (ds.TrangThai == 1 || ds.TrangThai == 2) && ap.ID == IDAP
                    select new cMCBView
                    {
                        ID = ds.ID,
                        DonVi = dv.TenDonVi,
                        DonViHienTai = dvht.TenDonVi,
                        NhanDe = ap.NhanDe,
                        IDAnPam = ds.IDAnPam,
                        IDDonVi = ds.IDDonVi,
                        IDDonVi_HienTai = ds.IDDonVi_HienTai,
                        TrangThai = ds.TrangThai,
                        MCB = ds.MCB
                    }).ToList();
            return item;
        }

        public bool Insert(long IDAP, long IDNV, int IDDonVi, int SL)
        {
            try
            {
                var maxMCB = db.cMCBs.Where(x => x.IDAnPam == IDAP && x.TrangThai != 10 && x.IDDonVi == IDDonVi).ToList();
                var donvi = db.sDonVis.FirstOrDefault(x => x.ID == IDDonVi);
                var anpham = db.cAnPhams.FirstOrDefault(x => x.ID == IDAP);
                if (maxMCB.Count == 0)
                {
                    for (int i = 0; i < SL; i++)
                    {
                        string mcb = "";
                        if (i + 1 < 10)
                        {
                            mcb = donvi.MaDonVi + "_" + anpham.MaAnPham + "_" + "000" + (i + 1).ToString();
                        }
                        else if (i + 1 < 100)
                        {
                            mcb = donvi.MaDonVi + "_" + anpham.MaAnPham + "_" + "00" + (i + 1).ToString();
                        }
                        else if (i + 1 < 1000)
                        {
                            mcb = donvi.MaDonVi + "_" + anpham.MaAnPham + "_" + "0" + (i + 1).ToString();
                        }
                        else
                        {
                            mcb = donvi.MaDonVi + "_" + anpham.MaAnPham + "_" + (i + 1).ToString();
                        }
                        cMCB result = new cMCB();
                        result.IDAnPam = IDAP;
                        result.IDDonVi = IDDonVi;
                        result.IDDonVi_HienTai = IDDonVi;
                        result.KieuAP = 1;
                        result.MCB = mcb;
                        result.MCBIndex = i + 1;
                        result.TrangThai = 1;
                        result.NguoiTao = IDNV;
                        result.NgayTao = DateTime.Now;
                        db.cMCBs.Add(result);
                        db.SaveChanges();
                    }
                }
                else
                {
                    maxMCB = maxMCB.OrderByDescending(x => x.MCBIndex).ToList();
                    for (int i = maxMCB[0].MCBIndex; i < maxMCB[0].MCBIndex + SL; i++)
                    {
                        string mcb = "";
                        if (i + 1 < 10)
                        {
                            mcb = donvi.MaDonVi + "_" + anpham.MaAnPham + "_" + "000" + (i + 1).ToString();
                        }
                        else if (i + 1 < 100)
                        {
                            mcb = donvi.MaDonVi + "_" + anpham.MaAnPham + "_" + "00" + (i + 1).ToString();
                        }
                        else if (i + 1 < 1000)
                        {
                            mcb = donvi.MaDonVi + "_" + anpham.MaAnPham + "_" + "0" + (i + 1).ToString();
                        }
                        else
                        {
                            mcb = donvi.MaDonVi + "_" + anpham.MaAnPham + "_" + (i + 1).ToString();
                        }
                        cMCB result = new cMCB();
                        result.IDAnPam = IDAP;
                        result.IDDonVi = IDDonVi;
                        result.IDDonVi_HienTai = IDDonVi;
                        result.KieuAP = 1;
                        result.MCB = mcb;
                        result.MCBIndex = i + 1;
                        result.TrangThai = 1;
                        result.NguoiTao = IDNV;
                        result.NgayTao = DateTime.Now;
                        db.cMCBs.Add(result);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Update(cMCB result, long IDNV)
        {
            try
            {
                cMCB item = db.cMCBs.FirstOrDefault(x => x.ID == result.ID);
                item.MCB = result.MCB;
                item.NgaySua = DateTime.Now;
                item.NguoiSua = IDNV;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(long ID, long IDNV)
        {
            try
            {
                cMCB item = db.cMCBs.FirstOrDefault(x => x.ID == ID);
                item.TrangThai = 10;
                item.NgaySua = DateTime.Now;
                item.NguoiSua = IDNV;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangePortal(long ID, int IDDV, long IDNV)
        {
            try
            {
                cMCB item = db.cMCBs.FirstOrDefault(x => x.ID == ID);
                item.IDDonVi_HienTai = IDDV;
                item.NgaySua = DateTime.Now;
                item.NguoiSua = IDNV;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}