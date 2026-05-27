using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Validators;

namespace BookTracker.ValueObjects;

public class TagName(string name) : ValueObject<string>(new TagNameValidator(), name);
