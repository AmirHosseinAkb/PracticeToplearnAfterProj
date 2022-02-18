using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TopLearn.Data.Entities.Permission;
using TopLearn.Data.Entities.User;
using TopLearn.Data.Entities.Wallet;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Data.Context
{
    public  class TopLearnContext:DbContext
    {
        public TopLearnContext(DbContextOptions<TopLearnContext> options) : base(options)
        {
        
        }
        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion
        #region Wallet

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }

        #endregion
        #region Permission

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Course

        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }


        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);

            modelBuilder.Entity<Course>()
                .HasQueryFilter(c => !c.IsDeleted);

            modelBuilder.Entity<CourseStatus>()
                .HasQueryFilter(s => !s.IsDeleted);

            modelBuilder.Entity<CourseLevel>()
                .HasQueryFilter(l => !l.IsDeleted);

            modelBuilder.Entity<CourseGroup>()
                .HasQueryFilter(g => !g.IsDeleted);

            modelBuilder.Entity<CourseEpisode>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDeleted);

            modelBuilder.Entity<Course>()
                .HasOne<CourseGroup>(c=>c.CourseGroup)
                .WithMany(g=>g.Courses)
                .HasForeignKey(c=>c.GroupId);

            modelBuilder.Entity<Course>()
                .HasOne<CourseGroup>(c=>c.SubGroup)
                .WithMany(g=>g.SubCourses)
                .HasForeignKey(c=>c.SubId)
                .OnDelete(DeleteBehavior.Restrict);
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
