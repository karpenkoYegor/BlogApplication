using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BlogApplication.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace BlogApplication.Data
{
    public class BlogSeeder
    {
        private readonly BlogContext _ctx;
        private readonly IHostEnvironment _env;
        private readonly UserManager<BlogUser> _userManager;

        public BlogSeeder
            (BlogContext ctx, IWebHostEnvironment env, UserManager<BlogUser> userManager)
        {
            _userManager = userManager;
            _env = env;
            _ctx = ctx;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            BlogUser user = await _userManager.FindByEmailAsync("admin@blogapp.com");

            if (user == null)
            {
                user = new BlogUser()
                {
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@blogapp.com",
                    UserName = "admin"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            if (!_ctx.Articles.Any())
            {
                var filePath = Path.Combine(_env.ContentRootPath, "Data/tags.json");
                var json = File.ReadAllText(filePath);
                var tags = JsonSerializer.Deserialize<IEnumerable<Tag>>(json);
                _ctx.Tags.AddRange(tags);
                _ctx.SaveChanges();
                filePath = Path.Combine(_env.ContentRootPath, "Data/categories.json");
                json = File.ReadAllText(filePath);
                var categories = JsonSerializer.Deserialize<IEnumerable<Category>>(json);
                _ctx.Categories.AddRange(categories);
                _ctx.SaveChanges();
                filePath = Path.Combine(_env.ContentRootPath, "Data/article.json");
                json = File.ReadAllText(filePath);
                var articles = JsonSerializer.Deserialize<IEnumerable<Article>>(json);
                _ctx.Articles.AddRange(articles);
                _ctx.SaveChanges();
                filePath = Path.Combine(_env.ContentRootPath, "Data/articleTag.json");
                json = File.ReadAllText(filePath);
                var articleTags = JsonSerializer.Deserialize<IEnumerable<ArticleTags>>(json);
                _ctx.ArticleTags.AddRange(articleTags);
                _ctx.SaveChanges();
            }
        }
    }
}