using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTYS_PROJE.Business.Concrete;
using VTYS_PROJE.Core.Results;
using VTYS_PROJE.Entities.Concrete;

namespace VTY_PROJE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("Insert")]
        public ActionResult Insert(User u)
        {
            Response<NoContext> response = _userService.Add(u);

            if (response.IsSuccessful)
            {

            }

            return Ok();
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            _userService.GetAll();

            return Ok();
        }

        [HttpGet("Update")]
        public ActionResult Update(User u)
        {
            _userService.Update(u);

            return Ok();
        }

        [HttpGet("Delete")]
        public ActionResult Delete(int id)
        {

            _userService.Delete(id);

            return Ok();
        }

        [HttpGet("Count")]
        public ActionResult Count()
        {

            _userService.Count();

            return Ok();
        }

        [HttpGet("GetById")]
        public ActionResult getById(int id)
        {

            _userService.GetById(id);

            return Ok();
        }
    }
}

/*User u = new User();
u.address_id = 3;
u.name = "Uğur47";
u.surname = "BEBE7";
u.user_name = "ugur.bebe37";
u.password = "Sifre12347";
u.phone_number = "1234567897";
u.user_type = 1;

u.id = 7;*/