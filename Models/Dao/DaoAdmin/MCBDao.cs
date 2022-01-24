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
                        where ds.TrangThai != 10 && ap.ID == IDAP
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
                        }).ToList();
            }
            else
            {
                item = (from ds in db.cMCBs
                        join ap in db.cAnPhams on ds.IDAnPam equals ap.ID
                        join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                        where ds.TrangThai != 10 && ds.MCB.Contains(search) && ap.ID == IDAP
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
                        }).ToList();
            }
            return item;
        }

        public bool Insert(cMCB result)
        {
            try
            {
                db.cMCBs.Add(result);
                db.SaveChanges();
                return true;
            }
            catch
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