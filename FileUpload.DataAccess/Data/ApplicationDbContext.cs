
using FileUpload.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUpload.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FileDescription> FileDescriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>().HasKey(u => new { u.UserId, u.RoleId });
            modelBuilder.Entity<UserRole>().HasOne(u => u.Role).WithMany(r => r.UserRoles).HasForeignKey(f => f.RoleId).IsRequired();
            modelBuilder.Entity<UserRole>().HasOne(u => u.User).WithMany(r => r.UserRoles).HasForeignKey(f => f.UserId).IsRequired();

            modelBuilder.Entity<FileDescription>().HasKey(u => new { u.Id});
            modelBuilder.Entity<FileDescription>().HasOne(u => u.User).WithMany(r => r.FileDescriptions).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
