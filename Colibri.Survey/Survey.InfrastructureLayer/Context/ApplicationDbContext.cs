using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Survey.InfrastructureLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(SqlConnectionFactory.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            #region AspNetCore Identity
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.Property(x => x.ConcurrencyStamp)
                    .IsConcurrencyToken();

                b.Property(x => x.Name)
                    .HasMaxLength(256);

                b.Property(x => x.NormalizedName)
                    .HasMaxLength(256);

                b.HasKey(x => x.Id);

                b.HasIndex(x => x.NormalizedName)
                    .HasName("RoleNameIndex");

                b.ToTable("AspNetRoles", "dbo");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                b.Property(x => x.RoleId)
                    .IsRequired();

                b.HasKey(x => x.Id);

                b.HasIndex(x => x.RoleId);

                b.HasOne<IdentityRole>()
                    .WithMany()
                    .HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.ToTable("AspNetRoleClaims", "dbo");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                b.Property(x => x.UserId)
                    .IsRequired();

                b.HasKey(x => x.Id);

                b.HasIndex(x => x.UserId);

                b.HasOne<ApplicationUser>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.ToTable("AspNetUserClaims", "dbo");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.Property(x => x.UserId)
                    .IsRequired();

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex(x => x.UserId);

                b.HasOne<ApplicationUser>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.ToTable("AspNetUserLogins", "dbo");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasKey("UserId", "RoleId");

                b.HasIndex(x => x.RoleId);

                b.HasIndex(x => x.UserId);

                b.HasOne<IdentityRole>()
                    .WithMany()
                    .HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne<ApplicationUser>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.ToTable("AspNetUserRoles", "dbo");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens", "dbo");
            });

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.Property(x => x.ConcurrencyStamp)
                    .IsConcurrencyToken();

                b.Property(x => x.Email)
                    .HasMaxLength(256);

                b.Property(x => x.NormalizedEmail)
                    .HasMaxLength(256);

                b.Property(x => x.NormalizedUserName)
                    .HasMaxLength(256);

                b.Property(x => x.UserName)
                    .HasMaxLength(256);

                b.HasKey(x => x.Id);

                b.HasIndex(x => x.NormalizedEmail)
                    .HasName("EmailIndex");

                b.HasIndex(x => x.NormalizedUserName)
                    .IsUnique()
                    .HasName("UserNameIndex");

                b.ToTable("AspNetUsers", "dbo");
            });

            #endregion
        }
    }
}
