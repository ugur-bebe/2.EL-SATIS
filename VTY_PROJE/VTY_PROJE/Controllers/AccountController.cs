using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VTYS_PROJE.Business.Concrete;
using VTYS_PROJE.Core.Results;
using VTYS_PROJE.Entities.Concrete;

namespace VTY_PROJE.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districService;
        private readonly IAddressService _addressService;

        public AccountController(IUserService userService, ICityService cityService, IDistrictService districtService, IAddressService addressService)
        {
            _userService = userService;
            _cityService = cityService;
            _districService = districtService;
            _addressService = addressService;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult login_page()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            HttpContext.Session.Remove("user_id");

            return View("views/index.cshtml");
        }

        [HttpGet]
        [Route("sign-in")]
        public IActionResult sign_page()
        {
            Response<List<City>> responseCity = _cityService.GetAll();
            Response<List<District>> responseDistrict = _districService.GetAll();
            Response<List<Address>> responseAddress = _addressService.GetAll();
                        

            ViewBag.CityList = responseCity.Data;
            ViewBag.DistrictList = responseDistrict.Data;
            ViewBag.AddressList = responseAddress.Data;

            return View("views/account/sign-in.cshtml");
        }


        [HttpPost]
        [Route("login_check")]
        public async Task<IActionResult> login_check(User u)
        {
            string userName = u.user_name;
            string password = u.password;
            var user = _userService.GetAll(x => x.user_name == userName && x.password == password);
            if (user.Data.Count != 0)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, u.user_name)
                };

                var identity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(claimsPrincipal);

                HttpContext.Session.SetString("user_id", user.Data.FirstOrDefault().id.ToString());
                HttpContext.Session.SetString("user_type", user.Data.FirstOrDefault().user_type.ToString());

                return RedirectToAction(nameof(CatalogController.Index), "Catalog");
            }
            return StatusCode(204);
        }


        [HttpPost]
        [Route("sign_in")]
        public IActionResult sign_in(User user)
        {
            //Set user_type to Standart User
            user.user_type = 2;
            //Set Address to Empty Address
            //user.address_id = 1;

            int city_id = int.Parse(user.city_name);
            int district_id = int.Parse(user.city_name);

            //Address a = new Address();
            var res = _addressService.GetAll(x => x.city_id == city_id && x.district_id == district_id);
            //a.id = res.Data.FirstOrDefault().id;

            //a.city_id = int.Parse(user.city_name);
            //a.district_id = int.Parse(user.district_name);
            //a.address_description = user.address_description;

            //var address = _addressService.Update(a);
            if (res.IsSuccessful && res.Data != null)
            {
                user.city_name = null;
                user.district_name = null;
                user.address_description = null;
                user.address_id = res.Data.FirstOrDefault().id;
                user.user_type = 1;

                Response<NoContext> response = _userService.Add(user);

                if (response.IsSuccessful)
                {
                    return StatusCode(201);
                }
            }


            return StatusCode(204);
        }


        [HttpGet]
        [Route("time-out")]
        public IActionResult time_out()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            HttpContext.Session.Remove("user_id");

            return View("views/time-out.cshtml");
        }
    }
}
