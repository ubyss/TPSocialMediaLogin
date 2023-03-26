using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TPSocialMedia.Data;
using TPSocialMedia.Models;
using TPSocialMedia.Services;
using static MVCClient.Models.ObjectUser;

namespace MVCClient.Controllers.Authentication
{
    public class AuthController : Controller
    {
        public readonly ApplicationDbContext db;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(ApplicationDbContext aplicationContext, UserManager<IdentityUser> userManager)
        {
            db = aplicationContext;
            _userManager = userManager;
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


            if (userSession == null) return RedirectToAction("Index", "Auth");

            if (TempData["isVisitor"] != null)
            {
                ViewBag.isVisitor = TempData["isVisitor"];
                ViewBag.imageLink = TempData["image"];
            }
            
            ViewBag.imageLink = null;

            using (var httpClient = new HttpClient())
            {
                
                var response = await httpClient.GetAsync("https://localhost:5001/api/Auth/User/" + userSession);

                var responseContent = await response.Content.ReadAsStringAsync();

                IdentityUserData UserDetails = JsonConvert.DeserializeObject<IdentityUserData>(responseContent);

                var responseImage = await httpClient.GetAsync("https://localhost:5001/api/Images/profilePicture/" + userSession);

                var responseContentImage = await responseImage.Content.ReadAsStringAsync();


                ViewBag.isVisitor = false;
                ViewBag.imageLink = responseContentImage;

                return View("Profile", UserDetails);
            }
        }

        public async Task<IActionResult> UserProfile(string? id)
        {
            ViewBag.isVisitor = true;

            using (var httpClient = new HttpClient())
            {

                var response = await httpClient.GetAsync("https://localhost:5001/api/Auth/User/" + id);

                var responseContent = await response.Content.ReadAsStringAsync();

                IdentityUserData UserDetails = JsonConvert.DeserializeObject<IdentityUserData>(responseContent);

                if (id != null)
                {

                    var responseImage = await httpClient.GetAsync("https://localhost:5001/api/Images/profilePicture/" + id);

                    var responseContentImage = await responseImage.Content.ReadAsStringAsync();


                    ViewBag.imageLink = responseContentImage;



                }
                return View("Profile", UserDetails);
            }
        }

        public IActionResult Register()
        {
            string userSession = HttpContext.Session.GetString("UserToken");
            if (userSession != null) return RedirectToAction("Index", "Auth");
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

                if (!string.IsNullOrEmpty(id))
                {
                    HttpContext.Session.SetString("UserToken", id);
                    TempData["isVisitor"] = false;
                    TempData["image"] = null;

                    return RedirectToAction("Profile", "Auth");

                }
                else
                {
                return RedirectToAction("Login", "Auth");

                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProcessUpdate(IdentityUserData user, IFormFile file)
        {
            var userId = user.id;


            if (file != null && file.Length > 0)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        using (var fileContent = new StreamContent(file.OpenReadStream()))
                        {
                            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "file",
                                FileName = Path.GetFileName(file.FileName),
                            };

                            content.Add(fileContent, "file");
                            content.Add(new StringContent(userId), "userId");

                            var response = await httpClient.PostAsync("https://localhost:5001/api/Images/upload/" + userId, content);

                            if (response.IsSuccessStatusCode)
                            {
                                var responseContent = await response.Content.ReadAsStringAsync();
                                var responseObject = JsonConvert.DeserializeObject<JObject>(responseContent);
                                var imageUrl = responseObject["blob"].ToString();

                            }
                            else
                            {
                                // Tratar a falha na requisição
                            }
                        }
                    }
                }
            }


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

                var response = await httpClient.PutAsync("https://localhost:5001/api/Auth/User/" + userSession, content);

                var responseContent = await response.Content.ReadAsStringAsync();

                IdentityUserData meuobjeto = JsonConvert.DeserializeObject<IdentityUserData>(responseContent);


                //return Ok(meuObjeto);

                return RedirectToAction("Profile", "Auth");
            }
        }
    }
}
