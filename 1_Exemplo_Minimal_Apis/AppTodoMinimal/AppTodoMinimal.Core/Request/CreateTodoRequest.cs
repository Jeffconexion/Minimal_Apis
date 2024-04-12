namespace AppTodoMinimal.Core.Request
{
    public class CreateTodoRequest
    {
        public string Title { get; set; }
        public Todo MapTo()
        {
            return new Todo(Guid.NewGuid(), Title, false);
        }
    }
}
