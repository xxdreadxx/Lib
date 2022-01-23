namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sDonVi")]
    public partial class sDonVi
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string MaDonVi { get; set; }

        [StringLength(200)]
        public string TenDonVi { get; set; }

        [StringLength(50)]
        public string Url { get; set; }

        public byte? TrangThai { get; set; }

        public DateTime? NgayTao { get; set; }

        public long? NguoiTao { get; set; }

        public DateTime? NgaySua { get; set; }

        public long? NguoiSua { get; set; }
    }
}
