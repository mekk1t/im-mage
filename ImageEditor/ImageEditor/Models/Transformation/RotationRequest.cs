﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor.Models.Transformation
{
    public class RotationRequest
    {
        public IFormFile Image { get; set; }
        public Rotation Rotation { get; set; }
    }
}
