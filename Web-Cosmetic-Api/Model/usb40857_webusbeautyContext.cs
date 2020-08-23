using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Web_Cosmetic_Api.Model
{
    public partial class usb40857_webusbeautyContext : DbContext
    {
        public usb40857_webusbeautyContext()
        {
        }

        public usb40857_webusbeautyContext(DbContextOptions<usb40857_webusbeautyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HinhAnh> HinhAnhs { get; set; }
        public virtual DbSet<KichCoSp> KichCoSps { get; set; }
        public virtual DbSet<LoaiSp> LoaiSps { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieux { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=112.78.2.30,1433;Initial Catalog=usb40857_webusbeauty;User ID=usb40857_uscosmetic;Password=Admin@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "usb40857_uscosmetic");

            modelBuilder.Entity<HinhAnh>(entity =>
            {
                entity.ToTable("HinhAnh", "usb40857_dangbmt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdSanPham).HasColumnName("idSanPham");

                entity.Property(e => e.IdThuongHieu).HasColumnName("idThuongHieu");

                entity.Property(e => e.LinkHinhAnh)
                    .HasColumnName("linkHinhAnh")
                    .HasMaxLength(50);

                entity.Property(e => e.TenHinhAnh)
                    .HasColumnName("tenHinhAnh")
                    .HasMaxLength(150);

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.HinhAnhs)
                    .HasForeignKey(d => d.IdSanPham)
                    .HasConstraintName("FK_HinhAnh_SanPham");

                entity.HasOne(d => d.IdThuongHieuNavigation)
                    .WithMany(p => p.HinhAnhs)
                    .HasForeignKey(d => d.IdThuongHieu)
                    .HasConstraintName("FK_HinhAnh_ThuongHieu");
            });

            modelBuilder.Entity<KichCoSp>(entity =>
            {
                entity.ToTable("KichCoSP", "usb40857_dangbmt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GiaSp).HasColumnName("giaSP");

                entity.Property(e => e.IdSanPham).HasColumnName("idSanPham");

                entity.Property(e => e.TenKichCo)
                    .HasColumnName("tenKichCo")
                    .HasMaxLength(250);

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.KichCoSps)
                    .HasForeignKey(d => d.IdSanPham)
                    .HasConstraintName("FK_KichCoSP_SanPham");
            });

            modelBuilder.Entity<LoaiSp>(entity =>
            {
                entity.ToTable("LoaiSP", "usb40857_dangbmt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdThuongHieu).HasColumnName("idThuongHieu");

                entity.Property(e => e.MoTa)
                    .HasColumnName("moTa")
                    .HasMaxLength(250);

                entity.Property(e => e.TenLoaiSp)
                    .HasColumnName("tenLoaiSP")
                    .HasMaxLength(250);

                entity.HasOne(d => d.IdThuongHieuNavigation)
                    .WithMany(p => p.LoaiSps)
                    .HasForeignKey(d => d.IdThuongHieu)
                    .HasConstraintName("FK_LoaiSP_LoaiSP");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.ToTable("SanPham", "usb40857_dangbmt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdLoaiSp).HasColumnName("idLoaiSP");

                entity.Property(e => e.LoiIch)
                    .HasColumnName("loiIch")
                    .HasMaxLength(500);

                entity.Property(e => e.MoTa)
                    .HasColumnName("moTa")
                    .HasMaxLength(500);

                entity.Property(e => e.TenSp)
                    .HasColumnName("tenSP")
                    .HasMaxLength(250);

                entity.HasOne(d => d.IdLoaiSpNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.IdLoaiSp)
                    .HasConstraintName("FK_SanPham_LoaiSP");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.ToTable("TaiKhoan", "usb40857_dangbmt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(250);

                entity.Property(e => e.MatKhau)
                    .HasColumnName("matKhau")
                    .HasMaxLength(250);

                entity.Property(e => e.TenTk)
                    .HasColumnName("tenTK")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<ThuongHieu>(entity =>
            {
                entity.ToTable("ThuongHieu", "usb40857_dangbmt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MoTa)
                    .HasColumnName("moTa")
                    .HasMaxLength(250);

                entity.Property(e => e.TenThuongHieu)
                    .HasColumnName("tenThuongHieu")
                    .HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
