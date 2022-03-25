using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace BlogApplication.Data.Entities
{
    public class Article
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        public string HeroImage { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<ArticleTags> ArticleTags { get; set; } = new List<ArticleTags>();
        public DateTime ArticleTime { get; set; }
    }
}
