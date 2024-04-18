namespace AppTodoMinimal.Config.ConfigServices
{
    public static class ConfigServiceDatabase
    {
        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
