using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsPortal3.Models;
using NewsPortal3.Models.Identity;
using NewsPortal3.Models.ViewModels;
using System;

namespace NewsPortal3.Data
{
    public class DataContext : IdentityDbContext<User, Role, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<NewsViewModel> News { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<User>()
        //        .HasIndex(u => u.Login)
        //        .IsUnique();
        //    base.OnModelCreating(builder);
        //}
    }
}
