﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.ModelsView
{
    public class ClassView
    {

    }

    public class sNhanVienView : sNhanVien
    {
        public string DonViThuVien { get; set; }
    }

    public class cBanDocView : cBanDoc
    {
        public string DonViThuVien { get; set; }
    }

    public class cAnPhamView : cAnPham
    {
        public string NXB { get; set; }
        public string TacGia { get; set; }
        public string PhanLoai { get; set; }
        public string KieuAP { get; set; }
    }

    public class cMCBView : cMCB
    {
        public string NhanDe { get; set; }
        public string DonVi { get; set; }
        public string DonViHienTai { get; set; }
    }

    public class cPhieuMuonView : cPhieuMuon
    {
        public string MCB { get; set; }
        public string BanDoc { get; set; }
        public string DonVi { get; set; }
    }
}
