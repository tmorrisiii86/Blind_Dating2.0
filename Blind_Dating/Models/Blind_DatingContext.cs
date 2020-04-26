using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Blind_Dating.Models
{
    public partial class Blind_DatingContext : DbContext
    {
        public Blind_DatingContext()
        {
        }

        public Blind_DatingContext(DbContextOptions<Blind_DatingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DatingProfile> DatingProfile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=Blind_Dating;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatingProfile>(entity =>
            {
                entity.ToTable("Dating_Profile");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserAccountId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
