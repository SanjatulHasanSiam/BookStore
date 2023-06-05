using BookStore.Models.Domain;
using BookStore.Repositorise.Abstract;
using BookStore.Repositorise.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly IPublisherService _publisherService;
        public BookController(IPublisherService publisherService,IBookService bookService, IAuthorService authorService, IGenreService genreService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
            _publisherService = publisherService;

        }
        public IActionResult Add()
        {
            var model = new Book();
            model.AuthorList = _authorService.GetAll().Select(a=>new SelectListItem { Text=a.AuthorName,Value=a.Id.ToString()}).ToList();
            model.PublisherList = _publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString()}).ToList();
            model.GenreList = _genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString()}).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(Book model)
        {
            model.AuthorList = _authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(),Selected=a.Id==model.AuthorId }).ToList();
            model.PublisherList = _publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(),Selected=a.Id==model.PublisherId }).ToList();
            model.GenreList = _genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(),Selected = a.Id==model.GenreId }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _bookService.Add(model);
            if (result)
            {
                TempData["msg"] = "Book Added Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return View(model);
        }


        public IActionResult Update(int id)
        {
            var model = _bookService.FindById(id);
            model.AuthorList = _authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.PublisherList = _publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
            model.GenreList = _genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Book model)
        {
            model.AuthorList = _authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.PublisherList = _publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
            model.GenreList = _genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _bookService.Update(model);
            if (result)
            {
                TempData["msg"] = "Book Updated Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return View(model);
        }


        public IActionResult Delete(int id)
        {
            var result = _bookService.Delete(id);
            if (result)
            {
                TempData["msg"] = "Book Deleted Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return RedirectToAction("GetAll", "Book");
        }

        public IActionResult GetAll()
        {
            var result = _bookService.GetAll();
            return View(result);
        }
    }
}
