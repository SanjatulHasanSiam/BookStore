using BookStore.Models.Domain;
using BookStore.Repositorise.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;
        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result= _publisherService.Add(model);
            if (result)
            {
                TempData["msg"] = "Publisher Added Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return View(model);
        }


        public IActionResult Update(int id)
        {
            var record= _publisherService.FindById(id);
            return View(record);
        }
        [HttpPost]
        public IActionResult Update(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _publisherService.Update(model);
            if (result)
            {
                TempData["msg"] = "Publisher Updated Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return View(model);
        }


        public IActionResult Delete(int id)
        {
            var result = _publisherService.Delete(id);
            if (result)
            {
                TempData["msg"] = "Publisher Deleted Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "An error occured";
            return RedirectToAction("GetAll","Author");
        }

        public IActionResult GetAll()
        {
            var result = _publisherService.GetAll();
          return View(result);
        }

    }
}
