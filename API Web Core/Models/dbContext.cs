using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API_Web_Core.Models
{
    public partial class dbContext : DbContext
    {
        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<PivotUserRole> PivotUserRoles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=VideoAspCore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId).ValueGeneratedOnAdd().HasColumnName("role_id");

                entity.Property(e => e.parentID).HasColumnName("parent_id");

                //entity.HasMany(d => d.Users)
                //    .WithMany(p => p.Roles).UsingEntity(j => j.ToTable("pivot_user_role"));

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.childRoles)
                    .HasForeignKey(d => d.parentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__pivot_parent__role___4A75D761");


                entity.Property(e => e.RoleDescription)
                    .HasColumnType("text")
                    .HasColumnName("role_description");

                entity.Property(e => e.RoleKey)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("role_key");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).ValueGeneratedOnAdd().HasColumnName("user_id");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users).UsingEntity<PivotUserRole>(
                        j => j
                            .HasOne(pt => pt.Role)
                            .WithMany(t => t.PivotUserRoles)
                            .HasForeignKey(pt => pt.RoleId),
                        j => j
                            .HasOne(pt => pt.User)
                            .WithMany(p => p.PivotUserRoles)
                            .HasForeignKey(pt => pt.UserId),
                        j =>
                        {
                            j.HasKey(t => new { t.RoleId, t.UserId });
                        });

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
