using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApplication.Data.Entities
{
    public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; } = new List<Article>();
        public ICollection<ArticleTags> ArticleTags { get; set; } = new List<ArticleTags>();
    }
}
