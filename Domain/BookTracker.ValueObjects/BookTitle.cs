using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Validators;

namespace BookTracker.ValueObjects;

public class BookTitle(string title) : ValueObject<string>(new BookTitleValidator(), title);
