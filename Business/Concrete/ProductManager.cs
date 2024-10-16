using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ILogger _logger;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ILogger logger, ICategoryService categoryService)
        {
            _productDal = productDal;
            _logger = logger;
            _categoryService = categoryService;
        }
        //Claim 
        //[SecuredOperation("product.add")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategory(product), CheckProductNameIsAgain(product), CheckCategoryCount());
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult("Ekleme işlemi tamamlandı");
        }

        [CacheAspect]
        public IDataResult<List<ProductDetailDto>> GetAll()
        {
            //iş kodları
            if (DateTime.Now.Hour == 8)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.ProductListError);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetailDtos(), Messages.ProductListSuccesfull);
        }
        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }
        public IDataResult<List<Product>> GetAllByPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetailDtos()
        {
            if (DateTime.Now.Hour == 15)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.ProductListError);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetailDtos());
        }
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {

            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result! < 10)
            {
                return new ErrorResult("Ürün Güncellenemedi");
            }
            _productDal.Update(product);
            return new SuccessResult("Ürün Güncellendi");
        }
        //[TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            return new SuccessResult();
        }

        private IResult CheckIfProductCountOfCategory(Product product)
        {
            //select count gibi  
            //select count(*) from products where categoryId =1
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count();
            if (result >= 10)
            {
                return new ErrorResult("Verilen kategori adına sınır aşıldı");
            }
            return new SuccessResult();
        }
        private IResult CheckProductNameIsAgain(Product product)
        {
            var result = _productDal.GetAll(p => p.ProductName == product.ProductName).Any();
            if (result)
            {
                return new ErrorResult("Böyle bir ürün zaten veritabanında mevcut");
            }
            return new SuccessResult();

        }
        private IResult CheckCategoryCount()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count() > 15)
            {
                return new ErrorResult("Kategori sayısı aşıldı");
            }
            return new SuccessResult("Ürün eklendi");
        }

    }
}
