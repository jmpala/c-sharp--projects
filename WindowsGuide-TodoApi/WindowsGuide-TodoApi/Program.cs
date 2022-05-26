using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/todoitems", async (TodoDb db) 
    => await db.Todos.Select(x => new TodoItemDTO(x)).ToListAsync());

app.MapGet("/todoitems/complete", async (TodoDb db) 
    => await db.Todos.Where(t => t.IsCompleted).ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, TodoDb db)
    => await db.Todos.FindAsync(id) is Todo todo
    ? Results.Ok(new TodoItemDTO(todo))
    : Results.NotFound());

app.MapPost("/todoitems", async (TodoItemDTO todoItemDTO, TodoDb db) =>
{
    var todoItem = new Todo
    {
        IsCompleted = todoItemDTO.IsCompleted,
        Name = todoItemDTO.Name
    };
    db.Todos.Add(todoItem);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todoItem.Id}", new TodoItemDTO(todoItem));
});

app.MapPut("/todoitems/{id}", async (int id, TodoItemDTO todoItemDTO, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = todoItemDTO.Name;
    todo.IsCompleted = todoItemDTO.IsCompleted;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(new TodoItemDTO(todo));
    }

    return Results.NotFound();
});

app.Run();


// Context
class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options) : base(options)
    {

    }

    public DbSet<Todo> Todos { get; set; }
}

// Model
class Todo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsCompleted { get; set; }
    public string? Secret { get; set; }
}

// DTO
class TodoItemDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsCompleted { get; set; }

    public TodoItemDTO() { }

    public TodoItemDTO(Todo todoItem)
        => (Id, Name, IsCompleted) = (todoItem.Id, todoItem.Name, todoItem.IsCompleted);
}