using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Validators;

namespace BookTracker.ValueObjects;

public class GenreName(string name) : ValueObject<string>(new GenreNameValidator(), name);
