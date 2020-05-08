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
        public virtual DbSet<MailMessages> MailMessages { get; set; }

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

                entity.Property(e => e.DisplayName)
                    .HasColumnName("displayName")
                    .HasMaxLength(40)
                    .IsUnicode(false);

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

                entity.Property(e => e.PhotoPath)
                    .HasColumnName("photoPath")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserAccountId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MailMessages>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FromProfileId).HasColumnName("fromProfileID");

                entity.Property(e => e.IsRead).HasColumnName("isRead");

                entity.Property(e => e.MessageText)
                    .IsRequired()
                    .HasColumnName("messageText")
                    .HasColumnType("text");

                entity.Property(e => e.MessageTitle)
                    .IsRequired()
                    .HasColumnName("messageTitle")
                    .IsUnicode(false);

                entity.Property(e => e.ToProfileId).HasColumnName("toProfileID");

                entity.HasOne(d => d.FromProfile)
                    .WithMany(p => p.MailMessagesFromProfile)
                    .HasForeignKey(d => d.FromProfileId)
                    .HasConstraintName("FK__MailMessa__fromP__5CD6CB2B");

                entity.HasOne(d => d.ToProfile)
                    .WithMany(p => p.MailMessagesToProfile)
                    .HasForeignKey(d => d.ToProfileId)
                    .HasConstraintName("FK__MailMessa__toPro__5DCAEF64");
            });
        }
    }
}
