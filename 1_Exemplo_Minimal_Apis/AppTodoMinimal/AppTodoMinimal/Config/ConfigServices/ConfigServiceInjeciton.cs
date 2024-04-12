using AppTodoMinimal.Application.Validation;
using AppTodoMinimal.Core.Request;
using FluentValidation;

namespace AppTodoMinimal.Config.ConfigServices
{
    public class ConfigServiceInjeciton
    {
        public void ConfigureInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<CreateTodoRequest>, CreateTodoValidation>();
            builder.Services.AddSwaggerGen();
        }
    }
}
