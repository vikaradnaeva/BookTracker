
using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

public class AuthorName : ValueObject<string>
{
    private AuthorName(string value) : base(new AuthorNameValidator(), value) { }

    public static AuthorName Create(string value) => new AuthorName(value);

    public static implicit operator string(AuthorName name) => name.Value;
    public static implicit operator AuthorName(string value) => Create(value);
}