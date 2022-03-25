using System.Collections.Generic;
using BlogApplication.Data;
using BlogApplication.Data.Entities;
using BlogApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    public class TagController : Controller
    {
        private readonly IBlogRepository _repository;
        private readonly IEnumerable<Tag> _tags;

        public TagController(IBlogRepository repository)
        {
            _repository = repository;
            _tags = _repository.GetAllTags();
        }
        // GET: TagController/Create
        public ActionResult Create()
        {
            ViewBag.Tags = _tags;
            return View();
        }

        // POST: TagController/Create
        [HttpPost]
        public ActionResult Create(TagViewModel model)
        {
            try
            {
                ViewBag.Tags = _tags;
                _repository.CreateTag(model);
                if (_repository.SaveAll())
                {
                    return RedirectToAction("Create", "Tag");
                }

                return BadRequest();
            }
            catch
            {
                return View();
            }
        }

        // GET: TagController/Delete/5
        public ActionResult Delete(int id)
        {
            var tag = _repository.GetTagById(id);
            return View();
        }

        // POST: TagController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Tag collection)
        {
            try
            {
                _repository.DeleteTag(id);
                if (_repository.SaveAll())
                {
                    return RedirectToAction("Create", "Tag");
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
