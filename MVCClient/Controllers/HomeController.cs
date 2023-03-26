using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCClient.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using TPSocialMedia.Data;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext aplicationContext)
        {
            _logger = logger;
            db = aplicationContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> Gallery()
        {
            // Criar um novo HttpClient
            using (var httpClient = new HttpClient())
            {
                // Fazer uma requisição GET para a URL fornecida
                var response = await httpClient.GetAsync("https://localhost:5001/api/Images/gallery");

                // Verificar se a resposta foi bem-sucedida
                if (response.IsSuccessStatusCode)
                {
                    // Ler o conteúdo da resposta como uma string
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Desserializar a resposta JSON em uma lista de objetos UserImageViewModel
                    var images = JsonConvert.DeserializeObject<List<UserImageView>>(jsonResponse);

                    // Retornar a View com a lista de imagens
                    return View(images);
                }
                else
                {
                    // Tratar a falha na requisição
                    return View("Error");
                }
            }
        }


    }
}