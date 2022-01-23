namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sNhanVien")]
    public partial class sNhanVien
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string NgaySinh { get; set; }

        [StringLength(500)]
        public string AnhDaiDien { get; set; }

        [StringLength(50)]
        public string CMTND { get; set; }

        public string DiaChi { get; set; }

        public int? IDDonVi { get; set; }

        public int? SDT { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public byte? TrangThai { get; set; }

        public DateTime? NgayTao { get; set; }

        public long? NguoiTao { get; set; }

        public DateTime? NgaySua { get; set; }

        public long? NguoiSua { get; set; }

        public byte? Quyen { get; set; }
    }
}
