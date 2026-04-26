
using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

public class TagName : ValueObject<string>
{
    private TagName(string value) : base(new TagNameValidator(), value) { }

    public static TagName Create(string value) => new TagName(value);

    public static implicit operator string(TagName name) => name.Value;
    public static implicit operator TagName(string value) => Create(value);
}