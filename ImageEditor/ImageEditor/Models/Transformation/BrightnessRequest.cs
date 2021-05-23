using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor.Models.Transformation
{
    public class BrightnessRequest
    {
        public ImageRequest Image { get; set; }
        public int BrightnessLevel { get; set; }
    }
}
