using System.ComponentModel.DataAnnotations;

namespace NewsFeed.Models
{
    public class News
    {
        [Required(ErrorMessage = "Can`t be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Can`t be empty")]
        public string Name { get; set; }
        
        public int NewsId { get; set; }
        public string Image { get; set; }
    }
}
