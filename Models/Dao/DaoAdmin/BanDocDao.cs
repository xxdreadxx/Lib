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
            List<cBanDocView> item = new List<cBanDocView>();
            if (search == "")
            {
                item = (from ds in db.sNhanViens
                        join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                        where ds.IDDonVi == IDDV
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
                        }).ToList();
            }
            else
            {
                item = (from ds in db.cBanDocs
                        join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                        where ds.IDDonVi == IDDV && ds.HoTen.Contains(search)
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
    }
}