namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("aNXB")]
    public partial class aNXB
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string TenNXB { get; set; }

        public byte? TrangThai { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public long? NguoiTao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySua { get; set; }

        public long? NguoiSua { get; set; }
    }
}
