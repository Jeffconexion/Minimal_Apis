#region Configuration builder, swagger end database.
using Asp.Versioning.Builder;

var app = GeneralConfiguration.ConfigureGeneralSettings(args);

#endregion

#region Endpoints for TODOS
ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1))
                .HasApiVersion(new ApiVersion(2))
                .ReportApiVersions()
                .Build();

RouteGroupBuilder versionedGroup = app.MapGroup("api/v{apiVersion:apiVersion}/")
                                    .WithApiVersionSet(apiVersionSet);


versionedGroup.MapTodoEndpoints();

#endregion

#region Running application
app.Run();

#endregion
