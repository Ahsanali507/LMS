//using System;
//using System.Collections.Generic;
//using System.IO;

//namespace LibraryMngSystem
//{
//    class Book
//    {
//        public string Id { get; set; }
//        public string Title { get; set; }
//        public string Author { get; set; }
//        public string Genre { get; set; }
//        public int PublicationYear { get; set; }
//        public bool IsBorrowed { get; set; }
//        public string BorrowerName { get; set; }
//    }

//    class MyLibrary
//    {
//        private List<Book> books;

//        public MyLibrary()
//        {
//            books = new List<Book>();
//        }

//        public void AddBook()
//        {
//            Console.WriteLine("Enter the title of book:");
//            string title = Console.ReadLine();

//            Console.WriteLine("Enter the author of book:");
//            string author = Console.ReadLine();

//            Console.WriteLine("Enter the genre of book:");
//            string genre = Console.ReadLine();

//            Console.WriteLine("Enter the publication year of book:");
//            int publicationYear = int.Parse(Console.ReadLine());

//            string id = Guid.NewGuid().ToString();

//            Book book = new Book
//            {
//                Id = id,
//                Title = title,
//                Author = author,
//                Genre = genre,
//                PublicationYear = publicationYear,
//                IsBorrowed = false,
//                BorrowerName = ""
//            };

//            books.Add(book);

//            SaveBooksToFile();
//            Console.WriteLine("Book is added successfully.");
//        }

//        public void DisplayAllBooks()
//        {
//            if (books.Count == 0)
//            {
//                Console.WriteLine("No any book found in the library.");
//            }
//            else
//            {
//                Console.WriteLine("MyLibrary books are:");

//                foreach (var bk in books)
//                {
//                    Console.WriteLine($"Id is: {bk.Id}");
//                    Console.WriteLine($"Title is: {bk.Title}");
//                    Console.WriteLine($"Author is: {bk.Author}");
//                    Console.WriteLine($"Genre is: {bk.Genre}");
//                    Console.WriteLine($"Publication Year is: {bk.PublicationYear}");
//                    Console.WriteLine($"Borrowed: {(bk.IsBorrowed ? "Yes" : "No")}");
//                    Console.WriteLine($"Borrower Name is: {bk.BorrowerName}");
//                    Console.WriteLine();
//                }
//            }
//        }

//        public void SearchBook()
//        {
//            Console.WriteLine("Enter the term for searching book:");
//            string searchTerm = Console.ReadLine();

//            List<Book> matchingBooks = books.FindAll(book =>
//                book.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
//                book.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
//                book.Genre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

//            if (matchingBooks.Count == 0)
//            {
//                Console.WriteLine("No any book found matching the search term.");
//            }
//            else
//            {
//                Console.WriteLine("Your matching books are:");

//                foreach (var bk in matchingBooks)
//                {
//                    Console.WriteLine($"Id is: {bk.Id}");
//                    Console.WriteLine($"Title is: {bk.Title}");
//                    Console.WriteLine($"Author is: {bk.Author}");
//                    Console.WriteLine($"Genre is: {bk.Genre}");
//                    Console.WriteLine($"Publication Year is: {bk.PublicationYear}");
//                    Console.WriteLine($"Borrowed: {(bk.IsBorrowed ? "Yes" : "No")}");
//                    Console.WriteLine($"Borrower Name is: {bk.BorrowerName}");
//                    Console.WriteLine();
//                }
//            }
//        }

//        public void BorrowBook()
//        {
//            Console.WriteLine("Enter the id of the book that you wanted to borrow from library:");
//            string id = Console.ReadLine();

//            Book book = books.Find(b => b.Id == id);

//            if (book == null)
//            {
//                Console.WriteLine("No any book found with the your provided id.");
//            }
//            else if (book.IsBorrowed)
//            {
//                Console.WriteLine("The book is already borrowed by someone.");
//            }
//            else
//            {
//                Console.WriteLine("Enter your name:");
//                string borrowerName = Console.ReadLine();

//                book.IsBorrowed = true;
//                book.BorrowerName = borrowerName;

//                SaveBooksToFile();
//                Console.WriteLine("Book is borrowed successfully.");
//            }
//        }

//        public void ReturnBook()
//        {
//            Console.WriteLine("Enter the id of the book that you wanted to return back to library:");
//            string id = Console.ReadLine();

//            Book book = books.Find(b => b.Id == id);

//            if (book == null)
//            {
//                Console.WriteLine("No any book found with the your provided id.");
//            }
//            else if (!book.IsBorrowed)
//            {
//                Console.WriteLine("The book is not currently borrowed.");
//            }
//            else
//            {
//                book.IsBorrowed = false;
//                book.BorrowerName = "";

//                SaveBooksToFile();
//                Console.WriteLine("Book is returned successfully.");
//            }
//        }

//        private void SaveBooksToFile()
//        {
//            using (StreamWriter writer = new StreamWriter("myLibraymanagmentsystem.txt"))
//            {
//                foreach (var bk in books)
//                {
//                    writer.WriteLine($"{bk.Id},{bk.Title},{bk.Author},{bk.Genre},{bk.PublicationYear},{bk.IsBorrowed},{bk.BorrowerName}");
//                }
//            }
//        }

//        public void LoadBooksFromFile()
//        {
//            if (File.Exists("myLibraymanagmentsystem.txt"))
//            {
//                using (StreamReader rd = new StreamReader("myLibraymanagmentsystem.txt"))
//                {
//                    string ln;

//                    while ((ln = rd.ReadLine()) != null)
//                    {
//                        string[] parts = ln.Split(',');

//                        Book book = new Book
//                        {
//                            Id = parts[0],
//                            Title = parts[1],
//                            Author = parts[2],
//                            Genre = parts[3],
//                            PublicationYear = int.Parse(parts[4]),
//                            IsBorrowed = bool.Parse(parts[5]),
//                            BorrowerName = parts[6]
//                        };

//                        books.Add(book);
//                    }
//                }
//            }
//        }
//    }

//    class MyLMS
//    {
//        static void Main(string[] args)
//        {
//            MyLibrary mylib = new MyLibrary();
//            mylib.LoadBooksFromFile();

//            bool exitProg = false;

//            while (!exitProg)
//            {
//                Console.WriteLine("MyLibrary Management System");
//                Console.WriteLine("1. Add a Book");
//                Console.WriteLine("2. Display All Books");
//                Console.WriteLine("3. Search for a Book");
//                Console.WriteLine("4. Borrow a Book");
//                Console.WriteLine("5. Return a Book");
//                Console.WriteLine("6. Exit");
//                Console.WriteLine("Enter your choice (1-6):");

//                string yourChoice = Console.ReadLine();

//                switch (yourChoice)
//                {
//                    case "1":
//                        mylib.AddBook();
//                        break;
//                    case "2":
//                        mylib.DisplayAllBooks();
//                        break;
//                    case "3":
//                        mylib.SearchBook();
//                        break;
//                    case "4":
//                        mylib.BorrowBook();
//                        break;
//                    case "5":
//                        mylib.ReturnBook();
//                        break;
//                    case "6":
//                        exitProg = true;
//                        break;
//                    default:
//                        Console.WriteLine("Invalid choice, Try again once more");
//                        break;
//                }

//                Console.WriteLine();
//            }
//        }
//    }
//}

