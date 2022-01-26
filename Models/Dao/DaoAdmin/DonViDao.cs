using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao.DaoAdmin
{
    public class DonViDao
    {
        LibDbContext db = new LibDbContext();

        public sDonVi getDataByID(int ID)
        {
            return db.sDonVis.FirstOrDefault(x => x.ID == ID);
        }

        public sDonVi getDataByHost(string host)
        {
            return db.sDonVis.FirstOrDefault(x => x.Url == host);
        }

        public List<sDonVi> getAllDataView()
        {
            List<sDonVi> lst = new List<sDonVi>();
            lst = db.sDonVis.Where(x => x.TrangThai != 10).ToList();
            //if (search == "")
            //{
            //    lst = db.sDonVis.Where(x => x.TrangThai != 10).ToList();
            //}
            //else
            //{
            //    lst = db.sDonVis.Where(x => x.TrangThai != 10 && x.TenDonVi.Contains(search)).ToList();
            //}
            return lst;
        }

        public bool InsertDonVi(sDonVi result)
        {
            try
            {
                db.sDonVis.Add(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDonVi(sDonVi result, long IDNV)
        {
            try
            {
                sDonVi item = db.sDonVis.FirstOrDefault(x => x.ID == result.ID);
                item.TenDonVi = result.TenDonVi;
                item.MaDonVi = result.MaDonVi;
                item.Url = result.Url;
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

        public bool DeleteDonVi(int ID, long IDNV)
        {
            try
            {
                sDonVi item = db.sDonVis.FirstOrDefault(x => x.ID == ID);
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
                sDonVi item = db.sDonVis.FirstOrDefault(x => x.ID == ID);
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