using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Models.CustomIdentity;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext<WebApplication1User, WebApplication1Role, string, IdentityUserClaim<string>,
        WebApplication1UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    //public class ApplicationDbContext : IdentityDbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Revendeur> Revendeurs { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<PosteTravail> PosteTravails { get; set; }
        public DbSet<Serveur> Serveurs { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<WebApplication1UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Revendeur>().ToTable("Revendeur");

            builder.Entity<Direction>().ToTable("Direction");

            builder.Entity<Employe>().ToTable("Employe");

            builder.Entity<PosteTravail>().ToTable("PosteTravail");

            builder.Entity<Serveur>().ToTable("Serveur");
        }
    }
}
