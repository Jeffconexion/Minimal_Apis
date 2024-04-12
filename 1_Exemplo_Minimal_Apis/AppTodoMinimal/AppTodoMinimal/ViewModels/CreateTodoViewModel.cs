using Flunt.Notifications;
using Flunt.Validations;

namespace AppTodoMinimal.ViewModels
{
    public class CreateTodoViewModel : Notifiable<Notification>
    {
        public string Title { get; set; }

        public Todo MapTo()
        {
            var contract = new Contract<Notification>()
                            .Requires()
                            .IsNotNull(Title, "Informe o título da tarefa")
                            .IsGreaterThan(Title, 5, "O título deve conter mais de 5 caracters");
            AddNotifications(contract);

            return new Todo(Guid.NewGuid(), Title, false);
        }
    }
}
