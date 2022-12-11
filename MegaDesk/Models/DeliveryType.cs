using System.ComponentModel.DataAnnotations;

namespace MegaDesk.Models
{
    public class DeliveryType
    {
        [Key]
        public int DeliveryTypeId { get; set; }

        [Display(Name = "Delivery Name")]
        public string DeliveryName { get; set; }
        public decimal PriceUnder1000 { get; set; }
        public decimal PriceBetween1000And2000 { get; set; }
        public decimal PriceOver1000 { get; set; }
    }
}
