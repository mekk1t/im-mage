using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor
{
    public static class Extensions
    {
        public static Image<Rgba32> AsImageSharp(this string base64) =>
            Image.Load(Convert.FromBase64String(base64));
    }
}
