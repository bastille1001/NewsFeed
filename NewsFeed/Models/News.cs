using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeed.Models
{
    public class News
    {
        [Required(ErrorMessage = "Can`t be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Can`t be empty")]
        public string Name { get; set; }

        public int Id { get; set; }


        [NotMapped]
        public IFormFile Image { get; set; }

        public string? ImageName { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
