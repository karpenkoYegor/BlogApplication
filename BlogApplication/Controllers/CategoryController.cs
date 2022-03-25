using System.Collections.Generic;
using BlogApplication.Data;
using BlogApplication.Data.Entities;
using BlogApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    [Authorize]
    [Route("category")]
    public class CategoryController : Controller
    {
        private readonly IBlogRepository _repository;
        private readonly IEnumerable<Category> _categories;

        public CategoryController(IBlogRepository repository)
        {
            _repository = repository;
            _categories = _repository.GetAllCategories();
        }
        [HttpGet("create")]
        public ActionResult Create()
        {
            ViewBag.Categories = _categories;
            return View();
        }

        [HttpPost("create")]
        public ActionResult Create(CategoryViewModel category)
        {
            try
            {
                ViewBag.Categories = _categories;
                _repository.CreateCategory(category);
                if (_repository.SaveAll())
                {
                    return RedirectToAction("Index", "Home");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("edit/{id}")]
        public ActionResult Edit(int id)
        {
            Category category = _repository.GetCategoryById(id);

            if (category != null)
            {
                return View(category);
            }

            return NotFound();
        }

        // POST: CategoryController/Edit/5
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            ViewBag.Categories = _categories;
            try
            {
                _repository.EditCategory(category);
                if (_repository.SaveAll())
                {
                    return RedirectToAction("Index", "Home");
                }

                return BadRequest();
            }
            catch
            {
                return View(category);
            }
        }

        [HttpGet("delete/{id}")]
        public ActionResult Delete(int id)
        {
            var category = _repository.GetCategoryById(id);
            
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _repository.DeleteCategory(id);
                if (_repository.SaveAll())
                {
                    return RedirectToAction("Index", "Home");
                }
                return BadRequest();
            }
            catch
            {
                return View();
            }
        }
    }
}
