using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AuthorizationModule.Models
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

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseModels && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseModels)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseModels)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }

        //public virtual DbSet<PivotUserRole> PivotUserRoles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<GetPermissions> GetPermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=DESKTOP-ARDK013;initial catalog=authorization_module;trusted_connection=true");
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

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.childRoles)
                    .HasForeignKey(d => d.parentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__pivot_parent__role___4A75D761");


                entity.Property(e => e.RoleDescription)
                    .HasColumnType("text")
                    .IsRequired(false)
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

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permissions");

                entity.HasKey(e => e.PermissionId);

                entity.Property(e => e.PermissionId).ValueGeneratedOnAdd().HasColumnName("permission_id");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Permissions).UsingEntity<PivotRolePermission>(
                        j => j
                            .HasOne(pt => pt.Role)
                            .WithMany(t => t.PivotRolePermission)
                            .HasForeignKey(pt => pt.RoleId),
                        j => j
                            .HasOne(pt => pt.Permission)
                            .WithMany(p => p.PivotRolePermission)
                            .HasForeignKey(pt => pt.PermissionId),
                        j =>
                        {
                            j.HasKey(t => new { t.RoleId, t.PermissionId });
                        });

                entity.Property(e => e.PermissionKey)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("permission_key");

                entity.HasIndex(e => e.PermissionKey).IsUnique();


                entity.Property(e => e.PermissionDescription)
                    .HasColumnType("text")
                    .IsRequired(false)
                    .HasColumnName("permission_description");
                   
            });

            modelBuilder.Entity<GetPermissions>(entity =>
            {
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
