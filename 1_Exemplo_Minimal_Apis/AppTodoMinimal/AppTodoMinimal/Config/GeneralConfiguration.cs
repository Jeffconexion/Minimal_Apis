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
            builder.ConfigureSwagger();

            //injection configuration
            builder.ConfigureInjection();

            //database configuration
            builder.ConfigureDatabase();

            //CORS configuration
            builder.ConfigurePolicyGeneric();

            //Version api configuration
            builder.ConfigureVersionApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.ConfigureSwaggerUse();
            }

            app.UseHttpsRedirection();
            return app;
        }
    }
}
