namespace AppTodoMinimal.Config.ConfigServices
{
    public static class ConfigServiceSwagger
    {
        public static void ConfigureSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataBaseContext>();
            builder.Services.AddSwaggerGen(x =>
            {
                x.EnableAnnotations();
            });
        }
        public static void ConfigureSwaggerUse(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
