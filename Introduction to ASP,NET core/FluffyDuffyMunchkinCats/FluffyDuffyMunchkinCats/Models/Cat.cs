
using System.ComponentModel.DataAnnotations;

namespace FluffyDuffyMunchkinCats.Models
{
    public class Cat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public string ImageUrl { get; set; }
    }
}
