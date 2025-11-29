using Panda.Models;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace Panda.ViewModels
{
    public class CreatePackageViewModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public string Recipient { get; set; }
    }
}
