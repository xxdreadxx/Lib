using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Models.ModelsView;

namespace Models.Dao.DaoAdmin
{
    public class BanDocDao
    {
        LibDbContext db = new LibDbContext();

        public cBanDoc getDataByID(long ID)
        {
            return db.cBanDocs.FirstOrDefault(x => x.ID == ID);
        }

        public long getDataByUsername(string username, string password, int IDDonVi)
        {
            sNhanVien item = new sNhanVien();
            item = db.sNhanViens.FirstOrDefault(x => x.Username == username && x.IDDonVi == IDDonVi && x.TrangThai != 10);
            if (item == null)
            {
                return -1;
            }
            else
            {
                item = db.sNhanViens.FirstOrDefault(x => x.Username == username && x.Password == password && x.IDDonVi == IDDonVi && x.TrangThai != 10);
                if (item == null)
                {
                    return -2;
                }
                else
                {
                    if (item.TrangThai != 1)
                    {
                        return -3;
                    }
                    else
                    {
                        return item.ID;
                    }
                }
            }
        }

        public cBanDocView getDataViewByID(long ID)
        {
            cBanDocView item = new cBanDocView();
            item = (from ds in db.cBanDocs
                    join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                    where ds.ID == ID
                    select new cBanDocView
                    {
                        ID = ds.ID,
                        HoTen = ds.HoTen,
                        AnhDaiDien = ds.AnhDaiDien,
                        CMTND = ds.CMTND,
                        IDDonVi = ds.IDDonVi,
                        DonViThuVien = dv.TenDonVi,
                        DiaChi = ds.DiaChi,
                        Email = ds.Email,
                        NgaySinh = ds.NgaySinh,
                        Password = ds.Password,
                        SDT = ds.SDT,
                        Username = ds.Username
                    }).FirstOrDefault();
            return item;
        }

        public List<cBanDocView> getAllDataView(int IDDV, string search)
        {
            var itemall = db.cBanDocs.Where(x => x.NgayHetHan < DateTime.Now && x.TrangThai != 10).ToList();
            foreach(var it in itemall)
            {
                it.TrangThai = 3;
            }
            db.SaveChanges();

            List<cBanDocView> item = new List<cBanDocView>();
            if (search == "")
            {
                item = (from ds in db.cBanDocs
                        join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                        where ds.IDDonVi == IDDV && ds.TrangThai!=10
                        select new cBanDocView
                        {
                            ID = ds.ID,
                            HoTen = ds.HoTen,
                            AnhDaiDien = ds.AnhDaiDien,
                            CMTND = ds.CMTND,
                            IDDonVi = ds.IDDonVi,
                            DonViThuVien = dv.TenDonVi,
                            DiaChi = ds.DiaChi,
                            Email = ds.Email,
                            NgaySinh = ds.NgaySinh,
                            SDT = ds.SDT,
                            NgayTao = ds.NgayTao,
                            NgayHetHan = ds.NgayHetHan,
                            Username = ds.Username,
                            TrangThai = ds.TrangThai
                        }).ToList();
            }
            else
            {
                item = (from ds in db.cBanDocs
                        join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                        where ds.IDDonVi == IDDV && ds.HoTen.Contains(search) && ds.TrangThai != 10
                        select new cBanDocView
                        {
                            ID = ds.ID,
                            HoTen = ds.HoTen,
                            AnhDaiDien = ds.AnhDaiDien,
                            CMTND = ds.CMTND,
                            IDDonVi = ds.IDDonVi,
                            DonViThuVien = dv.TenDonVi,
                            DiaChi = ds.DiaChi,
                            Email = ds.Email,
                            NgaySinh = ds.NgaySinh,
                            SDT = ds.SDT,
                            NgayTao = ds.NgayTao,
                            NgayHetHan = ds.NgayHetHan,
                            Username = ds.Username,
                            TrangThai = ds.TrangThai
                        }).ToList();
            }

            return item;
        }

        public bool Insert(cBanDoc result)
        {
            try
            {
                db.cBanDocs.Add(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(cBanDoc result, long IDNV)
        {
            try
            {
                cBanDoc item = db.cBanDocs.FirstOrDefault(x => x.ID == result.ID);
                item.HoTen = result.HoTen;
                item.AnhDaiDien = result.AnhDaiDien;
                item.CMTND = result.CMTND;
                item.DiaChi = result.DiaChi;
                item.Email = result.Email;
                item.NgaySinh = result.NgaySinh;
                item.SDT = result.SDT;
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
                cBanDoc item = db.cBanDocs.FirstOrDefault(x => x.ID == ID);
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
                cBanDoc item = db.cBanDocs.FirstOrDefault(x => x.ID == ID);
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

        public bool UpdateDate(long ID, long IDNV)
        {
            try
            {
                var item = db.cBanDocs.FirstOrDefault(x => x.ID == ID);
                DateTime tghethan = item.NgayHetHan.GetValueOrDefault();
                if (item.TrangThai == 3)
                {
                    item.TrangThai = 1;
                }
                item.NgayHetHan = tghethan.AddYears(1);
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

        public long SLBanDoc()
        {
            return db.cBanDocs.Where(x => x.TrangThai != 10).ToList().Count;
        }
    }
}