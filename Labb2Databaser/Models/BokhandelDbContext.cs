using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Labb2Databaser.Models;

public partial class BokhandelDbContext : DbContext
{
    public BokhandelDbContext()
    {
    }

    public BokhandelDbContext(DbContextOptions<BokhandelDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<InventoryBalance> InventoryBalances { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<VTitlesPerAuthor> VTitlesPerAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-JG62B3NH;Initial Catalog=BokhandelDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC07038EDE9F");

            entity.ToTable("Author");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(30);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Books__447D36EB7DA8B35A");

            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .HasColumnName("ISBN");
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.ReleaseYear).HasColumnType("date");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__AuthorId__2180FB33");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__Genre__737584F7E4FF85DD");

            entity.ToTable("Genre");

            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .HasColumnName("ISBN");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.Genres)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Genre__ISBN__3F115E1A");
        });

        modelBuilder.Entity<InventoryBalance>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.Isbn }).HasName("CK_InventoryBalance");

            entity.ToTable("InventoryBalance");

            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .HasColumnName("ISBN");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.InventoryBalances)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISBN");

            entity.HasOne(d => d.Store).WithMany(p => p.InventoryBalances)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoreId");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__Publishe__737584F781A790FD");

            entity.ToTable("Publisher");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .HasColumnName("ISBN");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.Publishers)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Publisher__ISBN__3C34F16F");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Store__3214EC07E6813F1C");

            entity.ToTable("Store");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(5);
        });

        modelBuilder.Entity<VTitlesPerAuthor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vTitlesPerAuthor");

            entity.Property(e => e.Age)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(51);
            entity.Property(e => e.TotalStoreValue)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TotalTitles)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
