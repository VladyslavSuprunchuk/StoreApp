namespace StoreApp.DatabaseProvider.Models
{
    public class ProductOrder
    {
        public int Id { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
