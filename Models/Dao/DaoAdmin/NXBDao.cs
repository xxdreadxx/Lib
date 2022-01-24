using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.Dao.DaoAdmin
{
    public class NXBDao
    {
        LibDbContext db = new LibDbContext();

        public aNXB getDataByID(int ID)
        {
            return db.aNXBs.FirstOrDefault(x => x.ID == ID);
        }

        public List<aNXB> getAllDataView(string search)
        {
            List<aNXB> lst = new List<aNXB>();
            if (search == "")
            {
                lst = db.aNXBs.Where(x => x.TrangThai != 10).ToList();
            }
            else
            {
                lst = db.aNXBs.Where(x => x.TrangThai != 10 && x.TenNXB.Contains(search)).ToList();
            }
            return lst;
        }

        public bool Insert(aNXB result)
        {
            try
            {
                db.aNXBs.Add(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(aNXB result, long IDNV)
        {
            try
            {
                aNXB item = db.aNXBs.FirstOrDefault(x => x.ID == result.ID);
                item.TenNXB = result.TenNXB;
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
                aNXB item = db.aNXBs.FirstOrDefault(x => x.ID == ID);
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
                aNXB item = db.aNXBs.FirstOrDefault(x => x.ID == ID);
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
