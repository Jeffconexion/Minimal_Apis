namespace AppTodoMinimal.Config.ConfigServices
{
    public static class ConfigServicePolicy
    {
        public static void ConfigPolicyGeneric(this WebApplicationBuilder builder)
        {
            var MyAllowSpecificOrigins = "_app_policy";
            builder.Services.AddCors(op =>
            {
                op.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://example.com",
                                           "http://www.contoso.com")
                                           .AllowAnyHeader()
                                           .AllowAnyMethod();
                    });
            });
        }
    }
}
