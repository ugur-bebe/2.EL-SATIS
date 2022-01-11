using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using VTYS_PROJE.Business.Concrete;
using VTYS_PROJE.Core.Results;
using VTYS_PROJE.Entities.Concrete;

namespace VTY_PROJE.Controllers
{
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districService;
        private readonly IAddressService _addressService;

        public ProfileController(IUserService userService, ICityService cityService, IDistrictService districtService, IAddressService addressService)
        {
            _userService = userService;
            _cityService = cityService;
            _districService = districtService;
            _addressService = addressService;
        }

        [Authorize]
        [HttpGet]
        [Route("profile/edit-pw")]
        public IActionResult edit_pw()
        {
            return View("views/Profile/edit-password.cshtml");
        }

        [Authorize]
        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
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
                Response<User> response = _userService.GetById(int.Parse(id));
                Response<List<City>> responseCity = _cityService.GetAll();
                Response<List<District>> responseDistrict = _districService.GetAll();
                Response<List<Address>> responseAddress = _addressService.GetAll();

                ViewBag.user_name = response.Data.user_name;
                ViewBag.name = response.Data.name;
                ViewBag.surname = response.Data.surname;
                ViewBag.phone_number = response.Data.phone_number;
                ViewBag.email = response.Data.email;
                ViewBag.ADDRESS_DESCRIPTION = response.Data.address_description;
                ViewBag.CITY_NAME = response.Data.city_name;
                ViewBag.DISTRICT_NAME = response.Data.district_name;

                ViewBag.CityList = responseCity.Data;
                ViewBag.DistrictList = responseDistrict.Data;
                ViewBag.AddressList = responseAddress.Data;

                return View("views/profile/profile.cshtml");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("profile/edit")]
        public IActionResult editProfile(User u)
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
                Address a = new Address();
                var res = _userService.GetById(int.Parse(id)).Data;
                a.id = res.address_id;
                a.city_id = int.Parse(u.city_name);
                a.district_id = int.Parse(u.district_name);
                a.address_description = u.address_description;

                var address = _addressService.Update(a);
                if (address.IsSuccessful)
                {
                    u.city_name = null;
                    u.district_name = null;
                    u.address_description = null;
                    u.address_id = res.address_id;
                    u.user_type = res.user_type;

                    u.id = int.Parse(id);

                    Response<NoContext> response = _userService.Update(u);

                    if (response.IsSuccessful)
                    {
                        return StatusCode(201);
                    }
                }

                return StatusCode(204);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("profile/edit-password")]
        public IActionResult editPassword([FromHeader] string oldPassword, [FromHeader] string newPassword)
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
                string old_pw = oldPassword;
                string new_pw = newPassword;
                int _id = int.Parse(id);

                Response<List<User>> response = _userService.GetAll(x => x.password == old_pw && x.id == _id);

                if (response.IsSuccessful && response.Data.Count != 0)
                {
                    int user_type = response.Data.FirstOrDefault().user_type;
                    int address_id = response.Data.FirstOrDefault().address_id;
                    Response<NoContext> response1 = _userService.Update(new User { password = new_pw, id = int.Parse(id), user_type = user_type , address_id  = address_id });
                    if (response1.IsSuccessful)
                    {
                        return StatusCode(200);
                    }
                }


                if (response.Data.Count == 0) return StatusCode(404);

                return StatusCode(204);
            }
        }
    }
}

