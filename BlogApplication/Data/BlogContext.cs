using System;
using System.Collections.Generic;
using BlogApplication.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogApplication.Data
{
    public class BlogContext : IdentityDbContext<BlogUser>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : 
            base(options)
        {
            
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleTags> ArticleTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Article>()
                .HasMany(a => a.Tags)
                .WithMany(t => t.Articles)
                .UsingEntity<ArticleTags>(
                    j => j
                        .HasOne(pt => pt.Tag)
                        .WithMany(t => t.ArticleTags)
                        .HasForeignKey(pt => pt.TagId),  // связь с таблицей Students через StudentId
                    j => j
                        .HasOne(pt => pt.Article)
                        .WithMany(p => p.ArticleTags)
                        .HasForeignKey(pt => pt.ArticleId)
                );

        }
    }
}