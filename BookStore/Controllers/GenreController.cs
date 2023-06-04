using BookStore.Models.Domain;
using BookStore.Repositorise.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result=_genreService.Add(model);
            if (result)
            {
                TempData["msg"] = "Genre Added Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return View(model);
        }


        public IActionResult Update(int id)
        {
            var record=_genreService.FindById(id);
            return View(record);
        }
        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _genreService.Update(model);
            if (result)
            {
                TempData["msg"] = "Genre Updated Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return View(model);
        }


        public IActionResult Delete(int id)
        {
            var result = _genreService.Delete(id);
            if (result)
            {
                TempData["msg"] = "Genre Deleted Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {
            var result = _genreService.GetAll();
          return View(result);
        }

    }
}
