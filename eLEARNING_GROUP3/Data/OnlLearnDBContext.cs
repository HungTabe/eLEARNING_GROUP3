using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data.Seeds;
using OnlineLearning.Models.Domains;
using OnlineLearning.Models.Domains.AIModels;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;
using OnlineLearning.Models.Domains.CourseModels.LessonModels;
using OnlineLearning.Models.Domains.CourseModels.QuizModels;
using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Models.Domains.UserCourseRelationship;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Data
{
    public class OnlLearnDBContext : DbContext
    {
        public OnlLearnDBContext(DbContextOptions<OnlLearnDBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseImageUrl> CourseImageUrls { get; set; }
        public DbSet<AdminReviewCourse> AdminReviewCourses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<CourseRating> CourseRatings { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserQuizResult> UserQuizResults { get; set; }
        public DbSet<DiscussionLesson> DiscussionLessons { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<AIReviewCourse> AIReviewCourses { get; set; }
        public DbSet<ChatbotMessage> ChatbotMessages { get; set; }
        public DbSet<AITrainingData> AITrainingData { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Notification> Notifications { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure composite keys
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            /* 
            Xác định khóa chính kép (composite key)
            Dù [Key] có thể làm điều này, Fluent API rõ ràng hơn và giúp EF hiểu đúng.
            */
            modelBuilder.Entity<CourseCategory>()
            .HasKey(cc => new { cc.CategoryId, cc.CourseId });

            modelBuilder.Entity<WishList>()
                .HasKey(w => new { w.UserId, w.CourseId });

            modelBuilder.Entity<CourseEnrollment>()
                .HasKey(ce => new { ce.UserId, ce.CourseId });

            // Configure relationships
            modelBuilder.Entity<User>()
            .HasOne(u => u.Wallet)
            .WithOne(w => w.User)
            .HasForeignKey<Wallet>(w => w.WalletId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.CreatorUser)
                .WithMany(u => u.CreatedCourses)
                .HasForeignKey(c => c.Creator)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.AcceptorUser)
                .WithMany(u => u.AcceptedCourses)
                .HasForeignKey(c => c.Acceptor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiscussionLesson>()
                .HasOne(d => d.ParentComment)
                .WithMany()
                .HasForeignKey(d => d.ParentCommentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa tin nhắn khi xóa user
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.User) // Người nhận chứng chỉ
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa chứng chỉ khi xóa user

            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.Issuer) // Người cấp chứng chỉ
                .WithMany()
                .HasForeignKey(c => c.IssuerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.Course) // Khóa học liên quan
                .WithMany(c => c.Certificates)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Cascade); // Nếu xóa khóa học thì xóa luôn chứng chỉ



            // Configure cascade delete behavior
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Modules)
                .WithOne(m => m.Course)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Module>()
                .HasMany(m => m.Lessons)
                .WithOne(l => l.Module)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Module>()
                .HasMany(m => m.Quizzes)
                .WithOne(q => q.Module)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.Questions)
                .WithOne(q => q.Quiz)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Options)
                .WithOne(o => o.Question)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure default values and constraints
            modelBuilder.Entity<User>()
                .Property(u => u.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                .Property(u => u.IsActived)
                .HasDefaultValue(true);

            modelBuilder.Entity<Course>()
                .Property(c => c.Status)
                .HasDefaultValue(Enums.CourseStatus.Draft);

            modelBuilder.Entity<Wallet>()
                .Property(w => w.Balance)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            modelBuilder.Entity<Wallet>()
                .Property(w => w.CurrencyCode)
                .HasDefaultValue("VND");

            // Configure indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Phone)
                .IsUnique();

            modelBuilder.Entity<TransactionHistory>()
                .HasIndex(t => t.ExternalTransactionId)
                .IsUnique();

            // Configure soft delete filter
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);


            modelBuilder.Entity<Notification>()
               .HasOne(n => n.Course)
               .WithMany(c => c.Notifications) 
               .HasForeignKey(n => n.CourseId)
               .OnDelete(DeleteBehavior.Cascade); // Xóa thông báo nếu khóa học bị xóa

            // Seed initial data
            Role admin = new Role { RoleId = 1, RoleName = Enums.RoleType.ADMIN };
            Role mentor = new Role { RoleId = 2, RoleName = Enums.RoleType.MENTOR };
            Role mentee = new Role { RoleId = 3, RoleName = Enums.RoleType.MENTEE };
            modelBuilder.Entity<Role>().HasData(
                admin,
                mentor,
                mentee
            );
            User user1 = new User {UserId = 1, FullName = "My Admin", Email = "admin@admin.com", Password = "123456", Dob = DateOnly.Parse("06-07-2003"), Phone = "0000000000", Gender = true };
            User user2 = new User {UserId = 2, FullName = "My Admin 2", Email = "admin2@admin.com", Password = "123456", Dob = DateOnly.Parse("06-07-2003"), Phone = "0000000001", Gender = true };
            modelBuilder.Entity<User>().HasData(
                user1,
                user2
            );
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 2, RoleId = 1 }
            );
            //seeding data language
            modelBuilder.ApplyConfiguration(new LanguageSeedConfiguration());
            modelBuilder.ApplyConfiguration(new AITrainingDataSeedConfiguration());
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && e.State is EntityState.Added or EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = DateTime.Now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.UpdatedAt = DateTime.Now;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
