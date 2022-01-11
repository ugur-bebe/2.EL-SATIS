using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTYS_PROJE.Business.Concrete;
using VTYS_PROJE.Entities.Concrete;

namespace VTY_PROJE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }


        //[HttpGet("{id}/{category_name}")]
        [HttpPost]
        public ActionResult Insert(Category c)
        {
            string user_type = HttpContext.Session.GetString("user_type");

            if (user_type != "2")
            { 
                return Ok("Yönetici Girişi Gerkeli");
            }
            _categoryService.Add(c);

            return Ok();
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            _categoryService.GetAll();

            return Ok();
        }

        [HttpGet("Update")]
        public ActionResult Update(Category u)
        {
            _categoryService.Update(u);

            return Ok();
        }

        [HttpGet("Delete")]
        public ActionResult Delete(int id)
        {

            _categoryService.Delete(id);

            return Ok();
        }

        [HttpGet("Count")]
        public ActionResult Count()
        {

            _categoryService.Count();

            return Ok();
        }

        [HttpGet("GetById")]
        public ActionResult getById(int id)
        {

            _categoryService.GetById(id);

            return Ok();
        }
    }
}
