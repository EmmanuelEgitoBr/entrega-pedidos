using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Application.Utils;

public static class PriceCalculator
{
    public static decimal GetTotalPrice(IList<Item> items)
    {
        decimal totalPrice = 0;

        if (items != null && items.Count > 0)
        {
            foreach (var item in items)
            {
                totalPrice = totalPrice + item.Product!.Price * Convert.ToDecimal(item.Count);
            }
        }

        return totalPrice;
    }
}
