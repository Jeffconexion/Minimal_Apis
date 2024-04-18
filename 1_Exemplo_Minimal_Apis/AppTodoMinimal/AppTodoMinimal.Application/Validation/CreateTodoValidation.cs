namespace AppTodoMinimal.Application.Validation
{
    public class CreateTodoValidation : AbstractValidator<CreateTodoRequest>
    {
        public CreateTodoValidation()
        {
            RuleFor(t => t.Title)
                                .NotEmpty()
                                .NotNull()
                                .MinimumLength(3)
                                .WithMessage("Title is required!");
        }
    }
}
