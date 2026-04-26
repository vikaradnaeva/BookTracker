
using Domain.ValueObjects.Base;

namespace Domain.ValueObjects.Validators;

public class ModeratorNameValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Имя модератора не может быть пустым.");

        if (value.Length < 2)
            throw new ArgumentException("Имя модератора должно содержать не менее 2 символов");

        if (value.Length > 50)
            throw new ArgumentException("Имя модератора не может превышать 50 символов.");
    }
}
