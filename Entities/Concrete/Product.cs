using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}
/*
Bire çok
bir ürünün bir kategorisi olabilir, ama bir kategorinin birden fazla ürünü olabilir.
    //var a = context.Set<Product>().Where(p => p.CategoryId == 1);
 */
