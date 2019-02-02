using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;
using dataaccesscore.Abstractions.Context;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;

namespace IdentityServer.Webapi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
        IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>,
        ApplicationRoleClaim, IdentityUserToken<Guid>>, IEntityContext
    {
        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] {
                new ConsoleLoggerProvider((category, level)
                    => category == DbLoggerCategory.Database.Command.Name
                       && level == LogLevel.Debug, true)
            });

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual DbSet<ApplicationRoleClaim> ApplicationRoleClaim { get; set; }
        public virtual DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public virtual DbSet<MemberGroups> MemberGroups { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<GroupNode> GroupNode { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(SqlConnectionFactory.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupNode>(b =>
            {
                b.HasKey(p => new { p.AncestorId, p.OffspringId });
            });

            modelBuilder.Entity<MemberGroups>(entity =>
            {   
                entity.HasIndex(p => new { p.UserId, p.GroupId }).IsUnique();
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.MemberGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberGroups_ToGroups");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MemberGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberGroups_ToAspNetUsers");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_ToGroups");

                entity
                    .HasMany(p => p.Ancestors)
                    .WithOne(d => d.Offspring)
                    .HasForeignKey(d => d.OffspringId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ancestor_ToOffspring");

                entity
                    .HasMany(p => p.Offspring)
                    .WithOne(d => d.Ancestor)
                    .HasForeignKey(d => d.AncestorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offspring_ToAncestor");
            });



            //#region AspNetCore Identity
            //modelBuilder
            //    .HasAnnotation("ProductVersion", "1.0.0-rc3")
            //    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            //modelBuilder.Entity<ApplicationRole>(b =>
            //{
            //    // Each Role can have many entries in the UserRole join table
            //    //b.HasMany(e => e.UserRoles)
            //    //    .WithOne(e => e.Role)
            //    //    .HasForeignKey(ur => ur.RoleId)
            //    //    .IsRequired();

            //    b.Property(x => x.ConcurrencyStamp)
            //        .IsConcurrencyToken();

            //    b.Property(x => x.Name)
            //        .HasMaxLength(256);

            //    b.Property(x => x.NormalizedName)
            //        .HasMaxLength(256);

            //    b.HasKey(x => x.Id);

            //    b.HasIndex(x => x.NormalizedName)
            //        .HasName("RoleNameIndex");

            //    b.ToTable("AspNetRoles", "dbo");
            //});

            //modelBuilder.Entity<ApplicationRoleClaim>(b =>
            //{
            //    b.Property(x => x.Id)
            //        .ValueGeneratedOnAdd();

            //    b.Property(x => x.RoleId)
            //        .IsRequired();

            //    b.HasKey(x => x.Id);

            //    b.HasIndex(x => x.RoleId);

            //    b.HasOne<ApplicationRole>()
            //        .WithMany()
            //        .HasForeignKey(x => x.RoleId)
            //        .OnDelete(DeleteBehavior.Cascade);

            //    b.ToTable("AspNetRoleClaims", "dbo");
            //});

            //modelBuilder.Entity<IdentityUserClaim<Guid>>(b =>
            //{
            //    b.Property(x => x.Id)
            //        .ValueGeneratedOnAdd();

            //    b.Property(x => x.UserId)
            //        .IsRequired();

            //    b.HasKey(x => x.Id);

            //    b.HasIndex(x => x.UserId);

            //    b.HasOne<ApplicationUser>()
            //        .WithMany()
            //        .HasForeignKey(x => x.UserId)
            //        .OnDelete(DeleteBehavior.Cascade);

            //    b.ToTable("AspNetUserClaims", "dbo");
            //});

            //modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
            //{
            //    b.Property(x => x.UserId)
            //        .IsRequired();

            //    b.HasKey("LoginProvider", "ProviderKey");

            //    b.HasIndex(x => x.UserId);

            //    b.HasOne<ApplicationUser>()
            //        .WithMany()
            //        .HasForeignKey(x => x.UserId)
            //        .OnDelete(DeleteBehavior.Cascade);

            //    b.ToTable("AspNetUserLogins", "dbo");
            //});

            //modelBuilder.Entity<ApplicationUserRole>(b =>
            //{
            //    b.HasKey("UserId", "RoleId");

            //    b.HasIndex(x => x.RoleId);

            //    b.HasIndex(x => x.UserId);

            //    b.HasOne<ApplicationRole>()
            //        .WithMany()
            //        .HasForeignKey(x => x.RoleId)
            //        .OnDelete(DeleteBehavior.Cascade);

            //    b.HasOne<ApplicationUser>()
            //        .WithMany()
            //        .HasForeignKey(x => x.UserId)
            //        .OnDelete(DeleteBehavior.Cascade);

            //    b.ToTable("AspNetUserRoles", "dbo");
            //});

            //modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
            //{
            //    b.HasKey("UserId", "LoginProvider", "Name");

            //    b.ToTable("AspNetUserTokens", "dbo");
            //});

            //modelBuilder.Entity<ApplicationUser>(b =>
            //{
            //    //b.HasMany(e => e.Claims)
            //    //.WithOne()
            //    //.HasForeignKey(uc => uc.UserId)
            //    //.IsRequired();

            //    //// Each User can have many UserLogins
            //    //b.HasMany(e => e.Logins)
            //    //    .WithOne()
            //    //    .HasForeignKey(ul => ul.UserId)
            //    //    .IsRequired();

            //    //// Each User can have many UserTokens
            //    //b.HasMany(e => e.Tokens)
            //    //    .WithOne()
            //    //    .HasForeignKey(ut => ut.UserId)
            //    //    .IsRequired();

            //    //// Each User can have many entries in the UserRole join table
            //    //b.HasMany(e => e.UserRoles)
            //    //    .WithOne()
            //    //    .HasForeignKey(ur => ur.UserId)
            //    //    .IsRequired();

            //    b.Property(x => x.ConcurrencyStamp)
            //        .IsConcurrencyToken();

            //    b.Property(x => x.Email)
            //        .HasMaxLength(256);

            //    b.Property(x => x.NormalizedEmail)
            //        .HasMaxLength(256);

            //    b.Property(x => x.NormalizedUserName)
            //        .HasMaxLength(256);

            //    b.Property(x => x.UserName)
            //        .HasMaxLength(256);

            //    b.HasKey(x => x.Id);

            //    b.HasIndex(x => x.NormalizedEmail)
            //        .HasName("EmailIndex");

            //    b.HasIndex(x => x.NormalizedUserName)
            //        .IsUnique()
            //        .HasName("UserNameIndex");

            //    b.Property(x => x.IsAdmin).HasDefaultValueSql("((0))");
            //    b.Property(x => x.DataEventRecordsRole).HasMaxLength(256);
            //    b.Property(x => x.SecuredFilesRole).HasMaxLength(256);
            //    b.Property(x => x.AccountExpires).HasDefaultValueSql("(sysdatetimeoffset())");

            //    b.ToTable("AspNetUsers", "dbo");
            //});

            //#endregion


            #region AspNetCore Identity
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<ApplicationRole>(b =>
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

            modelBuilder.Entity< ApplicationRoleClaim>(b =>
            {
                b.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                b.Property(x => x.RoleId)
                    .IsRequired();

                b.HasKey(x => x.Id);

                b.HasIndex(x => x.RoleId);

                b.HasOne<ApplicationRole>()
                    .WithMany()
                    .HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.ToTable("AspNetRoleClaims", "dbo");
            });

            modelBuilder.Entity<IdentityUserClaim<Guid>>(b =>
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

            modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
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

            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                //b.HasKey("UserId", "RoleId");

                b.HasKey(c => new { c.UserId, c.RoleId, c.GroupId });

                b.HasIndex(x => x.RoleId);

                b.HasIndex(x => x.UserId);

                b.HasOne<ApplicationRole>()
                    .WithMany()
                    .HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne<ApplicationUser>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);


                b.HasOne(d => d.Group)
                    .WithMany(p => p.ApplicationUserRoles)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserRole_ToGroups");

                b.ToTable("AspNetUserRoles", "dbo");
            });

            modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
            {
                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens", "dbo");
            });

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many entries in the UserRole join table
                //b.HasMany(e => e.UserRoles)
                //    .WithOne()
                //    .HasForeignKey(ur => ur.UserId)
                //    .IsRequired();

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
