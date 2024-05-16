using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BasisCRUD.Models;

public partial class DemoDbContext : DbContext
{
    public DemoDbContext()
    {
    }

    public DemoDbContext(DbContextOptions<DemoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = DESKTOP-7R12NSA\\SQLEXPRESS01; Database = demo_db; Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__3DE0C227028A7F32");

            entity.ToTable("Book");

            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.BookName).HasMaxLength(100);
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.PublishId).HasColumnName("PublishID");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__CategoryID__1DB06A4F");

            entity.HasOne(d => d.Publish).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__PublishID__1EA48E88");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2BA1D2AA56");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(255);
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublishId).HasName("PK__Publishe__C259F133BFE2B96B");

            entity.ToTable("Publisher");

            entity.Property(e => e.PublishId).HasColumnName("PublishID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContactName).HasMaxLength(255);
            entity.Property(e => e.PublishName).HasMaxLength(255);
            entity.Property(e => e.Telephone).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
