using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panda.Models
{
    public enum StatusType { Pending,Shipped, Delivered, Acquired }
    public class Package
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public string ShippingAddress { get; set; }
        public StatusType Status { get; set; }
        public DateTime EstimateDeliveryDate { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User Recipient { get; set; }
    }
}
