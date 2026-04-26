
using Domain.ValueObjects.Base;

namespace Domain.ValueObjects.Validators;

public class BookTitleValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Название книги не может быть пустым.");

        if (value.Length < 1)
            throw new ArgumentException("Название книги должно содержать не менее 1 символа.");

        if (value.Length > 200)
            throw new ArgumentException("Название книги не может превышать 200 символов.");
    }
}