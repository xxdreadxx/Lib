namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cPhieuMuon")]
    public partial class cPhieuMuon
    {
        public long ID { get; set; }

        public long? IDMCB { get; set; }

        public long? IDBanDoc { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayMuon { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayHenTra { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTra { get; set; }

        public byte? TrangThai { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public long? NguoiTao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySua { get; set; }

        public long? NguoiSua { get; set; }
    }
}
