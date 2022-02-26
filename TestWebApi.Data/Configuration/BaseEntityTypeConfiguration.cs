using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestWebApi.Domain.Entities;

namespace TestWebApi.Data.Configuration
{
    public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
        where T: BaseEntity
    {
        /// <summary>
        /// Note that the Configure() method has been marked virtual. This makes it possible for child entity type 
        /// configuration classes to define their own configuration on top of what the base class already provides.
        /// </summary>
        /// <param name="builder"></param>
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            builder.Property(p => p.UpdatedOn).ValueGeneratedOnUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        }
    }
}
