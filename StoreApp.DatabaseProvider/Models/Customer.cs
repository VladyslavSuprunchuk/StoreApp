namespace StoreApp.DatabaseProvider.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public List<Order> Orders { get; set; }
    }
}
