using ImageEditor.Models;
using ImageEditor.Models.Transformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace ImageEditor.Controllers
{
    [Route("api/images/transformations")]
    public class TransformationController : Controller
    {
        [Route("rotation")]
        [HttpPost]
        public IActionResult Rotate([FromBody] RotationRequest request)
        {
            using var image = Image.Load(Convert.FromBase64String(request.ImageBase64));
            switch (request.Rotation)
            {
                case Rotation.Left90Degrees:
                    image.Mutate(im => im.Rotate(RotateMode.Rotate270));
                    break;
                case Rotation.Right90Degrees:
                    image.Mutate(i => i.Rotate(RotateMode.Rotate90));
                    break;
                case Rotation.UpsideDown:
                    image.Mutate(i => i.Rotate(RotateMode.Rotate180));
                    break;
                default:
                    throw new ArgumentException("Неверное значения типа поворота изображения.");
            }

            using var ms = new MemoryStream();
            image.Save(ms, new JpegEncoder());
            return View("Picture", new PictureViewModel { Base64 = Convert.ToBase64String(ms.ToArray()), ContentType = request.ContentType });
        }

        [Route("mirror")]
        [HttpPost]
        public IActionResult Mirror([FromBody] FlipRequest request)
        {
            using var image = Image.Load(Convert.FromBase64String(request.ImageBase64));
            switch (request.Flip)
            {
                case Flip.Horizontally:
                    image.Mutate(i => i.Flip(FlipMode.Horizontal));
                    break;
                case Flip.Vertically:
                    image.Mutate(i => i.Flip(FlipMode.Vertical));
                    break;
                default:
                    throw new Exception("Неизвестный тип отражения.");
            }

            using var ms = new MemoryStream();
            image.Save(ms, Encoder(request.ContentType));
            return View("Picture", new PictureViewModel { Base64 = Convert.ToBase64String(ms.ToArray()), ContentType = request.ContentType });
        }

        private static IImageEncoder Encoder(string contentType) =>
            contentType switch
            {
                "image/png" => new PngEncoder(),
                "image/jpeg" or "image/jpg" => new JpegEncoder(),
                "image/bmp" => new BmpEncoder(),
                _ => new JpegEncoder(),
            };
    }
}
