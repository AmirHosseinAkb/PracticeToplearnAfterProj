using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Security
{
    public static class ImageValidator
    {
        public static bool IsImage(this IFormFile image)
        {
            if(image == null)
            {
                return true;
            }
            try
            {
                var file = System.Drawing.Image.FromStream(image.OpenReadStream());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
