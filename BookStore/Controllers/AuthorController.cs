using BookStore.Models.Domain;
using BookStore.Repositorise.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Author model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result= _authorService.Add(model);
            if (result)
            {
                TempData["msg"] = "Author Added Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return View(model);
        }


        public IActionResult Update(int id)
        {
            var record= _authorService.FindById(id);
            return View(record);
        }
        [HttpPost]
        public IActionResult Update(Author model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _authorService.Update(model);
            if (result)
            {
                TempData["msg"] = "Author Updated Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return View(model);
        }


        public IActionResult Delete(int id)
        {
            var result = _authorService.Delete(id);
            if (result)
            {
                TempData["msg"] = "Author Deleted Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return RedirectToAction("GetAll","Author");
        }

        public IActionResult GetAll()
        {
            var result = _authorService.GetAll();
          return View(result);
        }

    }
}
