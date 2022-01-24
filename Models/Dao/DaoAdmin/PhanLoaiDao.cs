using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.Dao.DaoAdmin
{
    public class PhanLoaiDao
    {
        LibDbContext db = new LibDbContext();

        public aPhanLoaiAP getDataByID(int ID)
        {
            return db.aPhanLoaiAPs.FirstOrDefault(x => x.ID == ID);
        }

        public List<aPhanLoaiAP> getAllDataView(string search)
        {
            List<aPhanLoaiAP> lst = new List<aPhanLoaiAP>();
            if (search == "")
            {
                lst = db.aPhanLoaiAPs.Where(x => x.TrangThai != 10).ToList();
            }
            else
            {
                lst = db.aPhanLoaiAPs.Where(x => x.TrangThai != 10 && x.TenPhanLoaiAP.Contains(search)).ToList();
            }
            return lst;
        }

        public bool Insert(aPhanLoaiAP result)
        {
            try
            {
                db.aPhanLoaiAPs.Add(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(aPhanLoaiAP result, long IDNV)
        {
            try
            {
                aPhanLoaiAP item = db.aPhanLoaiAPs.FirstOrDefault(x => x.ID == result.ID);
                item.TenPhanLoaiAP = result.TenPhanLoaiAP;
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

        public bool Delete(int ID, long IDNV)
        {
            try
            {
                aPhanLoaiAP item = db.aPhanLoaiAPs.FirstOrDefault(x => x.ID == ID);
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

        public bool ChangeStatus(int ID, byte tt, long IDNV)
        {
            try
            {
                aPhanLoaiAP item = db.aPhanLoaiAPs.FirstOrDefault(x => x.ID == ID);
                item.TrangThai = tt;
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
