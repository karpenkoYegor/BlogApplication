using System;
using System.Collections.Generic;
using System.Linq;
using BlogApplication.Data.Entities;
using BlogApplication.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BlogApplication.Data
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;
        private readonly IMapper _mapper;

        public BlogRepository(BlogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return _context.Articles
                .AsNoTracking()
                .Include(a=>a.Category)
                .Include(a => a.Tags)
                .ToList();
        }

        public List<Article> FilterArticles(int[] categoryId, int[] tagsIds, DateTime from,
            DateTime to)
        {
            var articles = _context.Articles
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.Tags)
                .ThenInclude(t => t.TagId)
                .ToList();
            List<Article> filteredArticlesByCategory = new List<Article>();
            foreach (var categoryid in categoryId)
            {
                filteredArticlesByCategory.AddRange(articles.Where(a => a.CategoryId == categoryid).ToList());
            }
            List<Article> filteredArticles = new List<Article>();
            foreach (var tagId in tagsIds)
            {
                foreach (var article in filteredArticlesByCategory)
                {
                    int count = 0;
                    foreach (var tag in article.Tags)
                    {
                        if (tag.TagId == tagId)
                        {
                            count++;
                        }

                        if (count == tagsIds.Length)
                        {
                            filteredArticles.Add(article);
                        }
                    }
                }
            }
            return filteredArticlesByCategory.Distinct().ToList();

        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _context.Tags.AsNoTracking().ToList();
        }

        public IEnumerable<Article> GetArticlesByCategory(int categoryId)
        {
            return _context.Articles
                .Include(a => a.Category)
                .Where(a => a.Category.Id == categoryId)
                .ToList();
        }
        public Article GetArticleById(int id)
        {
            return _context.Articles.AsNoTracking().Include(a=>a.Category).Include(a=>a.Tags).FirstOrDefault(a => a.Id == id);
        }
        public Category GetCategoryById(int id)
        {
            return _context.Categories.AsNoTracking().Include(c => c.Articles).FirstOrDefault(a => a.Id == id);
        }

        public Tag GetTagById(int id)
        {
            return _context.Tags.AsNoTracking().FirstOrDefault(a => a.TagId == id);
        }

        public void EditArticle(ArticleViewModel article)
        {
            var result = _mapper.Map<ArticleViewModel, Article>(article);
            _context.Articles.Update(result);
        }
        public void DeleteCategory(int id)
        {
            var category = _context.Categories.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
        }
        public void CreateTag(TagViewModel tag)
        {
            var result = _mapper.Map<TagViewModel, Tag>(tag);
            _context.Tags.Add(result);
        }

        public void DeleteTag(int id)
        {
            var tag = _context.Tags.AsNoTracking().FirstOrDefault(a => a.TagId == id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
        public void CreateArticle(ArticleViewModel article)
        {
            var result = _mapper.Map<ArticleViewModel, Article>(article);
            result.ArticleTime = DateTime.Now;
            _context.Articles.Add(result);
        }
        public void DeleteArticle(int id)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == id);
            if (article != null)
            {
                _context.Articles.Remove(article);
            }
        }
        public void CreateCategory(CategoryViewModel category)
        {
            var result = _mapper.Map<CategoryViewModel, Category>(category);
            _context.Categories.Add(result);
        }
        public void EditCategory(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}