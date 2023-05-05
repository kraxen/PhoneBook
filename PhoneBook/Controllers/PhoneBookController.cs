using Microsoft.AspNetCore.Mvc;
using PhoneBook.Models;
using System.Diagnostics;

namespace PhoneBook.Controllers
{
    public class PhoneBookController : Controller
    {
        private readonly ILogger<PhoneBookController> _logger;

        public IPhoneBookModel PhoneBook { get; }

        public PhoneBookController(ILogger<PhoneBookController> logger, IPhoneBookModel phoneBook)
        {
            _logger = logger;
            PhoneBook = phoneBook;
        }

        public IActionResult Index()
        {
            return View(PhoneBook);
        }
        [HttpGet]
        public IActionResult Node( int id)
        {
            var node = PhoneBook.Nodes.FirstOrDefault(n => n.Id == id);
            if (node is not null)
            {
                return View(node);
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(PhoneBookNode phoneBookNode)
        {
            PhoneBook.Add(phoneBookNode);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Change(int id)
        {
            var node = PhoneBook.Nodes.FirstOrDefault(n => n.Id == id);
            if (node is not null)
                return View(node);
            else return NotFound();
        }
        [HttpPost]
        [HttpPut]
        public IActionResult Change(PhoneBookNode node)
        {
            PhoneBook.Change(node);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [HttpDelete]
        public IActionResult Remove([FromQuery] int id)
        {
            var node = PhoneBook.Nodes.FirstOrDefault(n => n.Id == id);
            if(node is not null)
                PhoneBook.Remove(node);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}