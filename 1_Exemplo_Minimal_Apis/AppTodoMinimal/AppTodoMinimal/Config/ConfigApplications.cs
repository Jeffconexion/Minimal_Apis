namespace AppTodoMinimal.Config
{
    public class ConfigApplications
    {
        public  WebApplicationBuilder ConfigurationBuilder(string[] args)
        {
            return WebApplication.CreateBuilder(args);
        }
    }
}
