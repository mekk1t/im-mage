using ImageEditor.Models.Transformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor.Models.Filters
{
    public class ShadeRequest
    {
        public Shade Shade { get; set; }
        public ImageRequest Image { get; set; }
    }
}
