using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcProject._0.Models;
using MvcProject._0.ViewModels;

namespace MvcProject._0.Controllers
{
    public class HomeController : Controller
    {
        public static List<Author> Authors = new List<Author>    {
        new Author
        {
            Id = 1,
            Name = "J.K. Rowling",
            Country = "United Kingdom"
        },
        new Author
        {
            Id = 2,
            Name = "Robert Martin",
            Country = "USA"
        }
    };
        public static List<Book> Books = new List<Book>
    {
        new Book
        {
            Id = 1,
            Title = "Harry Potter",
            Price = 1500,
            AuthorId = 1
        },
        new Book
        {
            Id = 2,
            Title = "Clean Code",
            Price = 2000,
            AuthorId = 2
        },
        new Book
        {
            Id = 3,
            Title = "The Goblet of Fire",
            Price = 1800,
            AuthorId = 1
        }
    };
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            homeViewModel.Books = new List<BookCardViewModel>();

            homeViewModel.Authors = Authors;

            foreach (var book in Books)
            {
                string authorName = "Unknown";

                foreach (var author in Authors)
                {
                    if (author.Id == book.AuthorId)
                    {
                        authorName = author.Name;
                        break;
                    }
                }

                BookCardViewModel bookCard = new BookCardViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    AuthorName = authorName
                };

                homeViewModel.Books.Add(bookCard);
            }

            return View(homeViewModel);
        }
        [HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            int newId = Books.Max(b => b.Id) + 1;

            Book newBook = new Book
            {
                Id = newId,
                Title = title,
                Price = price,
                AuthorId = authorId
            };

            Books.Add(newBook);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            int newId = Authors.Max(a => a.Id) + 1;

            Author newAuthor = new Author
            {
                Id = newId,
                Name = name,
                Country = country
            };

            Authors.Add(newAuthor);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                Books.Remove(book);
            }

            return RedirectToAction("Index");
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
