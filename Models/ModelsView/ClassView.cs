using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.ModelsView
{
    public class ClassView
    {

    }

    [NotMapped]
    public class sNhanVienView : sNhanVien
    {
        public string DonViThuVien { get; set; }
    }

    [NotMapped]
    public class cBanDocView : cBanDoc
    {
        public string DonViThuVien { get; set; }
    }

    [NotMapped]
    public class cAnPhamView : cAnPham
    {
        public string NXB { get; set; }
        public string TacGia { get; set; }
        public string PhanLoai { get; set; }
        public string KieuAP { get; set; }
    }

    [NotMapped]
    public class cMCBView : cMCB
    {
        public string NhanDe { get; set; }
        public string DonVi { get; set; }
        public string DonViHienTai { get; set; }
    }

    [NotMapped]
    public class cPhieuMuonView : cPhieuMuon
    {
        public string MCB { get; set; }
        public string BanDoc { get; set; }
        public string DonViM { get; set; }
        public string DonViT { get; set; }
        public string NhanDe { get; set; }
    }
}
