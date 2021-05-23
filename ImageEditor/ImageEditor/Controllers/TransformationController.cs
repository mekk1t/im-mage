using ImageEditor.Models;
using ImageEditor.Models.Filters;
using ImageEditor.Models.Transformation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace ImageEditor.Controllers
{
    [Route("api/images")]
    public class TransformationController : Controller
    {
        [Route("transformations/rotation")]
        [HttpPost]
        public IActionResult Rotate([FromBody] RotationRequest request)
        {
            using var image = request.Image.Base64.AsImageSharp();
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

            return PictureView(image, request.Image.ContentType);
        }

        [Route("transformations/brightness")]
        [HttpPost]
        public IActionResult ChangeBrightness([FromBody] BrightnessRequest request)
        {
            using var image = request.Image.Base64.AsImageSharp();
            var brightness = (float)request.BrightnessLevel / (float)100.00;
            image.Mutate(i => i.Brightness(brightness));

            return PictureView(image, request.Image.ContentType);
        }

        [Route("transformations/contrast")]
        [HttpPost]
        public IActionResult ChangeContrast([FromBody] ContrastRequest request)
        {
            using var image = request.Image.Base64.AsImageSharp();
            var contrast = (float)request.ContrastLevel / (float)100.00;
            image.Mutate(context => context.Contrast(contrast));

            return PictureView(image, request.Image.ContentType);
        }

        [Route("transformations/flip")]
        [HttpPost]
        public IActionResult FlipImage([FromBody] FlipRequest request)
        {
            using var image = request.Image.Base64.AsImageSharp();
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

            return PictureView(image, request.Image.ContentType);
        }

        [Route("transformations/zoom")]
        [HttpPost]
        public IActionResult ZoomImage()
        {
            throw new NotImplementedException();
        }

        [Route("filters/sepia")]
        [HttpPost]
        public IActionResult Sepia([FromBody] ImageRequest request)
        {
            using var image = request.Base64.AsImageSharp();
            image.Mutate(context => context.Sepia());
            return PictureView(image, request.ContentType);
        }

        [Route("filters/blackAndWhite")]
        [HttpPost]
        public IActionResult Vintage([FromBody] ImageRequest request)
        {
            using var image = request.Base64.AsImageSharp();
            image.Mutate(context => context.BlackWhite());
            return PictureView(image, request.ContentType);
        }

        [Route("filters/blur")]
        [HttpPost]
        public IActionResult Blur([FromBody] ImageRequest request)
        {
            using var image = request.Base64.AsImageSharp();
            image.Mutate(context => context.BokehBlur());
            return PictureView(image, request.ContentType);
        }

        [Route("filters/nullify")]
        [HttpPost]
        public IActionResult Nullify([FromBody] ImageRequest request)
        {
            using var image = request.Base64.AsImageSharp();
            image.Mutate(context => context.Grayscale());
            return PictureView(image, request.ContentType);
        }

        [Route("filters/negative")]
        [HttpPost]
        public IActionResult Negative([FromBody] ImageRequest request)
        {
            using var image = request.Base64.AsImageSharp();
            image.Mutate(context => context.Invert());
            return PictureView(image, request.ContentType);
        }

        [Route("filters/shades")]
        [HttpPost]
        public IActionResult ShadesOf([FromBody] ShadeRequest request)
        {
            using var image = request.Image.Base64.AsImageSharp();
            switch (request.Shade)
            {
                case Shade.Blue:
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            image[x, y] = new Rgba32(0, 0, image[x, y].B);
                        }
                    }
                    break;
                case Shade.Green:
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            image[x, y] = new Rgba32(0, image[x, y].G, 0);
                        }
                    }
                    break;
                case Shade.Red:
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            image[x, y] = new Rgba32(image[x, y].R, 0, 0);
                        }
                    }
                    break;
            }
            return PictureView(image, request.Image.ContentType);
        }

        private ViewResult PictureView(Image<Rgba32> image, string contentType)
        {
            using var ms = new MemoryStream();
            image.Save(ms, Encoder(contentType));
            return View("Picture", new PictureViewModel { Base64 = Convert.ToBase64String(ms.ToArray()), ContentType = contentType });
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
