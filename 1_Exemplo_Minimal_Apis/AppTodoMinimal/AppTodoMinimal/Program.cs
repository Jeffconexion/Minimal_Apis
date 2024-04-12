using AppTodoMinimal.Config;
using AppTodoMinimal.Core.Request;
using AppTodoMinimal.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;

#region Configuration builder, swagger end database.
var app = GeneralConfiguration.ConfigureGeneralSettings(args);
#endregion

#region Endpoints for TODOS
app.MapGet("v1/get/todos", async (DataBaseContext context) =>
{
    try
    {
        var todos = await context.Todos.ToListAsync();
        return Results.Ok(todos);
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "Host terminated unexpectedly");
        return Results.Problem(ex.Message);
    }
    finally
    {
        Log.Information("Server Shutting down...");
        Log.CloseAndFlush();
    }

}).WithOpenApi()
.WithName("GetToDos")
.WithTags("Gets")
.WithSummary("Get All To dos")
.WithDescription("Endpoint to retrieve all tasks.")
.Produces((int)HttpStatusCode.OK)
.Produces((int)HttpStatusCode.InternalServerError);

app.MapGet("v1/get/{id}", async (DataBaseContext context, Guid id) =>
{

    try
    {
        var todo = await context.Todos.FindAsync(id);

        if (todo is null)
        {
            return Results.NotFound(todo);
        }

        return Results.Ok(todo);
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "Host terminated unexpectedly");
        return Results.Problem(ex.Message);
    }
    finally
    {
        Log.Information("Server Shutting down...");
        Log.CloseAndFlush();
    }

}).WithOpenApi()
  .WithName("GetTodoById")
  .WithTags("Gets")
  .WithSummary("Get a single task")
  .WithDescription("Users to get the details of a single task based on the provided ID.")
  .Produces((int)HttpStatusCode.OK)
  .Produces((int)HttpStatusCode.NotFound)
  .Produces((int)HttpStatusCode.InternalServerError);

app.MapPost("v1/post/todos", async (DataBaseContext context, CreateTodoRequest request, IValidator<CreateTodoRequest> validator) =>
{
    try
    {
        var validation = await validator.ValidateAsync(request);

        if (validation.IsValid is false)
        {
            return Results.ValidationProblem(validation.ToDictionary());
        }

        var todo = request.MapTo();

        await context.Todos.AddAsync(todo);
        await context.SaveChangesAsync();

        return Results.Created($"/v1/todos/{todo.Id}", todo);
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "Host terminated unexpectedly");
        return Results.Problem(ex.Message);
    }
    finally
    {
        Log.Information("Server Shutting down...");
        Log.CloseAndFlush();
    }

}).WithOpenApi()
.WithName("CreateTodo")
.WithTags("Posts")
.WithSummary("Create a new task")
.WithDescription("Endpoint to create a new task.")
.Produces((int)HttpStatusCode.OK)
.Produces((int)HttpStatusCode.NotFound)
.Produces((int)HttpStatusCode.InternalServerError);


app.MapPut("v1/put/todos/{id}", async (DataBaseContext context, Guid id, Todo newTodo) =>
{
    try
    {
        var oldTodo = await context.Todos.FindAsync(id);

        if (oldTodo is null)
        {
            return Results.NotFound();
        }

        oldTodo.Title = newTodo.Title;

        context.Todos.Update(oldTodo);
        await context.SaveChangesAsync();

        return Results.Ok(oldTodo);
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "Host terminated unexpectedly");
        return Results.Problem(ex.Message);
    }
    finally
    {
        Log.Information("Server Shutting down...");
        Log.CloseAndFlush();
    }

}).WithOpenApi()
.WithName("UpdateTodo")
.WithTags("Puts")
.WithSummary("Update a task")
.WithDescription("Endpoint to update an existing task based on the provided ID.")
.Produces((int)HttpStatusCode.OK)
.Produces((int)HttpStatusCode.NotFound)
.Produces((int)HttpStatusCode.InternalServerError);

app.MapDelete("v1/delete/todos/{id}", async (DataBaseContext context, Guid id) =>
{
    try
    {
        var todo = await context.Todos.FindAsync(id);

        if (todo is null)
        {
            return Results.NotFound();
        }
        context.Todos.Remove(todo);
        await context.SaveChangesAsync();

        return Results.Ok(todo);
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "Host terminated unexpectedly");
        return Results.Problem(ex.Message);
    }
    finally
    {
        Log.Information("Server Shutting down...");
        Log.CloseAndFlush();
    }

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
