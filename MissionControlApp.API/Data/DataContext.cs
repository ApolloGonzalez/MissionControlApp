using MissionControlApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MissionControlApp.API.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, 
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext>  options) : base (options) {}

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<MissionAccelerator> MissionAccelerators { get; set; }
        public DbSet<Accelerator> Accelerators { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<MissionPlatform> MissionPlatforms { get; set; }
        public DbSet<BusinessFunction> BusinessFunctions { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<MissionTeam> MissionTeams { get; set; }
        public DbSet<MissionAssessment> MissionAssessments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole => 
            {
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Mission>()
                .HasOne(a => a.MissionAssessment)
                .WithOne(b => b.Mission)
                // .HasForeignKey<Mission>(ma => ma.Id)
                .OnDelete(DeleteBehavior.Restrict);

           /*  builder.Entity<MissionAccelerator>().HasKey(ma => new {ma.MissionId, ma.AcceleratorId});

            builder.Entity<MissionAccelerator>()
                .HasOne(ma => ma.Accelerator)
                .WithMany(ma => ma.MissionAccelerators)
                .HasForeignKey(ma => ma.AcceleratorId)
                .OnDelete(DeleteBehavior.SetNull);
                
            builder.Entity<MissionAccelerator>()
                .HasOne(ma => ma.Mission)
                .WithMany(a => a.MissionAccelerators)
                .HasForeignKey(ma => ma.MissionId);  */

            builder.Entity<MissionTeam>()
                .HasOne(mt => mt.Mission)
                .WithMany(mt => mt.MissionTeam)
                .HasForeignKey(ma => ma.MissionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MissionAccelerator>()
                .HasOne(ma => ma.Mission)
                .WithMany(ma => ma.MissionAccelerators)
                .HasForeignKey(ma => ma.MissionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MissionPlatform>()
                .HasOne(mp => mp.Mission)
                .WithMany(mp => mp.MissionPlatforms)
                .HasForeignKey(mp => mp.MissionId)
                .OnDelete(DeleteBehavior.Restrict);                

           /*  builder.Entity<MissionAccelerator>()
                .HasOne(ma => ma.Accelerator)
                .WithOne(m => m.MissionAccelerator)
                .HasForeignKey<MissionAccelerator>(ma => ma.AcceleratorId)
                .OnDelete(DeleteBehavior.Restrict); 
 */
            /*  builder.Entity<Mission>()
                .HasOne(b => b.BusinessFunction)
                .WithOne(m => m.Mission)
                .HasForeignKey<Mission>(m => m.BusinessFunctionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Mission>()
                .HasOne(i => i.Industry)
                .WithOne(m => m.Mission)
                .HasForeignKey<Mission>(m => m.IndustryId)
                .OnDelete(DeleteBehavior.Restrict);    */              

            builder.Entity<Like>()
                .HasKey(k => new {k.LikerId, k.LikeeId});

            builder.Entity<Like>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Photo>().HasQueryFilter(p => p.IsApproved);
        }
    }
}