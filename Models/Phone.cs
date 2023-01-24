using System.ComponentModel.DataAnnotations;

namespace BraintreePaymentApp.Models
{
    public class Phone
    {
        [Key]
        public Guid Id { get; set; }
        public string? Producer { get; set; }
        public string? Receiver { get; set; }
        public string? Model { get; set; }
        public double Price { get; set; }

    }
}
