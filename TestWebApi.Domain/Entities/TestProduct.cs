﻿namespace TestWebApi.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The product.
    /// </summary>
    [Table("Products")]
    public class TestProduct : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
