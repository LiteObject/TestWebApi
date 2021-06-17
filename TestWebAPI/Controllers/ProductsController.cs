using System.Linq.Expressions;
using TestWebAPI.DTOs;

namespace TestWebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.Extensions.Logging;

    using TestWebApi.Data;
    using TestWebApi.Data.Repositories;
    using TestWebApi.Domain.Entities;

    using TestWebAPI.Library;

    /// <summary>
    /// The Products controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The Product repository.
        /// </summary>
        private readonly IRepository<Product> productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public ProductsController(ILogger<ProductsController> logger, IRepository<Product> repository)
        {
            this.productRepository = repository;
            this.logger = logger;

            this.logger.LogTrace($"{nameof(ProductsController)} class has been instantiated.");
        }

        /// <summary>
        /// The get Product.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await this.productRepository.GetAsync(id);

            if (product == null)
            {
                return this.NotFound($"Invalid Product id: {id}");
            }

            return this.Ok(product);
        }

        /// <summary>
        /// The get Products. Example: /products?productNameLike=apple&
        /// </summary>
        /// <param name="queryParams">
        /// The name Like.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductResourceQueryParams queryParams)
        {
            // [DONE] TODO: Pass pageSize & pageNumber to database query. We don't want to apply Take()/Skip() in-memory.
            var products = await this.productRepository.FindAsync(
                e => queryParams.ProductNamesLike.Contains(e.Name),  // TODO (?): Use EF.Functions.Like();
                queryParams.PageNumber, 
                queryParams.PageSize);
            
            if (!products.Any())
            {
                // [DONE] TODO: Include query param(s) used in the database search.
                return this.NotFound(queryParams);
            }

            var result = new List<ProductResponse>();
            products.ForEach(p => result.Add(ConvertToResponseObject(p)));

            return this.Ok(result);
        }

        /// <summary>
        /// The post Product.
        /// </summary>
        /// <param name="product">
        /// The Product.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this.productRepository.Add(product);
            await this.productRepository.SaveChangesAsync();

            return this.CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [NonAction]
        public Expression<Func<Product, bool>> RequestBuilderAsync(ProductResourceQueryParams queryParams)
        {
            var predicate = PredicateBuilder.True<Product>();
            
            if(queryParams.ProductNamesLike is not null && queryParams.ProductNamesLike.Any() == true)
            {
                predicate = predicate.And(p => EF.Functions.Like(p.Name, ""));
            }

            if (queryParams.CreatedOnOrAfter.HasValue)
            {
                predicate = predicate.And(p => p.CreatedOn >= queryParams.CreatedOnOrAfter);
            }
            
            return predicate;
        }

        /// <summary>
        /// HATEOAS (Hypermedia as the Engine of Application State)
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [NonAction]
        public ProductResponse ConvertToResponseObject(Product product)
        {
            var hostAddress = "localhost";
            var href = $"{hostAddress}/{nameof(Product)}/{product.Id}";
            
            var response = new ProductResponse
            {
                Id = product.Id, 
                Name = product.Name,
                Links = new List<BaseResponse.Link>()
                {
                    new() { Action = "GET", Rel = $"{nameof(Product)}", Href = href},
                    new() { Action = "PUT", Rel = $"{nameof(Product)}", Href = href}
                }
            };
            
            return response;
        }
    }
}