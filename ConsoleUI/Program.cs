﻿using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;
using System.Net.Http.Headers;

//namespace ConsoleUI
//{
//    internal class Program
//    //{
//    //    static void Main(string[] args)
//    //    {
//    //        ProductTest();
//    //        //CategoryTest();
//    //    }

//    //    private static void CategoryTest()
//    //    {
//    //        CategoryManager categoryManager = new CategoryManager(new EFCategoryDal());
//    //        foreach (var category in categoryManager.GetAll())
//    //        {
//    //            Console.WriteLine(category.CategoryName);
//    //        }
//    //    }

//    //    private static void ProductTest()
//    //    {
//    //        ProductManager productManager = new ProductManager(new EFProductDal());
//    //        var result = productManager.GetProductDetailDtos();
//    //        if (result.Success==true)
//    //        {
//    //            foreach (var product in result.Data)
//    //            {
//    //                Console.WriteLine(product.ProductName + " / " + product.CategoryName);
//    //            }
//    //        }
//    //        else
//    //        {
//    //            Console.WriteLine(result.Message);
//    //        }

           
//    //        Console.ReadLine();
//    //    }
//    }
//}
