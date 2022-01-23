using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Models.EF
{
    public partial class LibDbContext : DbContext
    {
        public LibDbContext()
            : base("name=LibDbContext")
        {
        }

        public virtual DbSet<aNXB> aNXBs { get; set; }
        public virtual DbSet<aPhanLoaiAP> aPhanLoaiAPs { get; set; }
        public virtual DbSet<aTacGia> aTacGias { get; set; }
        public virtual DbSet<cAnPham> cAnPhams { get; set; }
        public virtual DbSet<cBanDoc> cBanDocs { get; set; }
        public virtual DbSet<cBanDoc_AnPham> cBanDoc_AnPham { get; set; }
        public virtual DbSet<cMCB> cMCBs { get; set; }
        public virtual DbSet<cPhieuMuon> cPhieuMuons { get; set; }
        public virtual DbSet<sDonVi> sDonVis { get; set; }
        public virtual DbSet<sNhanVien> sNhanViens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
