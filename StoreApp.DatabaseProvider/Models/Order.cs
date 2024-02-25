using StoreApp.Core.Enums;

namespace StoreApp.DatabaseProvider.Models
{
    public class Order
    {
        public int Id { get; set; } 

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public bool IsPaid { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public PaymentType PaymentType { get; set; }

        public List<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}
