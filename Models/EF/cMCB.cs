namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cMCB")]
    public partial class cMCB
    {
        public long ID { get; set; }

        public long? IDAnPam { get; set; }

        [StringLength(50)]
        public string MCB { get; set; }
        public int MCBIndex { get; set; }

        public int? IDDonVi { get; set; }

        public int? IDDonVi_HienTai { get; set; }

        public byte? KieuAP { get; set; }

        public byte? TrangThai { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public long? NguoiTao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySua { get; set; }

        public long? NguoiSua { get; set; }
    }
}
