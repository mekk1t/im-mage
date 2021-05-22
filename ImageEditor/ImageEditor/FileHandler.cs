using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor
{
    public class FileHandler
    {
        public string ReadAsString(string filePath)
        {
            var content = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(content);
        }
    }
}
