
using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

public class GenreName : ValueObject<string>
{
    private GenreName(string value) : base(new GenreNameValidator(), value) { }

    public static GenreName Create(string value) => new GenreName(value);

    public static implicit operator string(GenreName name) => name.Value;
    public static implicit operator GenreName(string value) => Create(value);
}