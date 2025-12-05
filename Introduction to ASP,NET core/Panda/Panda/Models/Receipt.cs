using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panda.Models
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        public double Fee { get; set; }
        public DateTime IssuedOn { get; set; }
        public int RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public User Recipient { get; set; }
        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public Package Package { get; set; }
    }
}
