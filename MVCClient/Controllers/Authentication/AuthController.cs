using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using TPSocialMedia.Data;
using TPSocialMedia.Models;
using static MVCClient.Models.ObjectUser;

namespace MVCClient.Controllers.Authentication
{
    public class AuthController : Controller
    {
        public readonly ApplicationDbContext db;
        public AuthController(ApplicationDbContext aplicationContext)
        {
            db = aplicationContext;
        }

        public IActionResult Index()
        {
            string userSession = HttpContext.Session.GetString("UserToken");

            if (userSession != null) return RedirectToAction("Profile", "Auth");

            return View();
        }

        public async Task<IActionResult> Profile()
        {
            string userSession = HttpContext.Session.GetString("UserToken");

            if(userSession == null) return RedirectToAction("Index", "Auth");

            using (var httpClient = new HttpClient())
            {
                
                var response = await httpClient.GetAsync("https://localhost:5001/api/Auth/User/" + userSession);

                var responseContent = await response.Content.ReadAsStringAsync();

                IdentityUserData UserDetails = JsonConvert.DeserializeObject<IdentityUserData>(responseContent);

                return View(UserDetails);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessRegister(User user)
        {

            if (user == null)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {

                string jsonStr = JsonConvert.SerializeObject(user);

                var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://localhost:5001/api/Auth/Register", content);

                var responseContent = await response.Content.ReadAsStringAsync();

                JsonData meuObjeto = JsonConvert.DeserializeObject<JsonData>(responseContent);

                if (meuObjeto?.message == "User Registration Successful") return RedirectToAction("Index", "Auth");

                return BadRequest("dados incorretos");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProcessLogin(User user)
        {

            if (user == null)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {

                string jsonStr = JsonConvert.SerializeObject(user);

                var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://localhost:5001/api/Auth/Login", content);

                var responseContent = await response.Content.ReadAsStringAsync();

                JsonData meuObjeto = JsonConvert.DeserializeObject<JsonData>(responseContent);

                if(meuObjeto?.message == "Login Failed") return BadRequest();
                string id = meuObjeto?.identityUser?.id;

                HttpContext.Session.SetString("UserToken", id);

                return RedirectToAction("Profile", "Auth");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProcessUpdate(IdentityUserData user)
        {

            using (var httpClient = new HttpClient())
            {
                string userSession = HttpContext.Session.GetString("UserToken");

                User updatedUser = new()
                {
                    username = user.userName,
                    email = user.email,
                    phoneNumber = user.phoneNumber
                };


                string jsonstr = JsonConvert.SerializeObject(updatedUser);

                var content = new StringContent(jsonstr, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://localhost:5001/api/Auth/User/" + userSession, content);

                var responseContent = await response.Content.ReadAsStringAsync();

                IdentityUserData meuobjeto = JsonConvert.DeserializeObject<IdentityUserData>(responseContent);


                //return Ok(meuObjeto);

                return RedirectToAction("Profile", "Auth");
            }
        }
    }
}
