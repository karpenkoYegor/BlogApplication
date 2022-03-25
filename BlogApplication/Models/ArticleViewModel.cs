using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlogApplication.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace BlogApplication.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public IFormFile HeroImage { get; set; }
        public string HeroImagePath { get; set; }
        public CategoryViewModel Category { get; set; } 
        public ICollection<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
        public ICollection<ArticleTags> ArticleTags { get; set; } = new List<ArticleTags>();
        public ICollection<int> TagsIds { get; set; } = new List<int>();
        public int[] SelectTags { get; set; }
        public int[] SelectCategories { get; set; }
        public DateTime fromDate;
        public DateTime toDate;

    }
}