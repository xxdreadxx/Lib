using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Models.ModelsView;

namespace Models.Dao.DaoAdmin
{
    public class NhanVienDao
    {
        LibDbContext db = new LibDbContext();

        public sNhanVien getDataByID(long ID)
        {
            return db.sNhanViens.FirstOrDefault(x => x.ID == ID);
        }

        //0: đăng nhập thành công/ 1: username ko tồn tại/ 2: sai password/ 3: tài khoản bị dừng sử dụng hoặc chưa kích hoạt
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

        public sNhanVienView getDataViewByID(long ID)
        {
            sNhanVienView item = new sNhanVienView();
            item = (from ds in db.sNhanViens
                    join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                    where ds.ID == ID
                    select new sNhanVienView
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
                        Quyen = ds.Quyen,
                        SDT = ds.SDT,
                        Username = ds.Username
                    }).FirstOrDefault();
            return item;
        }

        public List<sNhanVienView> getAllDataView(int IDDV, string search)
        {
            List<sNhanVienView> item = new List<sNhanVienView>();
            if (search == "")
            {
                item = (from ds in db.sNhanViens
                        join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                        where ds.IDDonVi == IDDV
                        select new sNhanVienView
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
                            Quyen = ds.Quyen,
                            SDT = ds.SDT,
                            Username = ds.Username
                        }).ToList();
            }
            else
            {
                item = (from ds in db.sNhanViens
                        join dv in db.sDonVis on ds.IDDonVi equals dv.ID
                        where ds.IDDonVi == IDDV && ds.HoTen.Contains(search)
                        select new sNhanVienView
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
                            Quyen = ds.Quyen,
                            SDT = ds.SDT,
                            Username = ds.Username
                        }).ToList();
            }
            
            return item;
        }

        public bool Insert(sNhanVien nv)
        {
            try
            {
                db.sNhanViens.Add(nv);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(sNhanVien nv, long IDNV)
        {
            try
            {
                sNhanVien item = db.sNhanViens.FirstOrDefault(x => x.ID == nv.ID);
                item.HoTen = nv.HoTen;
                item.AnhDaiDien = nv.AnhDaiDien;
                item.CMTND = nv.CMTND;
                item.DiaChi = nv.DiaChi;
                item.Email = nv.Email;
                item.NgaySinh = nv.NgaySinh;
                item.SDT = nv.SDT;
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
                sNhanVien item = db.sNhanViens.FirstOrDefault(x => x.ID == ID);
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
                sNhanVien item = db.sNhanViens.FirstOrDefault(x => x.ID == ID);
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