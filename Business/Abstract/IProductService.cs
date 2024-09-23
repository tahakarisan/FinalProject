
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IResult Add(Product product);
        IResult Update (Product product);  
        IDataResult<List<Product>> GetAllByPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetailDtos();
        IDataResult<Product> GetById(int productId);
        IResult AddTransactionalTest(Product product);
    }
}
 