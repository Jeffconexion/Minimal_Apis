#region Criando Aplicação
using AppTodoMinimal.Data;
using AppTodoMinimal.ViewModels;

var builder = WebApplication.CreateBuilder(args);
//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//banco
builder.Services.AddDbContext<DataBaseContext>();
var app = builder.Build();

//swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

app.UseHttpsRedirection();

#region Endpoints for TODOS

app.MapGet("v1/get/todos", (DataBaseContext context) =>
{
    //var todos = new Todo(Guid.NewGuid(), "Ir a academia", false);
    var todos = context.Todos.ToList();
    return Results.Ok(todos);
})
  .WithName("GetTodos")
  .WithOpenApi();

app.MapPost("v1/post/todos", (DataBaseContext context, CreateTodoViewModel model) =>
{
    var todo = model.MapTo();
    if (model.IsValid is false)
    {
        return Results.BadRequest(model.Notifications);
    }

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{todo.Id}", todo);
})
  .WithName("PostTodos")
  .Produces<Todo>()
  .WithOpenApi();

#endregion


#region Executando aplicação

app.Run();
#endregion
