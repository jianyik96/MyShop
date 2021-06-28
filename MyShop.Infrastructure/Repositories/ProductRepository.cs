using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(ShoppingContext context) : base(context)
        {
        }

        /* Overriding the default behaviour of Update will allow
           to control the changes you arpprove to be changed */
        public override Product Update(Product entity)
        {
            var product = context.Products
                .Single(p => p.ProductId == entity.ProductId);

            product.Name = entity.Name;
            product.Price = entity.Price;

            return base.Update(product);
        }

    }
}
