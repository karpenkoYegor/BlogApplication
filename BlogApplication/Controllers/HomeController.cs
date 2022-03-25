using System.Collections.Generic;
using BlogApplication.Data;
using BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogApplication.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;

namespace BlogApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;


        public HomeController(ILogger<HomeController> logger, IBlogRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult Index(int id, int pageindex = 1)
        {
            IEnumerable<Article> results;
            if (id == 0)
            {
                results = _repository.GetAllArticles();
            }
            else
            {
                results = _repository.GetArticlesByCategory(id);
            }
            int pageSize = 3;
            var model = PagingList.Create<Article>(results, pageSize, pageindex);
            return View(model);
        }
        [HttpGet]
        public IActionResult Article(int id)
        {
            var result = _repository.GetArticleById(id);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            var model = _repository.GetAllArticles();
            return View(model);
        }
    }
}
