#region Configuration builder, swagger end database.
var app = GeneralConfiguration.ConfigureGeneralSettings(args);

#endregion

#region Endpoints for TODOS
app.MapTodoEndpoints();

#endregion

#region Running application
app.Run();

#endregion
