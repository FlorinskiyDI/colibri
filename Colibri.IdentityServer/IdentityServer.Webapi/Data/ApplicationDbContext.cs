using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Webapi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<ApplicationUserGroups> ApplicationUserGroups { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }

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

            modelBuilder.Entity<ApplicationUserGroups>(entity =>
            {
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

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_ToGroups");
            });

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

                b.Property(x => x.IsAdmin).HasDefaultValueSql("((0))");
                b.Property(x => x.DataEventRecordsRole).HasMaxLength(256);
                b.Property(x => x.SecuredFilesRole).HasMaxLength(256);
                b.Property(x => x.AccountExpires).HasDefaultValueSql("(sysdatetimeoffset())");

                b.ToTable("AspNetUsers", "dbo");
            });

            #endregion
        }
    }
}
