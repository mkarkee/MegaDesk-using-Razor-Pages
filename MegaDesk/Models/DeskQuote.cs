using System.ComponentModel.DataAnnotations;
using MegaDesk.Data;

namespace MegaDesk.Models
{
    public class DeskQuote
    {
        private const decimal BASE_DESK_PRICE = 200.00M;
        private const decimal SURFACE_AREA_COST = 1.00M;
        private const decimal DRAWER_COST = 50.00M;

        [Key]
        public int DeskQuoteId { get; set; }

        [Required, Display(Name = "Customer Name")]
        public string? CustomerName { get; set; }

        [Display(Name = "Quote Date")]
        public DateTime? QuoteDate { get; set; }

        [Display(Name = "Quote Price")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal? QuotePrice { get; set; }

        // reference to Desk and DeliveryType classes (foreign keys)
        public int? DeskId { get; set; }

        [Display(Name = "Delivery Type")]
        public int? DeliveryTypeId { get; set; }

        // referencing foreign keys are not enough. We need a way to store data referenced by the foreign key
        public Desk? Desk { get; set; }

        public DeliveryType? DeliveryType { get; set; }

        public decimal GetQuotePrice(MegaDeskContext context)
        {
            decimal quotePrice = BASE_DESK_PRICE;
            decimal surfaceArea = this.Desk.Depth * this.Desk.Width;
            decimal surfacePrice = 0.00M;

            if (surfaceArea > 1000)
            {
                surfacePrice = (surfaceArea - 1000) * SURFACE_AREA_COST;
            }

            decimal drawerPrice = this.Desk.NumDrawers * DRAWER_COST;

            decimal surfaceMaterialPrice = context.DesktopMaterial
                .Where(d => d.DesktopMaterialId == this.Desk.DesktopMaterialId)
                .FirstOrDefault()
                .Cost;

            decimal shippingPrice = 0.00M;
            var shippingPrices = context.DeliveryType
                .Where(d => d.DeliveryTypeId == this.DeliveryTypeId)
                .FirstOrDefault();

            if (surfaceArea < 1000)
            {
                shippingPrice = shippingPrices.PriceUnder1000;
            }
            else if (surfaceArea <= 2000)
            {
                shippingPrice = shippingPrices.PriceBetween1000And2000;
            }
            else
            {
                // oops, this should say price over 2000. I done messed up
                shippingPrice = shippingPrices.PriceOver1000;
            }

            quotePrice += shippingPrice;
            quotePrice += surfacePrice;
            quotePrice += drawerPrice;
            quotePrice += surfaceMaterialPrice;

            return quotePrice;
        }
    }
}
