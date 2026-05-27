using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Validators;

namespace BookTracker.ValueObjects;

public class AuthorName(string name) : ValueObject<string>(new AuthorNameValidator(), name);
