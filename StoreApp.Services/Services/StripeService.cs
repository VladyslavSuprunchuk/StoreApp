using Microsoft.Extensions.Options;
using StoreApp.Core.Options;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;
using Stripe.Checkout;

namespace StoreApp.Services.Services
{
    public class StripeService : IStripeService
    {
        private readonly ApiOptions _apiOptions;

        public StripeService(IOptions<ApiOptions> options)
        {
            _apiOptions = options.Value;
        }

        public string CreatePayment(List<ProductOrder> productOrders, int orderId)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = _apiOptions.Baseurl + $"api/Order/SuccessCheck/{orderId}",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };

            foreach (var item in productOrders)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Title
                        }
                    },
                    Quantity = item.Quantity,
                };
                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            var session = service.Create(options);

            return session.Url;
        }
    }
}
