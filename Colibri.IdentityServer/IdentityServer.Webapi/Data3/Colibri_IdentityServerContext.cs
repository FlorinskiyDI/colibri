using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IdentityServer.Webapi.Data3
{
    public partial class Colibri_IdentityServerContext : DbContext
    {
        public virtual DbSet<ApplicationUserGroups> ApplicationUserGroups { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<GroupNodes> GroupNodes { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"data source=SB-153\SQLEXPRESS ;Initial Catalog=Colibri.IdentityServer; Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUserGroups>(entity =>
            {
                entity.HasIndex(e => e.GroupId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ApplicationUserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserGroups_ToGroups");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApplicationUserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserGroups_ToAspNetUsers");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountExpires).HasDefaultValueSql("(sysdatetimeoffset())");
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<GroupNodes>(entity =>
            {
                entity.HasKey(e => new { e.AncestorId, e.OffspringId });

                entity.HasIndex(e => e.OffspringId);

                entity.HasOne(d => d.Ancestor)
                    .WithMany(p => p.GroupNodesAncestor)
                    .HasForeignKey(d => d.AncestorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupNodes_ToAncestor");

                entity.HasOne(d => d.Offspring)
                    .WithMany(p => p.GroupNodesOffspring)
                    .HasForeignKey(d => d.OffspringId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupNodes_ToOffspring");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasIndex(e => e.ParentId);

                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Groups_ToGroups");
            });
        }
    }
}
