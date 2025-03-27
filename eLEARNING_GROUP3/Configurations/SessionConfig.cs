namespace OnlineLearning.Configurations
{
    public static class SessionConfig
    {
        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache(); // Lưu cache trong bộ nhớ
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn Session
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }
    }
}
