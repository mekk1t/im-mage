using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor.Models.Transformation
{
    public class RotationRequest
    {
        public string ImageBase64 { get; set; }
        public string ContentType { get; set; }
        public Rotation Rotation { get; set; }
    }
}
