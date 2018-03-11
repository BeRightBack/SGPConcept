using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SGPConcept.Models.Store;
using SGPConcept.Models.ViewModels;

namespace SGPConcept.Controllers
{
    public class ProductController : BaseController
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        //[Route ("/Store")]
        public ViewResult List(string category, int productPage = 1)
             => View(new ProductsListViewModel
             {
                 Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                     .OrderBy(p => p.ProductID)
                     .Skip((productPage - 1) * PageSize)
                     .Take(PageSize),
                 PagingInfo = new PagingInfo
                 {
                     CurrentPage = productPage,
                     ItemsPerPage = PageSize,
                     TotalItems = category == null ?
                         repository.Products.Count() :
                         repository.Products.Where(e =>
                            e.Category == category).Count()

                 },
                 CurrentCategory = category
             });


    }
}