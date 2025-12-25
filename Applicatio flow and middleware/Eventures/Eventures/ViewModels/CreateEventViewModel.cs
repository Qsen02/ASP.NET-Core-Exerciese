using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Eventures.ViewModels
{
    public class CreateEventViewModel
    {
        [Required]
        [MinLength(10,ErrorMessage = "Name must be at least 10 symbols long!")]
        public string Name { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "Tickets must be non-zero number!")]
        public int TotalTickets { get; set; }
        [Required]
        public double PricePerTicket { get; set; }
    }
}
