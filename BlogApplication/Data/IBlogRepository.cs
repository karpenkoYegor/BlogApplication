using System.Collections.Generic;
using System.Linq;
using BlogApplication.Data.Entities;
using BlogApplication.Models;

namespace BlogApplication.Data
{
    public interface IBlogRepository
    {
        IEnumerable<Article> GetAllArticles();
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Tag> GetAllTags();
        IEnumerable<Article> GetArticlesByCategory(int categoryId);
        Article GetArticleById(int id);
        Category GetCategoryById(int id);
        Tag GetTagById(int id);
        public void EditArticle(ArticleViewModel article);
        public void CreateArticle(ArticleViewModel article);
        public void DeleteArticle(int id);
        public void CreateCategory(CategoryViewModel category);
        public void EditCategory(Category category);
        public void DeleteCategory(int id);
        public void CreateTag(TagViewModel tag);
        public void DeleteTag(int id);
        public bool SaveAll();
    }
}