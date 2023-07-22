using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Mng_System
{
    // DTO (Data Transfer Object) class to represent a Book
    public class Book
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PublicationYear { get; set; }
        public bool IsBorrowed { get; set; }
        public string Borrower { get; set; }
    }

    // DAL (Data Access Layer) class to handle file operations
    public class LibraryDAL
    {
        private const string FilePath = "myLibrarymngtsystem.txt";

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            if (File.Exists(FilePath))
            {
                string[] lines = File.ReadAllLines(FilePath);

                foreach (string line in lines)
                {
                    string[] data = line.Split(',');
                    Book book = new Book
                    {
                        id = Convert.ToInt32(data[0]),
                        Title = data[1],
                        Author = data[2],
                        Genre = data[3],
                        PublicationYear = Convert.ToInt32(data[4]),
                        IsBorrowed = Convert.ToBoolean(data[5]),
                        Borrower = data[6]
                    };
                    books.Add(book);
                }
            }

            return books;
        }

        public void SaveAllBooks(List<Book> books)
        {
            List<string> lines = new List<string>();

            foreach (Book book in books)
            {
                string line = $"{book.id},{book.Title},{book.Author},{book.Genre},{book.PublicationYear},{book.IsBorrowed},{book.Borrower}";
                lines.Add(line);
            }

            File.WriteAllLines(FilePath, lines);
        }
    }

    // BLL (Business Logic Layer) class to perform library operations
    public class MyLibraryBLL
    {
        private List<Book> books;
        private LibraryDAL libraryDAL;

        public MyLibraryBLL()
        {
            libraryDAL = new LibraryDAL();
            books = libraryDAL.GetAllBooks();
        }

        public void AddBook(string title, string author, string genre, int publicationYear)
        {
            int nextId = books.Count > 0 ? books.Max(b => b.id) + 1 : 1;

            Book book = new Book
            {
                id = nextId,
                Title = title,
                Author = author,
                Genre = genre,
                PublicationYear = publicationYear,
                IsBorrowed = false,
                Borrower = string.Empty
            };

            books.Add(book);
            libraryDAL.SaveAllBooks(books);
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            return books.Where(b =>
                b.Title.ToLower().Contains(searchTerm) ||
                b.Author.ToLower().Contains(searchTerm) ||
                b.Genre.ToLower().Contains(searchTerm)).ToList();
        }

        public void BorrowBook(int bookId, string borrower)
        {
            Book book = books.FirstOrDefault(b => b.id == bookId);

            if (book != null && !book.IsBorrowed)
            {
                book.IsBorrowed = true;
                book.Borrower = borrower;
                libraryDAL.SaveAllBooks(books);
            }
        }

        public void ReturnBook(int bookId)
        {
            Book book = books.FirstOrDefault(b => b.id == bookId);

            if (book != null && book.IsBorrowed)
            {
                book.IsBorrowed = false;
                book.Borrower = string.Empty;
                libraryDAL.SaveAllBooks(books);
            }
        }
    }

    // Console application to interact with the library management system
    public class Program
    {
        private static MyLibraryBLL libBLL = new MyLibraryBLL();

        static void Main()
        {
            Console.WriteLine("Welcome to the MyLibrary Management System!");

            bool exitProgram = false;

            while (!exitProgram)
            {
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("1. Add a Book");
                Console.WriteLine("2. Display All Books");
                Console.WriteLine("3. Search for a Book");
                Console.WriteLine("4. Borrow a Book");
                Console.WriteLine("5. Return a Book");
                Console.WriteLine("6. Exit");

                Console.Write("\nEnter your choice (1-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        DisplayAllBooks();
                        break;
                    case "3":
                        SearchBook();
                        break;
                    case "4":
                        BorrowBook();
                        break;
                    case "5":
                        ReturnBook();
                        break;
                    case "6":
                        exitProgram = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            Console.WriteLine("Thank you for using the Library Management System!");
        }

        static void AddBook()
        {
            Console.WriteLine("\nAdding a Book");

            Console.Write("Enter the book's title: ");
            string title = Console.ReadLine();

            Console.Write("Enter the book's author: ");
            string author = Console.ReadLine();

            Console.Write("Enter the book's genre: ");
            string genre = Console.ReadLine();

            Console.Write("Enter the book's publication year: ");
            int publicationYear = Convert.ToInt32(Console.ReadLine());

            libBLL.AddBook(title, author, genre, publicationYear);

            Console.WriteLine("Book added successfully!");
        }

        static void DisplayAllBooks()
        {
            Console.WriteLine("\nDisplaying All Books");

            List<Book> books = libBLL.GetAllBooks();

            if (books.Count == 0)
            {
                Console.WriteLine("No books found.");
            }
            else
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-15} {4,-10}", "ID", "Title", "Author", "Genre", "Publication Year");
                Console.WriteLine("--------------------------------------------------------------------------------");

                foreach (Book book in books)
                {
                    Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-15} {4,-10}", book.id, book.Title, book.Author, book.Genre, book.PublicationYear);
                }
            }
        }

        static void SearchBook()
        {
            Console.WriteLine("\nSearch for a Book");

            Console.Write("Enter the search term: ");
            string searchTerm = Console.ReadLine();

            List<Book> searchResults = libBLL.SearchBooks(searchTerm);

            if (searchResults.Count == 0)
            {
                Console.WriteLine("No books found matching the search term.");
            }
            else
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-15} {4,-10}", "ID", "Title", "Author", "Genre", "Publication Year");
                Console.WriteLine("--------------------------------------------------------------------------------");

                foreach (Book book in searchResults)
                {
                    Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-15} {4,-10}", book.id, book.Title, book.Author, book.Genre, book.PublicationYear);
                }
            }
        }

        static void BorrowBook()
        {
            Console.WriteLine("\nBorrow a Book");

            Console.Write("Enter the book's ID: ");
            int bookId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the borrower's name: ");
            string borrower = Console.ReadLine();

            libBLL.BorrowBook(bookId, borrower);

            Console.WriteLine("Book borrowed successfully!");
        }

        static void ReturnBook()
        {
            Console.WriteLine("\nReturn a Book");

            Console.Write("Enter the book's ID: ");
            int bookId = Convert.ToInt32(Console.ReadLine());

            libBLL.ReturnBook(bookId);

            Console.WriteLine("Book returned successfully!");
        }
    }

}
