using Domain.ValueObjects.Base;

namespace Domain.ValueObjects.Validators;

public class AuthorNameValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Имя автора не может быть пустым.");

        if (value.Length < 2)
            throw new ArgumentException("Имя автора должно содержать не менее 2 символов.");

        if (value.Length > 100)
            throw new ArgumentException("Имя автора не может превышать 100 символов.");
    }
}
