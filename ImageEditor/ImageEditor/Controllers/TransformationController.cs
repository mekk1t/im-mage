using ImageEditor.Models;
using ImageEditor.Models.Transformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace ImageEditor.Controllers
{
    [Route("api/images/transformations")]
    public class TransformationController : Controller
    {
        [Route("rotate")]
        public IActionResult Rotate(RotationRequest request)
        {
            using var image = Image.Load(request.Image.OpenReadStream());
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
            return View("Picture", new PictureViewModel { Base64 = Convert.ToBase64String(ms.ToArray()) });
        }
    }
}
