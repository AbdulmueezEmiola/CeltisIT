using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CeltisIT.Model.Repository
{
    public partial class CeltisITContext : DbContext
    {
        public CeltisITContext()
        {
        }

        public CeltisITContext(DbContextOptions<CeltisITContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Tabcust> Tabcust { get; set; }
        public virtual DbSet<Tabsodata> Tabsodata { get; set; }
        public virtual DbSet<Tabsorder> Tabsorder { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

           // optionsBuilder.UseLazyLoadingProxies();            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemId);
            });

            modelBuilder.Entity<Tabcust>(entity =>
            {
                entity.HasKey(e => e.Custid)
                    .HasName("PK__TABCUST__3D9DDA3112B4E6C6");

                entity.ToTable("TABCUST");

                entity.Property(e => e.Custid)
                    .HasColumnName("CUSTID")
                    .HasColumnType("numeric(3, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Custname)
                    .IsRequired()
                    .HasColumnName("CUSTNAME")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Tabsodata>(entity =>
            {
                entity.HasKey(e => e.Sodataid)
                    .HasName("PK__tmp_ms_x__3FD9318E52F3DFF2");

                entity.ToTable("TABSODATA");

                entity.Property(e => e.Sodataid)
                    .HasColumnName("SODATAID")
                    .HasColumnType("numeric(3, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Dataexst)
                    .IsRequired()
                    .HasColumnName("DATAEXST")
                    .HasMaxLength(3);

                entity.Property(e => e.Itemid).HasColumnName("ITEMID");

                entity.Property(e => e.Itemrate)
                    .HasColumnName("ITEMRATE")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Sordid)
                    .HasColumnName("SORDID")
                    .HasColumnType("numeric(3, 0)");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Tabsodata)
                    .HasForeignKey(d => d.Itemid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TABSODATA__ITEMI__66603565");

                entity.HasOne(d => d.Sord)
                    .WithMany(p => p.Tabsodata)
                    .HasForeignKey(d => d.Sordid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TABSODATA__SORDI__6754599E");
            });

            modelBuilder.Entity<Tabsorder>(entity =>
            {
                entity.HasKey(e => e.Sordid)
                    .HasName("PK__TABSORDE__587C1D0E60B6BB12");

                entity.ToTable("TABSORDER");

                entity.Property(e => e.Sordid)
                    .HasColumnName("SORDID")
                    .HasColumnType("numeric(3, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Custid)
                    .HasColumnName("CUSTID")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Dataexst)
                    .IsRequired()
                    .HasColumnName("DATAEXST")
                    .HasMaxLength(3);

                entity.Property(e => e.Sordamnt)
                    .HasColumnName("SORDAMNT")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Sorddate)
                    .HasColumnName("SORDDATE")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Tabsorder)
                    .HasForeignKey(d => d.Custid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TABSORDER__CUSTI__68487DD7");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
