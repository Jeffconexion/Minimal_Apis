using AppTodoMinimal.Data;

namespace AppTodoMinimal.Config.ConfigServices
{
    public class ConfigServiceDatabase
    {
        public void ConfigureDatabase(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
