using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Models.ModelsView;

namespace Models.Dao.DaoAdmin
{
    public class AnPhamDao
    {
        LibDbContext db = new LibDbContext();

        public cAnPham getDataByID(long ID)
        {
            return db.cAnPhams.FirstOrDefault(x => x.ID == ID);
        }

        public cAnPhamView getDataViewByID(long ID)
        {
            cAnPhamView item = new cAnPhamView();
            item = (from ds in db.cAnPhams
                    join tg in db.aTacGias on ds.IDTacGia equals tg.ID
                    join nxb in db.aNXBs on ds.IDNXB equals nxb.ID
                    join pl in db.aPhanLoaiAPs on ds.IDPLAP equals pl.ID
                    where ds.ID == ID
                    select new cAnPhamView
                    {
                        ID = ds.ID,
                        DongTacGia = ds.DongTacGia,
                        GioiThieu = ds.GioiThieu,
                        HinhAnh = ds.HinhAnh,
                        IDNXB = ds.IDNXB,
                        IDPLAP = ds.IDPLAP,
                        IDTacGia = ds.IDTacGia,
                        LKieuAP = ds.LKieuAP,
                        MaAnPham = ds.MaAnPham,
                        KieuAP = ds.LKieuAP == 1 ? "Sách" : ds.LKieuAP == 2 ? "Báo" : "Tạp chí",
                        So = ds.So,
                        TacGia = tg.HoTen,
                        NgayXuatBan = ds.NgayXuatBan,
                        NhanDe = ds.NhanDe,
                        NXB = nxb.TenNXB,
                        PhanLoai = pl.TenPhanLoaiAP,
                    }).FirstOrDefault();
            return item;
        }

        public List<cAnPhamView> getAllDataView(string search)
        {
            List<cAnPhamView> item = new List<cAnPhamView>();
            if (search == "")
            {
                item = (from ds in db.cAnPhams
                        join tg in db.aTacGias on ds.IDTacGia equals tg.ID
                        join nxb in db.aNXBs on ds.IDNXB equals nxb.ID
                        join pl in db.aPhanLoaiAPs on ds.IDPLAP equals pl.ID
                        where ds.TrangThai!=10
                        select new cAnPhamView
                        {
                            ID = ds.ID,
                            DongTacGia = ds.DongTacGia,
                            GioiThieu = ds.GioiThieu,
                            HinhAnh = ds.HinhAnh,
                            IDNXB = ds.IDNXB,
                            IDPLAP = ds.IDPLAP,
                            IDTacGia = ds.IDTacGia,
                            LKieuAP = ds.LKieuAP,
                            MaAnPham = ds.MaAnPham,
                            KieuAP = ds.LKieuAP == 1 ? "Sách" : ds.LKieuAP == 2 ? "Báo" : "Tạp chí",
                            So = ds.So,
                            TacGia = tg.HoTen,
                            NgayXuatBan = ds.NgayXuatBan,
                            NhanDe = ds.NhanDe,
                            NXB = nxb.TenNXB,
                            PhanLoai = pl.TenPhanLoaiAP,
                            TrangThai = ds.TrangThai
                        }).ToList();
            }
            else
            {
                item = (from ds in db.cAnPhams
                        join tg in db.aTacGias on ds.IDTacGia equals tg.ID
                        join nxb in db.aNXBs on ds.IDNXB equals nxb.ID
                        join pl in db.aPhanLoaiAPs on ds.IDPLAP equals pl.ID
                        where ds.TrangThai!=10 && ds.NhanDe.Contains(search)
                        select new cAnPhamView
                        {
                            ID = ds.ID,
                            DongTacGia = ds.DongTacGia,
                            GioiThieu = ds.GioiThieu,
                            HinhAnh = ds.HinhAnh,
                            IDNXB = ds.IDNXB,
                            IDPLAP = ds.IDPLAP,
                            IDTacGia = ds.IDTacGia,
                            LKieuAP = ds.LKieuAP,
                            MaAnPham = ds.MaAnPham,
                            KieuAP = ds.LKieuAP == 1 ? "Sách" : ds.LKieuAP == 2 ? "Báo" : "Tạp chí",
                            So = ds.So,
                            TacGia = tg.HoTen,
                            NgayXuatBan = ds.NgayXuatBan,
                            NhanDe = ds.NhanDe,
                            NXB = nxb.TenNXB,
                            PhanLoai = pl.TenPhanLoaiAP,
                            TrangThai = ds.TrangThai
                        }).ToList();
            }
            return item;
        }

        public bool Insert(cAnPham result, long IDNV)
        {
            try
            {
                result.NguoiTao = IDNV;
                result.NgayTao = DateTime.Now;
                db.cAnPhams.Add(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(cAnPham result, long IDNV)
        {
            try
            {
                cAnPham item = db.cAnPhams.FirstOrDefault(x => x.ID == result.ID);
                item.NhanDe = result.NhanDe;
                if (result.HinhAnh != null)
                {
                    item.HinhAnh = result.HinhAnh;
                }
                item.DongTacGia = result.DongTacGia;
                item.GioiThieu = result.GioiThieu;
                item.IDNXB = result.IDNXB;
                item.IDPLAP = result.IDPLAP;
                item.IDTacGia = result.IDTacGia;
                item.LKieuAP = result.LKieuAP;
                item.MaAnPham = result.MaAnPham;
                item.So = result.So;
                item.NgayXuatBan = result.NgayXuatBan;
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
                cAnPham item = db.cAnPhams.FirstOrDefault(x => x.ID == ID);
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
                cAnPham item = db.cAnPhams.FirstOrDefault(x => x.ID == ID);
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

        public long GetCountMCB(long ID)
        {
            return db.cMCBs.Where(x => x.IDAnPam == ID && x.TrangThai != 10).ToList().Count();
        }
    }
}