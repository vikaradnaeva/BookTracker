
using Domain.ValueObjects.Base;

namespace Domain.ValueObjects.Validators;

public class ReaderNameValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Имя читателя не может быть пустым.");

        if (value.Length < 2)
            throw new ArgumentException("Имя читателя должно содержать не менее 2 символов");

        if (value.Length > 50)
            throw new ArgumentException("Имя читателя не может превышать 50 символов.");
    }
}
