using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Core.Enums;
using StoreApp.Core.Models;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IStripeService _stripeService;

        public OrderController(IMapper mapper, IOrderService orderService, IStripeService stripeService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _stripeService = stripeService;
        }

        [HttpGet("SuccessCheck/{orderId}")]
        public async Task<ActionResult> SuccessCheck([FromRoute] int orderId)
        {
            var isSuccessfullyUpdated = await _orderService.UpdateOrderStatusAsync(orderId);

            if (isSuccessfullyUpdated)
            {
                return Ok();
            }

            return BadRequest("Invalid order id requested");
        }

        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] OrderClient orderClient)
        {
            var customer = _mapper.Map<Customer>(orderClient);
            var productOrders = _mapper.Map<List<ProductOrder>>(orderClient.OrderDetails);
            var order = _mapper.Map<Order>(orderClient);

            order = await _orderService.CreateOrderAsync(order, customer, productOrders);

            if (order.PaymentType == PaymentType.Card)
            {
                var redirectUrl = _stripeService.CreatePayment(order.ProductOrders, order.Id);
                Response.Headers.Add("Location", redirectUrl);

                return Ok();
            }

            return Ok();
        }
    }
}
