namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("aTacGia")]
    public partial class aTacGia
    {
        public long ID { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; }

        [StringLength(500)]
        public string AnhDaiDien { get; set; }

        [StringLength(50)]
        public string NgaySinh { get; set; }

        [StringLength(50)]
        public string NgayMat { get; set; }

        public string DiaChi { get; set; }

        public string GoiThieu { get; set; }

        [StringLength(100)]
        public string MaTG { get; set; }

        public byte? TrangThai { get; set; }

        public DateTime? NgayTao { get; set; }

        public long? NguoiTao { get; set; }

        public DateTime? NgaySua { get; set; }

        public long? NguoiSua { get; set; }
    }
}
