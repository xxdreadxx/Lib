using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Models.ModelsView;

namespace Models.Dao.DaoAdmin
{
    public class PhieuMuon_Dao
    {
        LibDbContext db = new LibDbContext();

        public cPhieuMuon getDataByID(long ID)
        {
            return db.cPhieuMuons.FirstOrDefault(x => x.ID == ID);
        }

        public cPhieuMuonView getDataViewByID(long ID)
        {
            cPhieuMuonView item = new cPhieuMuonView();
            item = (from ds in db.cPhieuMuons
                    join bd in db.cBanDocs on ds.IDBanDoc equals bd.ID
                    join mcb in db.cMCBs on ds.IDMCB equals mcb.ID
                    where ds.ID == ID
                    select new cPhieuMuonView
                    {
                        ID = ds.ID,
                        BanDoc = bd.HoTen,
                        IDBanDoc = ds.IDBanDoc,
                        IDMCB = ds.IDMCB,
                        MCB = mcb.MCB,
                        NgayHenTra = ds.NgayHenTra,
                        NgayMuon = ds.NgayMuon,
                        NgayTra = ds.NgayTra,
                        TrangThai = ds.TrangThai
                    }).FirstOrDefault();
            return item;
        }

        public List<cPhieuMuonView> getListDataViewByIDBanDoc(long ID)
        {
            List<cPhieuMuonView> item = new List<cPhieuMuonView>();
            item = (from ds in db.cPhieuMuons
                    join bd in db.cBanDocs on ds.IDBanDoc equals bd.ID
                    join mcb in db.cMCBs on ds.IDMCB equals mcb.ID
                    join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                    join ap in db.cAnPhams on mcb.IDAnPam equals ap.ID
                    where ds.IDBanDoc == ID && ds.TrangThai != 10
                    select new cPhieuMuonView
                    {
                        ID = ds.ID,
                        BanDoc = bd.HoTen,
                        IDBanDoc = ds.IDBanDoc,
                        IDMCB = ds.IDMCB,
                        MCB = mcb.MCB,
                        NgayHenTra = ds.NgayHenTra,
                        NgayMuon = ds.NgayMuon,
                        NgayTra = ds.NgayTra,
                        DonVi = dv.TenDonVi,
                        NhanDe = ap.NhanDe,
                        TrangThai = ds.TrangThai
                    }).ToList();
            return item;
        }

        public List<cPhieuMuonView> getAllDataView(int IDDV, string search)
        {
            var itemall = db.cPhieuMuons.Where(x => x.NgayHenTra < DateTime.Now && x.TrangThai != 1 && x.TrangThai != 10).ToList();
            foreach (var it in itemall)
            {
                it.TrangThai = 4;
            }
            db.SaveChanges();

            List<cPhieuMuonView> item = new List<cPhieuMuonView>();
            item = (from ds in db.cPhieuMuons
                    join bd in db.cBanDocs on ds.IDBanDoc equals bd.ID
                    join mcb in db.cMCBs on ds.IDMCB equals mcb.ID
                    join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                    where ds.IDDonVi == IDDV && ds.TrangThai != 10
                    select new cPhieuMuonView
                    {
                        ID = ds.ID,
                        BanDoc = bd.HoTen,
                        IDBanDoc = ds.IDBanDoc,
                        IDMCB = ds.IDMCB,
                        MCB = mcb.MCB,
                        NgayHenTra = ds.NgayHenTra,
                        NgayMuon = ds.NgayMuon,
                        NgayTra = ds.NgayTra,
                        DonVi = dv.TenDonVi,
                        TrangThai = ds.TrangThai
                    }).ToList();
            return item;
        }

        public bool Insert(cPhieuMuon result)
        {
            try
            {
                db.cPhieuMuons.Add(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(cPhieuMuon result, long IDNV)
        {
            try
            {
                cPhieuMuon item = db.cPhieuMuons.FirstOrDefault(x => x.ID == result.ID);
                item.IDBanDoc = result.IDBanDoc;
                item.IDMCB = result.IDMCB;
                item.NgayHenTra = result.NgayHenTra;
                item.NgayMuon = result.NgayMuon;
                item.NgayTra = result.NgayTra;
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
                cPhieuMuon item = db.cPhieuMuons.FirstOrDefault(x => x.ID == ID);
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

        public bool ChangeStatus(long ID, byte tt, long IDNV)
        {
            try
            {
                cPhieuMuon item = db.cPhieuMuons.FirstOrDefault(x => x.ID == ID);
                item.TrangThai = tt;
                if (tt == 1)
                {
                    item.NgayTra = DateTime.Now;
                }
                item.NgaySua = DateTime.Now;
                item.NguoiSua = IDNV;
                db.SaveChanges();
                if (tt == 1)
                {
                    var mcb = db.cMCBs.FirstOrDefault(x => x.ID == item.IDMCB);
                    mcb.TrangThai = 1;
                    db.SaveChanges();
                }
                if (tt == 3)
                {
                    var mcb = db.cMCBs.FirstOrDefault(x => x.ID == item.IDMCB);
                    mcb.TrangThai = 2;
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public long SLPhieuMuon()
        {
            return db.cPhieuMuons.Where(x => x.TrangThai != 10).ToList().Count;
        }

        public bool UpdateDate(long ID, long IDNV)
        {
            try
            {
                var item = db.cPhieuMuons.FirstOrDefault(x => x.ID == ID);
                DateTime tghethan = item.NgayHenTra.GetValueOrDefault();
                if (item.TrangThai == 4 || item.TrangThai == 5)
                {
                    item.TrangThai = 3;
                }
                item.NgayHenTra = tghethan.AddDays(5);
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