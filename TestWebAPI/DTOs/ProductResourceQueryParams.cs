using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebAPI.DTOs
{
    public class ProductResourceQueryParams : BaseResourceQueryParams
    {
        public string[] ProductNamesLike { get; set; }

        /// <summary>
        /// This property/query param can be used to search for recent records.
        /// </summary>
        public DateTime? CreatedOnOrAfter { get; set; }
    }
}
