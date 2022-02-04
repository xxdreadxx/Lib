using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.Dao.DaoAdmin
{
    public class TacGiaDao
    {
        LibDbContext db = new LibDbContext();

        public aTacGia getDataByID(int ID)
        {
            return db.aTacGias.FirstOrDefault(x => x.ID == ID);
        }

        public List<aTacGia> getAllDataView(string search)
        {
            List<aTacGia> lst = new List<aTacGia>();
            if (search == "")
            {
                lst = db.aTacGias.Where(x => x.TrangThai != 10).ToList();
            }
            else
            {
                lst = db.aTacGias.Where(x => x.TrangThai != 10 && x.HoTen.Contains(search)).ToList();
            }
            return lst;
        }

        public bool Insert(aTacGia result, long IDNV)
        {
            try
            {
                result.NguoiTao = IDNV;
                result.NgayTao = DateTime.Now;
                db.aTacGias.Add(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(aTacGia result, long IDNV)
        {
            try
            {
                aTacGia item = db.aTacGias.FirstOrDefault(x => x.ID == result.ID);
                item.HoTen = result.HoTen;
                item.DiaChi = result.DiaChi;
                item.NgayMat = result.NgayMat;
                item.NgaySinh = result.NgaySinh;
                item.MaTG = result.MaTG;
                item.AnhDaiDien = result.AnhDaiDien;
                item.GoiThieu = result.GoiThieu;
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
                aTacGia item = db.aTacGias.FirstOrDefault(x => x.ID == ID);
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
                aTacGia item = db.aTacGias.FirstOrDefault(x => x.ID == ID);
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

        public List<aTacGia> get4TacGia()
        {
            return db.aTacGias.Where(x => x.TrangThai == 1).OrderByDescending(x => x.ID).ToList();
        }
    }
}
