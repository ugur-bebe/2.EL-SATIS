using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using VTYS_PROJE.Business.Abstract;
using VTYS_PROJE.Business.Concrete;
using VTYS_PROJE.Core.Results;
using VTYS_PROJE.Entities.Concrete;

namespace VTY_PROJE.Controllers
{
    [ApiController]
    public class PruductController : Controller
    {
        private readonly IProductService _productService;
        private readonly INoSqlService _noSqlService;

        public PruductController(IProductService productDal, INoSqlService noSqlService)
        {
            this._productService = productDal;
            _noSqlService = noSqlService;
        }

        [Authorize]
        [HttpGet]
        [Route("product/add-new")]
        public IActionResult AddNew()
        {
            string id = HttpContext.Session.GetString("user_id");
            if (id == null)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                return RedirectToAction(nameof(AccountController.time_out), "Account");
            }
            else
            {
                return View("views/product/new-product.cshtml");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("product/my-product")]
        public IActionResult getAllMyProduct()
        {
            string id = HttpContext.Session.GetString("user_id");
            //string id = "7";
            if (id == null)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                return RedirectToAction(nameof(AccountController.time_out), "Account");
            }
            else
            {
                int _id = int.Parse(id);
                Response<List<Product>> response = _productService.GetAll(x => x.owner_id == _id);

                List<(Product product, ProductProperty property)> products = new List<(Product x, ProductProperty y)>();

                if (response.IsSuccessful)
                {
                    foreach (Product product in response.Data)
                    {
                        Response<ProductProperty> response2 = _noSqlService.GetById(product.no_sql_property_id);
                        products.Add((product, response2.Data));
                    }

                    ViewBag.ProductList = products;
                }
            }
            return View("views/product/my-all-product.cshtml");
        }

        [Authorize]
        [HttpGet]
        [Route("product/edit-product/{id:int}")]
        public IActionResult editProduct(int id)
        {
            string _user_id = HttpContext.Session.GetString("user_id");
            //string id = "7";
            if (_user_id == null)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                return RedirectToAction(nameof(AccountController.time_out), "Account");
            }
            else
            {
                int _id = id;
                int _ownerId = int.Parse(_user_id);
                Response<List<Product>> response = _productService.GetAll(x => x.id == _id && x.owner_id == _ownerId);

                if (response.IsSuccessful && response.Data != null && response.Data.Count != 0)
                {
                    ViewBag.title = response.Data.FirstOrDefault().title;
                    ViewBag.price = response.Data.FirstOrDefault().price;
                    ViewBag.explanation = response.Data.FirstOrDefault().explanation;
                    ViewBag.product_type = response.Data.FirstOrDefault().type_name;
                    ViewBag.product_id = response.Data.FirstOrDefault().id;

                    return View("views/product/edit.cshtml");
                }
            }

            ViewBag.ProductList = new List<Product>();
            return View("views/product/my-all-product.cshtml");
        }

        [Authorize]
        [HttpPost]
        [Route("product/new-product")]

        public IActionResult NewProduct(Product product)
        {
            string id = HttpContext.Session.GetString("user_id");
            if (id == null)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                return RedirectToAction(nameof(AccountController.time_out), "Account");
            }
            else
            {
                product.owner_id = int.Parse(id);
                product.release_date = DateTime.Now;

                ProductProperty p = new ProductProperty() { images = new List<string>() { product.base64 } };

                Response<NoContext> response2 = _noSqlService.Add(ref p);

                product.no_sql_property_id = p._id.ToString();
                Response<NoContext> response = _productService.Add(product);

                if (response.IsSuccessful)
                {
                    return StatusCode(201);
                }

                return StatusCode(204);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("product/update-product")]
        public ActionResult UpdateProduct(Product u)
        {
            string id = HttpContext.Session.GetString("user_id");
            u.owner_id = int.Parse(id);

            Response<NoContext> response = _productService.Update(u);

            if (response.IsSuccessful)
            {
                return StatusCode(201);
            }

            return StatusCode(204);
        }

        [Authorize]
        [HttpPost]
        [Route("product/delete-product")]
        public ActionResult deleteProduct([FromHeader] int id)
        {
            string _user_id = HttpContext.Session.GetString("user_id");
            //string id = "7";
            if (_user_id == null)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                return RedirectToAction(nameof(AccountController.time_out), "Account");
            }
            else
            {
                int _id = id;
                int _ownerId = int.Parse(_user_id);
                Response<List<Product>> responseProduct = _productService.GetAll(x => x.id == _id && x.owner_id == _ownerId);

                if (responseProduct.IsSuccessful && responseProduct.Data != null && responseProduct.Data.Count != 0)
                {
                    Response<NoContext> responseDelete = _productService.Delete(id);
                    if (responseDelete.IsSuccessful)
                    {
                        return StatusCode(201);
                    }
                }
            }
            return StatusCode(204);
        }

        [HttpGet]
        [Route("product/my-product-json")]
        public IActionResult getAllMyProductJson()
        {
            Response<List<Product>> response = _productService.GetAll(x => x.id == 7);

            if (response.IsSuccessful)
            {
                JsonSerializerOptions jso = new JsonSerializerOptions();
                jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                var json = JsonSerializer.Serialize(response, jso);

                return Ok(json);
            }

            return StatusCode(204);
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            var list = _productService.GetAll();
            var json = JsonSerializer.Serialize(list, jso);

            return Ok(json);
        }

        [HttpGet("Update")]
        public ActionResult Update(Product u)
        {
            Response<NoContext> response = _productService.Update(u);

            if (response.IsSuccessful)
            {
                return StatusCode(201);
            }

            return StatusCode(204);
        }

        [HttpGet("Delete")]
        public ActionResult Delete(int id)
        {
            Response<NoContext> response = _productService.Delete(id);

            if (response.IsSuccessful)
            {
                return StatusCode(201);
            }

            return StatusCode(204);
        }

        [HttpGet("Count")]
        public ActionResult Count()
        {
            Response<int> response = _productService.Count();

            if (response.IsSuccessful)
            {
                return StatusCode(201, response.Data);
            }

            return StatusCode(204);
        }

        [HttpGet("GetById")]
        public ActionResult getById(int id)
        {
            var list = _productService.GetById(id);
            var json = JsonSerializer.Serialize(list);

            return Ok(json);
        }
    }
}
