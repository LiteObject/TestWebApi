using Newtonsoft.Json;

namespace TestWebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    
    using TestWebAPI.DTOs;

    /// <summary>
    /// The values controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private ILogger<UsersController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public UsersController(ILogger<UsersController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="resultCount">
        /// The result Count.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] int resultCount = default)
        {
            if (resultCount == default)
            {
                resultCount = 10;
            }

            var users = new List<User>();

            // https://randomuser.me/api/?results=10
            using (var client = new HttpClient()
                                    {
                                        BaseAddress = new Uri("https://randomuser.me"),
                                        Timeout = TimeSpan.FromMinutes(10)
                                    })
            {
                // client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var result = await client.GetAsync($"api/?results={resultCount}");

                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    var userApiResponse = JsonConvert.DeserializeObject<UserApiResponse>(response);
                    users.AddRange(userApiResponse.Results);
                }
            }

            return this.Ok(users);
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

        [HttpGet("diag")]
        public IActionResult CallGC([FromQuery] bool runGc = false)
        {
            var beforeGcTotal = GC.GetTotalMemory(false);

            var p = Process.GetCurrentProcess();

            var beforeGcWorkingSet64 = p.WorkingSet64;
            var beforeGcPrivate = p.PrivateMemorySize64;

            if (runGc)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                var afterGcTotal = GC.GetTotalMemory(true);

                var afterGcWorkingSet64 = p.WorkingSet64;
                var afterGcPrivate = p.PrivateMemorySize64;

                var diagnostics = new
                                      {
                                          BeforeGcTotal = beforeGcTotal,
                                          AfterGcTotal = afterGcTotal,
                                          DifferenceTotal = beforeGcTotal - afterGcTotal,
                                          BeforeGcWorkingSet64 = beforeGcTotal,
                                          BeforeGcPrivate = beforeGcPrivate,
                                          AfterGcWorkingSet64 = afterGcWorkingSet64,
                                          AfterGcPrivate = afterGcPrivate
                                      };

                return this.Ok(diagnostics);

            }
            else
            {
                var diagnostics = new
                                      {
                                          TotalMemory = beforeGcTotal / 1000000,
                                          WorkingSet64 = beforeGcWorkingSet64 / 1000000,
                                          Private = beforeGcPrivate / 1000000
                                      };

                return this.Ok(diagnostics);
            }
        }
    }
}