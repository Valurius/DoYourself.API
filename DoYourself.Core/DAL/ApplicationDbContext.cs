using DoYourself.Core.DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace DoYourself.Core.DAL
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            if (!Users.Any(u => u.Email == "Admin@gmail.com"))
            {
                var newUser = new User("Admin@gmail.com", "1234");
       
                Users.Add(newUser);
                SaveChanges();
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamTask> TeamTasks { get; set; }
        public DbSet<TeamUser> TeamUsers { get; set; }
        public DbSet<Award> Awards{ get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStatistic> ProjectStatistics { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Purchase> Purchases { get; set; }


    }

}

 
