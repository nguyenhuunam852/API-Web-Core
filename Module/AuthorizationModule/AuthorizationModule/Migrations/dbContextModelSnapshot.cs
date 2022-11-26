﻿// <auto-generated />
using System;
using AuthorizationModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthorizationModule.Migrations
{
    [DbContext(typeof(dbContext))]
    partial class dbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AuthorizationModule.Models.GetPermissions", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("GetPermissions");
                });

            modelBuilder.Entity("AuthorizationModule.Models.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("permission_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionId"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PermissionDescription")
                        .HasColumnType("text")
                        .HasColumnName("permission_description");

                    b.Property<string>("PermissionKey")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("permission_key");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PermissionId");

                    b.HasIndex("PermissionKey")
                        .IsUnique();

                    b.ToTable("permissions", (string)null);
                });

            modelBuilder.Entity("AuthorizationModule.Models.PivotRolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("PivotRolePermission");
                });

            modelBuilder.Entity("AuthorizationModule.Models.PivotUserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("PivotUserRole");
                });

            modelBuilder.Entity("AuthorizationModule.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("text")
                        .HasColumnName("role_description");

                    b.Property<string>("RoleKey")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("role_key");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("parentID")
                        .HasColumnType("int")
                        .HasColumnName("parent_id");

                    b.HasKey("RoleId");

                    b.HasIndex("parentID");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("AuthorizationModule.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("user_name");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("user_password");

                    b.HasKey("UserId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("AuthorizationModule.Models.PivotRolePermission", b =>
                {
                    b.HasOne("AuthorizationModule.Models.Permission", "Permission")
                        .WithMany("PivotRolePermission")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthorizationModule.Models.Role", "Role")
                        .WithMany("PivotRolePermission")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("AuthorizationModule.Models.PivotUserRole", b =>
                {
                    b.HasOne("AuthorizationModule.Models.Role", "Role")
                        .WithMany("PivotUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthorizationModule.Models.User", "User")
                        .WithMany("PivotUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AuthorizationModule.Models.Role", b =>
                {
                    b.HasOne("AuthorizationModule.Models.Role", "Parent")
                        .WithMany("childRoles")
                        .HasForeignKey("parentID")
                        .HasConstraintName("FK__pivot_parent__role___4A75D761");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("AuthorizationModule.Models.Permission", b =>
                {
                    b.Navigation("PivotRolePermission");
                });

            modelBuilder.Entity("AuthorizationModule.Models.Role", b =>
                {
                    b.Navigation("PivotRolePermission");

                    b.Navigation("PivotUserRoles");

                    b.Navigation("childRoles");
                });

            modelBuilder.Entity("AuthorizationModule.Models.User", b =>
                {
                    b.Navigation("PivotUserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}