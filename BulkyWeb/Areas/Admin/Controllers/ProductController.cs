using BookBazaar.DataAccess.Repository.IRepository;
using BookBazaar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookBazaar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> productList = _unitOfWork.Product.GetAll().ToList();
            return View(productList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll(). // This is projection implementation in EFCore 
               Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString()
               });

            ViewBag.CategoryList = CategoryList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (obj.Title == obj.Author)
            {
                ModelState.AddModelError("Title", "Book Title and Author Name cannot be the same");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Book created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? product = _unitOfWork.Product.Get(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Book updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product product = _unitOfWork.Product.Get(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id) {
            Product product = _unitOfWork.Product.Get(p => p.Id == id);
            
            if (product == null)
            {
                TempData["Error"] = "Book not found";
                return RedirectToAction("Index");
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["Success"] = "Book deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
