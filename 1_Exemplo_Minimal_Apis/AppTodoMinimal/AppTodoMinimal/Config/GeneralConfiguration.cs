using AppTodoMinimal.Config.ConfigServices;

namespace AppTodoMinimal.Config
{
    public static class GeneralConfiguration
    {
        public static WebApplication ConfigureGeneralSettings(string[] args)
        {
            //builder configuration
            var configApplication = new ConfigApplications();
            var builder = configApplication.ConfigurationBuilder(args);

            //swagger configuration
            var configServiceSwagger = new ConfigServiceSwagger();
            configServiceSwagger.ConfigureSwagger(builder);

            //injection configuration
            var configServiceInjeciton = new ConfigServiceInjeciton();
            configServiceInjeciton.ConfigureInjection(builder);

            //database configuration
            var configServiceDatabase = new ConfigServiceDatabase();
            configServiceDatabase.ConfigureDatabase(builder);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                configServiceSwagger.ConfigureSwaggerUse(app);
            }

            app.UseHttpsRedirection();
            return app;
        }
    }
}
