using Microsoft.EntityFrameworkCore;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .HasKey(x => x.RoleId);

            modelBuilder.Entity<User>()
                .HasKey(x => x.UserId);

            modelBuilder.Entity<Team>()
                .HasKey(x => x.TeamId);

            modelBuilder.Entity<TeamMember>()
                .HasKey(x => x.TeamMemberId);

            modelBuilder.Entity<TaskItem>()
                .HasKey(x => x.TaskId);

            modelBuilder.Entity<Comment>()
                .HasKey(x => x.CommentId);

            modelBuilder.Entity<Notification>()
                .HasKey(x => x.NotificationId);
        }
    }
}
