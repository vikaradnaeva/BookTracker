
using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

public class ModeratorName : ValueObject<string>
{
    private ModeratorName(string value) : base(new ModeratorNameValidator(), value) { }

    public static ModeratorName Create(string value) => new ModeratorName(value);

    public static implicit operator string(ModeratorName name) => name.Value;
    public static implicit operator ModeratorName(string value) => Create(value);
}