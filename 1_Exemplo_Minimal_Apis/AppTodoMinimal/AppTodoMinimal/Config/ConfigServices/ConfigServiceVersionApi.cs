namespace AppTodoMinimal.Config.ConfigServices
{
    public static class ConfigServiceVersionApi
    {
        public static void ConfigureVersionApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(op =>
            {
                op.DefaultApiVersion = new ApiVersion(1);
                op.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(op =>
            {
                op.GroupNameFormat = "'v'V";
                op.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
