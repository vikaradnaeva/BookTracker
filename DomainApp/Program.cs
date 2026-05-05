using BookTracker.Domain.Entities;
using BookTracker.Domain.Exceptions;
using BookTracker.ValueObjects;
using BookTracker.ValueObjects.Exceptions;

Console.OutputEncoding = System.Text.Encoding.UTF8;

void Header(string text)
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(new string('═', 68));
    Console.WriteLine($"  {text}");
    Console.WriteLine(new string('═', 68));
    Console.ResetColor();
}

void Section(string num, string text)
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ┌─ {num}. {text}");
    Console.ResetColor();
}

void Ok(string text)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("  │  ✓ ");
    Console.ResetColor();
    Console.WriteLine(text);
}

void Info(string text)
{
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.Write("  │    → ");
    Console.ResetColor();
    Console.WriteLine(text);
}

void Note(string text)
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.Write("  │  ℹ ");
    Console.ResetColor();
    Console.WriteLine(text);
}

void DomainError(string scenario, Action action)
{
    try
    {
        action();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  │  ✗ Ожидалось исключение: {scenario}");
        Console.ResetColor();
    }
    catch (DomainException ex)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("  │  ⚡ ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"[{ex.GetType().Name}] ");
        Console.ResetColor();
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex) when (ex is ArgumentNullOrWhiteSpaceException
                                  or ArgumentShortValueException
                                  or ArgumentLongValueException)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("  │  ⚡ ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"[{ex.GetType().Name}] ");
        Console.ResetColor();
        Console.WriteLine(ex.Message);
    }
}

Header("BookTracker — Демонстрация доменного слоя");

Section("1", "Value Objects — неизменяемость и равенство по значению");

var title1 = new BookTitle("Мастер и Маргарита");
var title2 = new BookTitle("Мастер и Маргарита");
var title3 = new BookTitle("1984");
Ok($"BookTitle(\"{title1}\") == BookTitle(\"{title2}\"): {title1 == title2}  ← разные объекты, одно значение");
Ok($"BookTitle(\"{title1}\") == BookTitle(\"{title3}\"): {title1 == title3}");

var genre1 = new Genre("Роман");
var genre2 = new Genre("роман");
Ok($"Genre(\"Роман\") == Genre(\"роман\"): {genre1 == genre2}  ← case-insensitive");

var tag1 = new Tag("  Классика  ");
var tag2 = new Tag("классика");
Ok($"Tag(\"  Классика  \") == Tag(\"классика\"): {tag1 == tag2}  ← trim + lowercase");

var rating1 = new Rating(4.678);
Ok($"Rating(4.678) округляется → {rating1}");

DomainError("Rating(0) — вне диапазона 1–5",          () => new Rating(0));
DomainError("Rating(5.5) — выше максимума",           () => new Rating(5.5));
DomainError("BookTitle(\"\") — пустое название",      () => new BookTitle(""));
DomainError("Username \"ab\" — короче 3 символов",    () => new Username("ab"));
DomainError("Genre 51 символ — длиннее максимума",    () => new Genre(new string('А', 51)));
DomainError("Tag 31 символ — длиннее максимума",      () => new Tag(new string('т', 31)));
DomainError("Email без @ — неверный формат",          () => new Email("notanemail"));

Section("2", "Создание пользователей (Читатель и Модератор)");

var moderator = new User(
    new Username("kate_mod"),
    new Email("kate@booktracker.ru"),
    UserRole.Moderator);

var reader = new User(
    new Username("ivan_reads"),
    new Email("ivan@mail.ru"),
    UserRole.Reader);

var reader2 = new User(
    new Username("maria_books"),
    new Email("maria@mail.ru"),
    UserRole.Reader);

Ok($"Модератор: {moderator.Username}  [{moderator.Role}]  ID={moderator.Id.ToString()[..8]}…");
Ok($"Читатель:  {reader.Username}   [{reader.Role}]  ID={reader.Id.ToString()[..8]}…");
Ok($"Читатель:  {reader2.Username}  [{reader2.Role}]  ID={reader2.Id.ToString()[..8]}…");
Ok($"IsModerator() → {moderator.IsModerator()} / {reader.IsModerator()}");

Section("3", "Модератор добавляет книги в каталог");

var book1 = new Book(
    new BookTitle("Мастер и Маргарита"),
    new Author("Михаил Булгаков"),
    new Description("Роман о визите Дьявола в советскую Москву."),
    32);

var book2 = new Book(
    new BookTitle("Преступление и наказание"),
    new Author("Фёдор Достоевский"),
    new Description("Психологический роман о студенте Раскольникове."),
    6);

var book3 = new Book(
    new BookTitle("1984"),
    new Author("Джордж Оруэлл"),
    new Description("Антиутопия о тоталитарном обществе под властью Старшего Брата."),
    3);

var catalog = new List<Book> { book1, book2, book3 };

Ok($"«{book1.Title}» — {book1.Author}, {book1.TotalChapters} гл.");
Ok($"«{book2.Title}» — {book2.Author}, {book2.TotalChapters} ч.");
Ok($"«{book3.Title}» — {book3.Author}, {book3.TotalChapters} ч.");

DomainError("Книга с 0 глав — нарушение инварианта",
    () => new Book(new BookTitle("Тест"), new Author("Автор Тест"), new Description(""), 0));
DomainError("Книга с отрицательным кол-вом глав",
    () => new Book(new BookTitle("Тест"), new Author("Автор Тест"), new Description(""), -5));

Section("4", "Модератор редактирует теги и жанры книг");

book1.UpdateTagsAndGenres(
    genres: [new Genre("Роман"), new Genre("Мистика"), new Genre("Сатира")],
    tags:   [new Tag("классика"), new Tag("советская литература"), new Tag("дьявол")]);

book2.UpdateTagsAndGenres(
    genres: [new Genre("Роман"), new Genre("Психологическая проза")],
    tags:   [new Tag("классика"), new Tag("философия"), new Tag("преступление")]);

book3.UpdateTagsAndGenres(
    genres: [new Genre("Антиутопия"), new Genre("Политическая фантастика")],
    tags:   [new Tag("классика"), new Tag("тоталитаризм"), new Tag("свобода")]);

Ok($"«{book1.Title}» → жанры: [{string.Join(", ", book1.Genres)}]");
Info($"теги: [{string.Join(", ", book1.Tags)}]");
Ok($"«{book2.Title}» → жанры: [{string.Join(", ", book2.Genres)}]");
Ok($"«{book3.Title}» → жанры: [{string.Join(", ", book3.Genres)}]");

Section("5", "Читатель ищет книги (in-memory поиск по жанру и тегу)");

var byRoman = catalog.Where(b => b.Genres.Any(g => g == new Genre("Роман"))).ToList();
Ok($"Поиск жанр «Роман» → {byRoman.Count} книг(и):");
foreach (var b in byRoman) Info($"«{b.Title}»");

var byClassic = catalog.Where(b => b.Tags.Any(t => t == new Tag("классика"))).ToList();
Ok($"Поиск тег «классика» → {byClassic.Count} книг(и):");
foreach (var b in byClassic) Info($"«{b.Title}»");

var byTitle = catalog.Where(b =>
    b.Title.Value.Contains("и", StringComparison.OrdinalIgnoreCase)).ToList();
Ok($"Поиск по вхождению «и» в названии → {byTitle.Count} книг(и):");
foreach (var b in byTitle) Info($"«{b.Title}»");

Section("6", "Читатель начинает чтение (прогресс запоминает главу)");

var prog1 = reader.StartReading(book1);
Ok($"StartReading «{book1.Title}»: гл. {prog1.CurrentChapter}/{prog1.TotalChapters}  ({prog1.ProgressPercentage:F0}%)");

reader.UpdateReadingProgress(book1.Id, 8);
Ok($"UpdateProgress → гл. 8   [{ProgressBar(prog1.ProgressPercentage)}] {prog1.ProgressPercentage:F0}%");

reader.UpdateReadingProgress(book1.Id, 20);
Ok($"UpdateProgress → гл. 20  [{ProgressBar(prog1.ProgressPercentage)}] {prog1.ProgressPercentage:F0}%");

reader.UpdateReadingProgress(book1.Id, 32);
Ok($"UpdateProgress → гл. 32  [{ProgressBar(prog1.ProgressPercentage)}] {prog1.ProgressPercentage:F0}%  ← финал");

var prog1Again = reader.StartReading(book1);
Note($"StartReading повторно → возвращает тот же прогресс (гл. {prog1Again.CurrentChapter}), не сбрасывает");

var prog2 = reader.StartReading(book2);
reader.UpdateReadingProgress(book2.Id, 3);
Ok($"«{book2.Title}»: гл. {prog2.CurrentChapter}/{prog2.TotalChapters}  [{ProgressBar(prog2.ProgressPercentage)}] {prog2.ProgressPercentage:F0}%");

DomainError("Глава 100 из 32 — выход за границы",
    () => reader.UpdateReadingProgress(book1.Id, 100));
DomainError("Глава -1 — отрицательный номер",
    () => reader.UpdateReadingProgress(book1.Id, -1));
DomainError("book3 не начата — нельзя обновить прогресс",
    () => reader.UpdateReadingProgress(book3.Id, 1));

Section("7", "Читатель добавляет книги в списки (избранное / в планах / прочитано)");

reader.AddBookToList(book1, BookListType.Favorites);
reader.AddBookToList(book2, BookListType.Planned);
reader.AddBookToList(book3, BookListType.Read);

Ok($"«{book1.Title}»            → {BookListType.Favorites}");
Ok($"«{book2.Title}» → {BookListType.Planned}");
Ok($"«{book3.Title}»                      → {BookListType.Read}");

reader.AddBookToList(book2, BookListType.Read);
var entry2 = reader.BookListEntries.First(e => e.BookId == book2.Id);
Ok($"«{book2.Title}» переведена в → {entry2.ListType}  (смена списка)");

Console.WriteLine();
Note("Списки читателя ivan_reads:");
foreach (var e in reader.BookListEntries)
{
    var t = catalog.First(b => b.Id == e.BookId).Title;
    Console.WriteLine($"  │    • «{t}» → {e.ListType}");
}

Section("8", "Читатель ставит оценку книге (1.0 – 5.0)");

reader.RateBook(book1, 5.0);
Ok($"ivan_reads → «{book1.Title}»: ★ {book1.AverageRating:F2}  ({book1.RatingsCount} оценка)");

reader2.RateBook(book1, 4.0);
Ok($"maria_books → «{book1.Title}»: ★ {book1.AverageRating:F2}  ({book1.RatingsCount} оценки)  ← среднее пересчитано");

reader.RateBook(book2, 4.0);
reader.RateBook(book3, 3.5);
Ok($"Оценки выставлены: «{book2.Title}» ★{book2.AverageRating:F1}, «{book3.Title}» ★{book3.AverageRating:F1}");

DomainError("Повторная оценка той же книги тем же пользователем",
    () => reader.RateBook(book1, 3.0));
DomainError("Рейтинг 6.0 — выше максимума",
    () => reader2.RateBook(book2, 6.0));
DomainError("Рейтинг 0.5 — ниже минимума",
    () => reader2.RateBook(book3, 0.5));

Section("9", "Модератор удаляет книгу из каталога");

var tempBook = new Book(
    new BookTitle("Временная книга"),
    new Author("Тест Тестович"),
    new Description("Будет удалена модератором."),
    5);
catalog.Add(tempBook);
Ok($"Добавлена: «{tempBook.Title}»  IsDeleted={tempBook.IsDeleted}");

tempBook.Delete();
Ok($"Удалена:   «{tempBook.Title}»  IsDeleted={tempBook.IsDeleted}");

DomainError("Повторное удаление уже удалённой книги",
    () => tempBook.Delete());
DomainError("StartReading удалённой книги",
    () => reader.StartReading(tempBook));
DomainError("AddBookToList удалённой книги",
    () => reader.AddBookToList(tempBook, BookListType.Favorites));
DomainError("RateBook удалённой книги",
    () => reader.RateBook(tempBook, 4.0));
DomainError("UpdateTagsAndGenres удалённой книги",
    () => tempBook.UpdateTagsAndGenres(
        [new Genre("Жанр")], [new Tag("тег")]));

Section("10", "Итоговое состояние системы");

Console.WriteLine();
Console.WriteLine($"  │  Читатель «{reader.Username}»");
Console.WriteLine($"  │  ┌{'─',50}┬{'─',8}┬{'─',10}┐");
Console.WriteLine($"  │  │ {"Книга",-48} │ {"Глава",6} │ {"Прогресс",8} │");
Console.WriteLine($"  │  ├{'─',50}┼{'─',8}┼{'─',10}┤");
foreach (var p in reader.ReadingProgresses)
{
    var t = catalog.First(b => b.Id == p.BookId).Title.Value;
    Console.WriteLine($"  │  │ {t,-48} │ {$"{p.CurrentChapter}/{p.TotalChapters}",6} │ {p.ProgressPercentage,7:F0}% │");
}
Console.WriteLine($"  │  └{'─',50}┴{'─',8}┴{'─',10}┘");

Console.WriteLine();
Console.WriteLine($"  │  Каталог активных книг:");
Console.WriteLine($"  │  ┌{'─',38}┬{'─',7}┬{'─',9}┐");
Console.WriteLine($"  │  │ {"Название",-36} │ {"★ Рейт",5} │ {"Оценок",7} │");
Console.WriteLine($"  │  ├{'─',38}┼{'─',7}┼{'─',9}┤");
foreach (var b in catalog.Where(b => !b.IsDeleted))
    Console.WriteLine($"  │  │ {b.Title.Value,-36} │ {b.AverageRating,5:F2} │ {b.RatingsCount,7} │");
Console.WriteLine($"  │  └{'─',38}┴{'─',7}┴{'─',9}┘");

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("  └─ Демонстрация завершена ✓");
Console.ResetColor();
Console.WriteLine();

static string ProgressBar(double pct)
{
    int filled = (int)(pct / 10);
    return new string('█', filled) + new string('░', 10 - filled);
}
