using AppTodoMinimal.Data;

namespace AppTodoMinimal.Config.ConfigServices
{
    public class ConfigServiceSwagger
    {
        public void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataBaseContext>();
            builder.Services.AddSwaggerGen(x =>
            {
                x.EnableAnnotations();
            });
        }
        public void ConfigureSwaggerUse(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
