using AutoMapper;
using BlogApplication.Data;
using BlogApplication.Data.Entities;
using BlogApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlogApplication.Controllers
{
    [Authorize]
    [Route("article")]
    public class ArticleController : Controller
    {
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IEnumerable<Category> _categories;
        private readonly IEnumerable<Tag> _tags;

        public ArticleController(IBlogRepository repository, IMapper mapper, IWebHostEnvironment env)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
            _categories = _repository.GetAllCategories();
            _tags = _repository.GetAllTags();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Tags = _tags;
            ViewBag.Categories = _categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleViewModel article)
        {
            ViewBag.Tags = _tags;
            ViewBag.Categories = _categories;
            try
            {
                foreach (var tagsId in article.TagsIds)
                {
                    var tag = _repository.GetTagById(tagsId);
                    article.ArticleTags.Add(new ArticleTags() {ArticleId = article.Id, TagId = tagsId});
                }

                if (article.HeroImage != null)
                {
                    string path = Path.Combine("/img/",article.HeroImage.FileName);
                    using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                    {
                        await article.HeroImage.CopyToAsync(fileStream);
                    }

                    article.HeroImagePath = path;
                }
                _repository.CreateArticle(article);
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
        [HttpGet("edit/{id}")]

        public IActionResult Edit(int id)
        {
            ViewBag.Tags = _tags;
            ViewBag.Categories = _categories;
            Article article = _repository.GetArticleById(id);
            ArticleViewModel model = new ArticleViewModel()
            {
                Id = article.Id,
                Name = article.Name,
                ShortDescription = article.ShortDescription,
                Description = article.Description,
                HeroImagePath = article.HeroImage,
                CategoryId = article.CategoryId
            };

            if (article != null)
            {
                return View(model);
            }
                
            return NotFound();

        }
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(ArticleViewModel article)
        {
            ViewBag.Tags = _tags;
            ViewBag.Categories = _categories;
            try
            {
                foreach (var tagsId in article.TagsIds)
                {
                    var tag = _repository.GetTagById(tagsId);
                    article.ArticleTags.Add(new ArticleTags() { ArticleId = article.Id, TagId = tagsId });
                }

                if (article.HeroImage != null)
                {
                    string path = Path.Combine("/img/", article.HeroImage.FileName);
                    if (!System.IO.File.Exists(_env.WebRootPath + path))
                    {
                        using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                        {
                            await article.HeroImage.CopyToAsync(fileStream);
                        }
                    }
                    article.HeroImagePath = article.HeroImage.FileName;
                }
                _repository.EditArticle(article);
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

        [HttpGet("delete/{id}")]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            return View();
        }
        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteArticle(id);
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
