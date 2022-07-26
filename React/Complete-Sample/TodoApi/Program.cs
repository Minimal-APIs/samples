using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(options => options.UseInMemoryDatabase("items"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/api/todos", async (TodoDb db) => await db.Todos.ToListAsync());
app.MapPost("/api/todos", async(TodoDb db, TodoItem todo) =>
{
    await db.Todos.AddAsync(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todo/{todo.Id}", todo);
}
);

app.Run();

class TodoItem
{
   public int Id { get; set; }
    public string? Item { get; set; }
    public bool IsComplete { get; set; }

}

class TodoDb : DbContext
{
    public TodoDb(DbContextOptions options) : base(options) { }
    public DbSet<TodoItem> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Todos");
    }
}
