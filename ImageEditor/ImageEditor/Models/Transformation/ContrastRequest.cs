using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor.Models.Transformation
{
    public class ContrastRequest
    {
        public ImageRequest Image { get; set; }
        public int ContrastLevel { get; set; }
    }
}
