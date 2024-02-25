using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Core.Models;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(
            IMapper mapper,
            IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] ProductClient productClient)
        {
            var product = _mapper.Map<Product>(productClient);
            product = await _productService.CreateProductAsync(product);

            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productClient = _mapper.Map<Product>(product);

            return Ok(productClient);
        }

        [HttpGet()]
        public ActionResult GetAll()
        {
            var products = _productService.GetAll();
            var productClients = _mapper.Map<List<Product>>(products);

            return Ok(productClients);
        }
    }
}
