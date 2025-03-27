namespace OnlineLearning.Configurations
{
    public static class AppSettingsConfig
    {
        public static IConfigurationBuilder ConfigureAppSettings(this IConfigurationBuilder builder, WebApplicationBuilder webBuilder)
        {
            return builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{webBuilder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}