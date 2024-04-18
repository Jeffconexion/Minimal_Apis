namespace AppTodoMinimal.Endpoints
{
    public static class TodoEndpoints
    {
        public static void MapTodoEndpoints(this IEndpointRouteBuilder app)
        {


            app.MapGet("list-all", async (DataBaseContext context) =>
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
              .WithName("GetToDos Depreciated")
              .WithTags("Gets")
              .WithSummary("Get All To dos")
              .WithDescription("Endpoint to retrieve all tasks.")
              .Produces((int)HttpStatusCode.OK)
              .Produces((int)HttpStatusCode.InternalServerError)
              .MapToApiVersion(1);

            app.MapGet("list-all", async (DataBaseContext context) =>
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
              .Produces((int)HttpStatusCode.InternalServerError)
              .MapToApiVersion(2);


            app.MapGet("list/{id}", async (DataBaseContext context, Guid id) =>
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
              .Produces((int)HttpStatusCode.InternalServerError)
              .MapToApiVersion(1);


            app.MapPost("add-new", async (DataBaseContext context, CreateTodoRequest request, IValidator<CreateTodoRequest> validator) =>
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


            app.MapPut("update/{id}", async (DataBaseContext context, Guid id, Todo newTodo) =>
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


            app.MapDelete("remove/{id}", async (DataBaseContext context, Guid id) =>
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
        }
    }
}
