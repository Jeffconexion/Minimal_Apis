namespace AppTodoMinimal.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

    }
}
