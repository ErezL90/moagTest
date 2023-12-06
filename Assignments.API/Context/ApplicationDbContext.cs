using Assignments.API.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Assignments.API.Context
{
    public class ApplicationDbContext : IdentityDbContext //<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AssignmentType> AssignmentTypes { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Assignment>()
            //    .HasOne(t => t.ApplicationUser)
            //    .WithMany(u => u.Assignments)
            //    .HasForeignKey(t => t.ApplicationUserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade); //when user deleted, all assignments for this user deleted..(Cascade)

            builder.Entity<Assignment>()
                .HasOne(t => t.AssignmentType)
                .WithMany(a => a.Assignments)
                .HasForeignKey(t => t.AssignmentTypeId)
                .IsRequired();

        }
    }
}
