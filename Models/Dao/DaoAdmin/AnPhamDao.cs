using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public List<cAnPhamView> getAllData(int IDDonVi)
        {
            //var lst = (from ds in db.cAnPhams
            //           join mcb in db.cMCBs on ds.ID equals mcb.IDAnPam
            //           where (mcb.IDDonVi == IDDonVi || mcb.IDDonVi_HienTai == IDDonVi) && ds.TrangThai != 10
            //           select ds).ToList();
            List<cAnPhamView> _result = new List<cAnPhamView>();
            using (SqlConnection _conn = new SqlConnection(ConectionLib.ConnectString))
            {
                _conn.Open();
                try
                {
                    var _sqlStr = "select ap.ID, ap.NhanDe, ap.HinhAnh, ap.MaAnPham, nxb.TenNXB, pl.TenPhanLoaiAP, tg.HoTen from cAnPham ap " +
                        "join cMCB mcb on mcb.IDAnPam = ap.ID " +
                        "join aNXB nxb on ap.IDNXB = nxb.ID " +
                        "join aTacGia tg on tg.ID = ap.IDTacGia " +
                        "join aPhanLoaiAP pl on pl.id = ap.IDPLAP " +
                        "where mcb.TrangThai <> 10 and(mcb.IDDonVi = " + IDDonVi + " Or mcb.IDDonVi_HienTai = " + IDDonVi + ") and ap.LKieuAP = 1 " +
                        "group by ap.ID, ap.NhanDe, ap.HinhAnh, ap.MaAnPham, nxb.TenNXB, pl.TenPhanLoaiAP, tg.HoTen";

                    _result = _conn.Query<cAnPhamView>(_sqlStr, null, commandType: CommandType.Text).ToList<cAnPhamView>();
                    return _result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<cAnPhamView> getAll(string search, long IDTG, int IDPL)
        {
            List<cAnPhamView> _result = new List<cAnPhamView>();
            using (SqlConnection _conn = new SqlConnection(ConectionLib.ConnectString))
            {
                _conn.Open();
                try
                {
                    var _sqlStr = "select ap.ID, ap.NhanDe, ap.HinhAnh, ap.MaAnPham, nxb.TenNXB, pl.TenPhanLoaiAP, tg.HoTen from cAnPham ap " +
                        "join aNXB nxb on ap.IDNXB = nxb.ID " +
                        "join aTacGia tg on tg.ID = ap.IDTacGia " +
                        "join aPhanLoaiAP pl on pl.id = ap.IDPLAP " +
                        "where (tg.ID = " + IDTG + " or " + IDTG + " = 0) and (pl.ID = " + IDPL + " or " + IDPL + " = 0) and ap.LKieuAP = 1 " +
                        "and (ap.NhanDe like N'%" + search + "%') " +
                        "group by ap.ID, ap.NhanDe, ap.HinhAnh, ap.MaAnPham, nxb.TenNXB, pl.TenPhanLoaiAP, tg.HoTen";
                    _result = _conn.Query<cAnPhamView>(_sqlStr, null, commandType: CommandType.Text).ToList<cAnPhamView>();
                    return _result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<cAnPhamView> getAll1(byte type, int NXB, string search, string fromdate, string todate)
        {
            List<cAnPhamView> _result = new List<cAnPhamView>();
            using (SqlConnection _conn = new SqlConnection(ConectionLib.ConnectString))
            {
                string sqlA = "";
                if (fromdate != "") {
                    sqlA += "and ap.NgayXuatBan >= " + fromdate + " ";
                        }
                if (todate != "")
                {
                    sqlA += "and ap.NgayXuatBan <= " + todate + " ";
                }
                _conn.Open();
                try
                {
                    var _sqlStr = "select ap.ID, ap.NhanDe, ap.HinhAnh, ap.MaAnPham, nxb.TenNXB, pl.TenPhanLoaiAP, tg.HoTen from cAnPham ap " +
                        "join aNXB nxb on ap.IDNXB = nxb.ID " +
                        "where ap.LKieuAP = " + type + " and (nxb.ID = " + NXB + " or " + NXB + " = 0) " +
                        "" + sqlA + "" +
                        "and (ap.NhanDe like N'%" + search + "%') " +
                        "group by ap.ID, ap.NhanDe, ap.HinhAnh, ap.MaAnPham, nxb.TenNXB, pl.TenPhanLoaiAP, tg.HoTen";
                    _result = _conn.Query<cAnPhamView>(_sqlStr, null, commandType: CommandType.Text).ToList<cAnPhamView>();
                    return _result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public int SLAP()
        {
            return db.cAnPhams.Where(x => x.TrangThai != 10).ToList().Count;
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