using AppTodoMinimal.Config;
using AppTodoMinimal.Core.Request;
using AppTodoMinimal.Data;
using System.Net;
using System.Net.Mime;

#region Configuration builder, swagger end database.
var app = GeneralConfiguration.ConfigureGeneralSettings(args);
#endregion

#region Endpoints for TODOS
app.MapGet("v1/get/todos", (DataBaseContext context) =>
{

    var todos = context.Todos.ToList();
    return Results.Ok(todos);

}).WithOpenApi()
.WithName("GetToDos")
.WithTags("Gets")
.WithSummary("Get All To dos")
.WithDescription("Endpoint to retrieve all tasks.")
.Produces((int)HttpStatusCode.OK)
.Produces((int)HttpStatusCode.InternalServerError);

app.MapGet("v1/get/{id}", (DataBaseContext context, Guid id) =>
{
    var todo = context.Todos.Find(id);

    if (todo is null)
    {
        return Results.NotFound(todo);
    }

    return Results.Ok(todo);

}).WithOpenApi()
  .WithName("GetTodoById")
  .WithTags("Gets")
  .WithSummary("Get a single task")
  .WithDescription("Users to get the details of a single task based on the provided ID.")
  .Produces((int)HttpStatusCode.OK)
  .Produces((int)HttpStatusCode.NotFound)
  .Produces((int)HttpStatusCode.InternalServerError);

app.MapPost("v1/post/todos", (DataBaseContext context, CreateTodoRequest request) =>
{
    var todo = request.MapTo();

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{todo.Id}", todo);

}).WithOpenApi()
.WithName("CreateTodo")
.WithTags("Posts")
.WithSummary("Create a new task")
.WithDescription("Endpoint to create a new task.")
.Produces((int)HttpStatusCode.OK)
.Produces((int)HttpStatusCode.NotFound)
.Produces((int)HttpStatusCode.InternalServerError);


app.MapPut("v1/put/todos/{id}", (DataBaseContext context, Guid id, Todo newTodo) =>
{
    var oldTodo = context.Todos.Find(id);

    if (oldTodo is null)
    {
        return Results.NotFound();
    }

    oldTodo.Title = newTodo.Title;

    context.Todos.Update(oldTodo);
    context.SaveChanges();

    return Results.Ok(oldTodo);

}).WithOpenApi()
.WithName("UpdateTodo")
.WithTags("Puts")
.WithSummary("Update a task")
.WithDescription("Endpoint to update an existing task based on the provided ID.")
.Produces((int)HttpStatusCode.OK)
.Produces((int)HttpStatusCode.NotFound)
.Produces((int)HttpStatusCode.InternalServerError);

app.MapDelete("v1/delete/todos/{id}", (DataBaseContext context, Guid id) =>
{
    var todo = context.Todos.Find(id);

    if (todo is null)
    {
        return Results.NotFound();
    }
    context.Todos.Remove(todo);
    context.SaveChanges();

    return Results.Ok(todo);

}).WithOpenApi()
.WithName("DeleteTodo")
.WithTags("Deletes")
.WithSummary("Delete a task")
.WithDescription("Endpoint to delete an existing task based on the provided ID.")
.Produces((int)HttpStatusCode.OK)
.Produces((int)HttpStatusCode.NotFound)
.Produces((int)HttpStatusCode.InternalServerError);

#endregion

#region Running application
app.Run();
#endregion
