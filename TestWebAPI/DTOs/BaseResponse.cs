using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TestWebAPI.DTOs
{
    public abstract class BaseResponse
    {
        public IEnumerable<Link> Links { get; set; }

        public class Link
        {
            public Link() { }

            public Link(string rel, string href, string method)
            {
                this.Rel = rel;
                this.Href = href;
                this.Action = method;
            }

            /// <summary>
            /// Gets or sets Rel, which specifies the relationship between the
            /// current document and the linked document/resource.
            /// </summary>
            public string Rel { get; set; }

            /// <summary>
            /// Gets or Sets Action - HttpVerbs such as GET, POST, PUT, DELETE, etc.
            /// </summary>
            public string Action { get; set; }

            /// <summary>
            /// Gets or Sets Href.
            /// </summary>
            public string Href { get; set; }
        }
    }
}
