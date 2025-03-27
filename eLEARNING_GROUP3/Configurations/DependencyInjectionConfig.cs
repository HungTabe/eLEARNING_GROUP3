using OnlineLearning.Models.Momo;
using OnlineLearning.Repositories.Implementations;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Implementations;
using OnlineLearning.Services.Implementations.Admin;
using OnlineLearning.Services.Interfaces;
using OnlineLearning.Services.Interfaces.Admin;
using Microsoft.Extensions.Configuration;
using OnlineLearning.Services.Implementations.AI;
using OnlineLearning.Services.Interfaces.AI;
using OnlineLearning.Repositories.Implementations.AI;
using OnlineLearning.Repositories.Interfaces.AI;

namespace OnlineLearning.Configurations
{
    public static class DependencyInjectionConfig
    {
        //DI 
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            //faq
            services.AddScoped<IFAQsRepository, FAQsRepository>();
            services.AddScoped<IFAQsService, FAQsService>();

            //quiz
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IQuizService, QuizService>();


            //auth
            services.AddSession();
            services.AddScoped<IEmailService, EmailService>();
            
            //Admin

            services.AddScoped<IUserManagementService, UserManagementServiceImpl>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IMyCourseRepository, MyCourseRepository>();
            services.AddScoped<IMyCourseService, MyCourseService>();

            //Notification
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();


            //chatbot
            services.AddScoped<IChatbotRepository, ChatbotRepository>();
            services.AddScoped<IChatbotService, ChatbotService>();
            services.AddScoped<IAITrainingRepository, AITrainingRepository>();
            services.AddScoped<IAITrainingService, AITrainingService>();
            services.AddHttpClient<IOpenAIEmbeddingService, OpenAIEmbeddingService>();

            //level
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<ILevelService, LevelService>();

            //language
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILanguageService, LanguageService>();

            //course
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseService>();

            //category
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            //courseImage
            services.AddScoped<ICourseImageRepository, CourseImageRepository>();
            services.AddScoped<ICourseImageService, CourseImageService>();

            //courseCategories
            services.AddScoped<ICourseCategoriesRepository, CourseCategoriesRepository>();
            services.AddScoped<ICourseCategoriesService, CourseCategoriesService>();

            // Cấu hình MoMoConfig từ appsettings.json
            services.Configure<MoMoConfig>(configuration.GetSection("MomoAPI"));
            services.AddScoped<IMomoService, MomoService>();

            //module
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IModuleService, ModuleService>();

            //lesson
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonService, LessonService>();

            //Wishlist
            services.AddScoped<IWishlistRepository, WishlistRepository>();

            //course enrollment
            services.AddScoped<ICourseEnrollentRepository, CourseEnrollmentRepository>();
            services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();

            //transaction history
            services.AddScoped<ITransactionRepository, TransactionRepositoty>();
            services.AddScoped<ITransactionService, TransactionService>();

            //userRole
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleService>();

            //message
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}
