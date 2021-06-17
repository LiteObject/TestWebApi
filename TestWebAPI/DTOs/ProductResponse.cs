using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebAPI.DTOs
{
    public class ProductResponse: BaseResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
