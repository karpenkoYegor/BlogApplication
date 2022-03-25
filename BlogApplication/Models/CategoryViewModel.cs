using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
