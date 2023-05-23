using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.API.Entities;
using Product.API.Repositories.Interfaces;
using Shared.DTOs;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly IProductRepository repo;
        private readonly IMapper mapper;

        public ProductsController(IProductRepository _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var producs = await repo.GetPruducts();
            var resul = mapper.Map<IEnumerable<ProductDTO>>(producs);
            return Ok(resul);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProduct([Required] long id)
        {
            var product = await repo.GetByIdAsync(id);
            if(product == null) return NotFound();
            var resul = mapper.Map<ProductDTO>(product);
            return Ok(resul);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProductDTO productdto)
        {
            var exist = await repo.GetProductByNo(productdto.No);
            if(exist !=null) Console.WriteLine($"product json {productdto.No} product exist {exist.No} id {exist.Id}");
            if(exist != null) return BadRequest();
            var product = mapper.Map<CatalogProduct>(productdto);
            await repo.CreateProduct(product);
            await repo.SaveChangeAsync();
            var resul = mapper.Map<ProductDTO>(product);
            return Ok(resul);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProduct([Required] long id, [FromBody] UpdateProductDTO uProduct)
        {
            var product = await repo.GetProductByID(id);
            if(product == null) return NotFound();
            
            var updateproduct = mapper.Map<UpdateProductDTO,CatalogProduct>(uProduct, product);
            await repo.UpdateProduct(product);
            await repo.SaveChangeAsync();
            var resul = mapper.Map<ProductDTO>(product);

            return Ok(resul);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct([Required]long id)
        {
            var product = await repo.GetProductByID(id);
            if(product == null) return NotFound();
            await repo.DeleteProduct(id);
            await repo.SaveChangeAsync();

            return NoContent();
        }

        [HttpGet("get-product-by-no/{no}")]
        public async Task<IActionResult> GetproductByNo([Required]string no)
        {
            var product = await repo.GetProductByNo(no);
            if(product == null) return NotFound();
            var resul = mapper.Map<ProductDTO>(product);
            return Ok(resul);

        }

        
    }
}