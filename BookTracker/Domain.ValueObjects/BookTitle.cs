
using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

public class BookTitle : ValueObject<string>
{
    private BookTitle(string value) : base(new BookTitleValidator(), value) { }

    public static BookTitle Create(string value) => new BookTitle(value);

    public static implicit operator string(BookTitle title) => title.Value;
    public static implicit operator BookTitle(string value) => Create(value);
}