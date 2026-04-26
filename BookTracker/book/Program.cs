using Domain.BookTracker.Domain.Entities;
using Domain.BookTracker.Domain.Enums;
using Domain.ValueObjects;
using Domain.ValueObjects.Base.Exceptions;

namespace book
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Book Tracker\n");

            var book = new Book("1984", "George Orwell");
            var reader = new Reader("John Doe");
            var moderator = new Moderator("Jane Smith");
            var genre = new Genre("Dystopian");
            var tag = new Tag("Classic");

            Console.WriteLine($"   Book: {book.Title} by {book.Author}");
            Console.WriteLine($"   Reader: {reader.Name}");
            Console.WriteLine($"   Moderator: {moderator.Name}");
            Console.WriteLine($"   Genre: {genre.Name}");
            Console.WriteLine($"   Tag: {tag.Name}");

            Console.WriteLine("\n2. Value Objects validation:");

            try
            {
                var invalidTitle = BookTitle.Create("");
            }
            catch (ValueObjectValidationException ex)
            {
                Console.WriteLine($"   Validation error (title): {ex.Message}");
            }

            try
            {
                var invalidAuthor = AuthorName.Create("A");
            }
            catch (ValueObjectValidationException ex)
            {
                Console.WriteLine($"   Validation error (author): {ex.Message}");
            }

            // Демонстрация работы читателя
            Console.WriteLine("\n3. Reader actions:");

            reader.AddBook(book.Id, BookStatus.InPlans);
            Console.WriteLine($"   Book added to plans: {reader.GetBookStatus(book.Id)}");

            reader.SaveProgress(book.Id, 5);
            Console.WriteLine($"   Progress saved: chapter {reader.GetProgress(book.Id)}");

            reader.UpdateStatus(book.Id, BookStatus.InProgress);
            Console.WriteLine($"   Status updated: {reader.GetBookStatus(book.Id)}");

            // Демонстрация работы с книгой
            Console.WriteLine("\n4. Book actions:");

            book.AddGenre(genre);
            book.AddTag(tag);
            Console.WriteLine($"   Genres: {book.Genres.Count}, Tags: {book.Tags.Count}");

            book.AddRating(5);
            book.AddRating(4);
            Console.WriteLine($"   Rating: {book.AverageRating:F1} (from {book.RatingCount} ratings)");

            // Демонстрация работы модератора
            Console.WriteLine("\n5. Moderator actions:");

            moderator.AddModeratedBook(book.Id);
            Console.WriteLine($"   Moderated books: {moderator.ModeratedBookIds.Count}");

            // Вывод итогов
            Console.WriteLine("\n=== Demo completed ===");
            Console.WriteLine($"\nEntities created at:");
            Console.WriteLine($"   Book: {book.CreatedAt}");
            Console.WriteLine($"   Reader: {reader.CreatedAt}");
            Console.WriteLine($"   Moderator: {moderator.CreatedAt}");
        }
    }


}




