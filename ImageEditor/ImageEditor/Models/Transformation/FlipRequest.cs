using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor.Models.Transformation
{
    public class FlipRequest
    {
        public ImageRequest Image { get; set; }
        public Flip Flip { get; set; }
    }
}
