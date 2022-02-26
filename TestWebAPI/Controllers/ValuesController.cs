﻿namespace TestWebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// The values controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        public ValuesController()
        {
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "value1", "value2" };
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// The get async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet("async")]
        public async Task<ActionResult> GetAsync()
        {
            List<string> values = await this.ValueRepo(default);
            return this.Ok(values);
        }

        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet("async/{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            List<string> values = await this.ValueRepo(id);
            return this.Ok(values);
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /// <summary>
        /// The value repo.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private Task<List<string>> ValueRepo(int id)
        {
            var values = new List<string>();

            if (id == default)
            {
                for (var i = 0; i < 1000; i++)
                {
                    values.Add($"value{i}");
                }
            }
            else
            {
                values.Add($"value{id}");
            }

            return Task.FromResult(values);
        }
    }
}