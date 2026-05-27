using BookTracker.Domain.Entities;
using BookTracker.ValueObjects;

var moderator = new Moderator(Guid.NewGuid(), new AuthorName("admin"));

var catalogue = new List<Book>();
var book = moderator.AddBook(catalogue, new BookTitle("Мастер и Маргарита"), new AuthorName("Булгаков"), 32);

var genre = moderator.CreateGenre(new GenreName("Роман"));
var tag   = moderator.CreateTag(new TagName("классика"));
moderator.AddGenreToBook(book, genre);
moderator.AddTagToBook(book, tag);

Console.WriteLine($"Книга: {book.Title} | Глав: {book.TotalChapters}");
Console.WriteLine($"Жанр: {book.Genres.First().Name} | Тег: {book.Tags.First().Name}");

var reader = new Reader(Guid.NewGuid(), new AuthorName("ivan"));

var progress = reader.StartReading(book);
reader.UpdateReadingChapter(book, 5);
Console.WriteLine($"Читатель на главе: {progress.CurrentChapter}");

var rating = reader.RateBook(book, 5);
Console.WriteLine($"Оценка: {rating.Rating}/5");

reader.AddToList(book, ReadingStatus.Favourite);
Console.WriteLine($"Статус книги: {reader.BookList[book.Id]}");
