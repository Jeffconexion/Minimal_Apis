namespace AppTodoMinimal.Config.ConfigServices
{
    public static class ConfigServiceInjeciton
    {
        public static void ConfigureInjection(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<CreateTodoRequest>, CreateTodoValidation>();
            builder.Services.AddSwaggerGen();
            builder.AddSerilogApi(builder.Configuration);
        }
    }
}
