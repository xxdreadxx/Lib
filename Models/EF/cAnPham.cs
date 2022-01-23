namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cAnPham")]
    public partial class cAnPham
    {
        public long ID { get; set; }

        [StringLength(200)]
        public string NhanDe { get; set; }

        [StringLength(50)]
        public string MaAnPham { get; set; }

        public long? IDTacGia { get; set; }

        public string DongTacGia { get; set; }

        public int? IDPLAP { get; set; }

        [StringLength(500)]
        public string HinhAnh { get; set; }

        public string GioiThieu { get; set; }

        public byte? LKieuAP { get; set; }

        public int? IDNXB { get; set; }

        [StringLength(50)]
        public string So { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayXuatBan { get; set; }

        public byte? TrangThai { get; set; }

        public DateTime? NgayTao { get; set; }

        public long? NguoiTao { get; set; }

        public DateTime? NgaySua { get; set; }

        public long? NguoiSua { get; set; }
    }
}
