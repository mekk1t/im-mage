using ImageEditor.Models;
using ImageEditor.Models.Transformation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static System.IO.File;

namespace ImageEditor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Picture", new PictureViewModel());
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var image = Convert.ToBase64String(ms.ToArray());
            return View("Picture", new PictureViewModel { ContentType = file.ContentType, Base64 = image });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("files/{fileName}")]
        public IActionResult Save([FromRoute] string fileName, [FromQuery] string contentType)
        {
            var fileContent = ReadAllBytes(fileName);

            return File(fileContent, contentType, fileName);
        }

        [HttpPost("files")]
        public IActionResult Upload([FromBody] ImageRequest request)
        {
            using var image = request.Base64.AsImageSharp();
            using var ms = new MemoryStream();

            switch (request.ContentType)
            {
                case "image/jpeg":
                case "image/jpg":
                    image.SaveAsJpeg(ms);
                    break;
                case "image/png":
                    image.SaveAsPng(ms);
                    break;
                case "image/bmp":
                    image.SaveAsBmp(ms);
                    break;
            }

            var fileName = $"{DateTime.UtcNow.ToShortDateString().Replace("/", "-")}_image.{request.ContentType.Substring(request.ContentType.IndexOf("/") + 1)}";

            WriteAllBytes(fileName, ms.ToArray());

            return Ok(new { FileName = fileName });
        }
    }
}
