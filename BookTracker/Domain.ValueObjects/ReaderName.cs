
using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

public class ReaderName : ValueObject<string>
{
    private ReaderName(string value) : base(new ReaderNameValidator(), value) { }

    public static ReaderName Create(string value) => new ReaderName(value);

    public static implicit operator string(ReaderName name) => name.Value;
    public static implicit operator ReaderName(string value) => Create(value);
}