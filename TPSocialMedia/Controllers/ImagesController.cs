using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPSocialMedia.Data;
using TPSocialMedia.Models;

namespace TPSocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ApplicationDbContext _db;
        public ImagesController(ApplicationDbContext db)
        {
            _connectionString = "DefaultEndpointsProtocol=https;AccountName=atpbthiagovinicius;AccountKey=qtbPQbY7n4oF4Io3ln93jKcX9qAzmXoSOikTYs543O0m4htUfjnweLEFiSUlOLQXDGcYldzYkeNT+ASt4V462w==;EndpointSuffix=core.windows.net";
            _db = db;
        }

        [HttpPost("upload/{id}")]
        public async Task<IActionResult> UploadImage(IFormFile file, string id)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("blobatpbthiagovinicius");

                    string fileName = Path.GetFileName(file.FileName);
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);


                    await blobClient.UploadAsync(stream, overwrite: true);

                    var blobImage = new UserImage()
                    {
                        ImageUrl = blobClient.Uri.ToString(),
                        UserId = id,
                    };

                    _db.Add(blobImage);
                    _db.SaveChanges();
                    return Ok(new { blob = blobClient.Uri.ToString(), Message = "Salvo com sucesso" });
                }
            }
            else
            {
                return BadRequest("Arquivo inválido.");
            }
        }

        [HttpGet("profilePicture/{id}")]
        public async Task<IActionResult> RetriveProfilePicture(string id)
        {
            var userImage = await _db.UserImages.FirstOrDefaultAsync(i => i.UserId == id);

            if (userImage == null)
            {
                return NotFound("Nenhuma imagem foi encontrada");
            }

            return Ok(userImage.ImageUrl);
        }

        [HttpGet("gallery")]
        public async Task<IActionResult> getGallery()
        {
            var images = await _db.UserImages.ToListAsync();

            if (images == null)
            {
                return NotFound("Nenhuma imagem foi encontrada");
            }

            return Ok(images);

        }
    }
}
