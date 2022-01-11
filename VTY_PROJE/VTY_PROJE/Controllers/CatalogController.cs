using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTYS_PROJE.Business.Abstract;
using VTYS_PROJE.Business.Concrete;
using VTYS_PROJE.Core.Results;
using VTYS_PROJE.Entities.Concrete;

namespace VTY_PROJE.Controllers
{
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductTypeService _productTypeService;
        private readonly INoSqlService _noSqlService;

        public CatalogController(IProductService productService, ICategoryService categoryService, IProductTypeService productTypeService, INoSqlService noSqlService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productTypeService = productTypeService;
            _noSqlService = noSqlService;
        }

        [Authorize]
        [HttpGet]
        [Route("catalog")]
        public IActionResult Index()
        {
            Response<List<Category>> responseCategory = _categoryService.GetAll();
            ViewBag.CategoryList = responseCategory.Data;

            Response<List<ProductType>> responseProductType = _productTypeService.GetAll();
            ViewBag.ProductTypeList = responseProductType.Data;

            Response<int> response = _productService.Count();

            if (response.IsSuccessful)
            {
                ViewBag.DataCount = response.Data;
            }
            else
                ViewBag.DataCount = "-Error-";


            return View("views/catalog/catalog.cshtml");
        }


        [HttpGet]
        [Route("catalog/search/detail/{id}")]
        public IActionResult detailProduct(int id)
        {
            Response<Product> response = _productService.GetById(id);
            if (response.IsSuccessful && response.Data != null)
            {
                ViewBag.Data = response.Data;
                ViewBag.Base64 = _noSqlService.GetById(response.Data.no_sql_property_id).Data.images[0];
            }

            return View("views/catalog/detail.cshtml");
        }

        [HttpGet]
        [Route("catalog/search")]
        public IActionResult Search(int minPrice, int maxPrice, string category, int type, int page, string search_txt)
        {
            ViewBag.Page = page;
            ViewBag.search_txt = search_txt;

            int category_id = (category != null && category != "undefined") ? int.Parse(category) : 0;
            string __category = "";

            if (category_id != 0)
            {
                var data = _categoryService.GetAll(x => x.id == category_id).Data.FirstOrDefault();
                if (data != null)
                    __category = data.category_name;
            }

            int _type = type;
            string type_response = "";
            if (type != 0)
            {
                var data = _productTypeService.GetAll(x => x.id == _type).Data.FirstOrDefault();
                if (data != null)
                    type_response = data.type_name;
            }

            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;

            ViewBag.category = __category;
            ViewBag.type = type_response;
            string s = ((ViewBag.type != "") ? "value=\"" + ViewBag.type + "\"" : "");
            /*
             * İyileştirme Yap
             * Parametrelerin tamamı sql sorgusunda çalışsın
             * 
             * */

            string _category = category;
            int _type_id = type;
            Response<List<Product>> response;
            if (type != 0)
                response = _productService.GetAll(x => x.product_type_id == _type_id);
            else
                response = _productService.GetAll();

            int min = (page == 1 || page == 0) ? 0 : (page - 1) * 10;
            int _maxPrice = (maxPrice == 0) ? int.MaxValue : maxPrice;

            var products = response.Data.Where(x => x.price >= minPrice && x.price <= _maxPrice);

            if (search_txt != null) { 
                products = products.Where(x => x.title.ToLower().Contains(search_txt.ToLower()));
                ViewBag.search_txt = search_txt;
            }
            else
            {
                ViewBag.search_txt = "";
            }

            if (category_id != 0)
            {
                products = products.Where(x => x.category_name == __category);
            }

            ViewBag.AllProductCount = products.Count();

            products = products.Skip(min).Take(10);

            List<(Product product, ProductProperty property)> _products = new List<(Product x, ProductProperty y)>();
            foreach (Product pr in products)
            {
                Response<ProductProperty> response2 = _noSqlService.GetById(pr.no_sql_property_id);
                _products.Add((pr, response2.Data));
            }


            ViewBag.Products = _products;
            ViewBag.ProductCount = products.Count();


            /*if (search_txt != null && category_id == 0)
            {
                ViewBag.AllProductCount = _productService.Count(x =>
                                                                x.price >= minPrice
                                                                && x.price <= _maxPrice
                                                                && x.title.ToLower().Contains(search_txt.ToLower())).Data;
            }
            else if (search_txt == null && category_id != 0)
            {
                ViewBag.AllProductCount = _productService.Count(x => x.price >= minPrice
                                                                && x.price <= _maxPrice
                                                                && x.category_name == __category).Data;
            }
            else if(search_txt != null && category_id != 0)
            {
                ViewBag.AllProductCount = _productService.Count(x =>
                                                                x.price >= minPrice
                                                                && x.price <= _maxPrice && x.title.ToLower().Contains(search_txt.ToLower())
                                                                && x.category_name == __category).Data;
            }
            else
            {
                ViewBag.AllProductCount = _productService.Count(x =>
                                                                x.price >= minPrice
                                                                && x.price <= _maxPrice).Data;
            }*/


            Response<List<Category>> responseCategory = _categoryService.GetAll();
            ViewBag.CategoryList = responseCategory.Data;

            Response<List<ProductType>> responseProductType = _productTypeService.GetAll();

            /*if (category_id != 0)
                ViewBag.ProductTypeList = responseProductType.Data.Where(x => x.category_id == category_id).ToList();
            else
                ViewBag.ProductTypeList = responseProductType.Data.ToList();*/

            ViewBag.ProductTypeList = responseProductType.Data.ToList();

            return View("views/catalog/search.cshtml");
        }
    }
}
