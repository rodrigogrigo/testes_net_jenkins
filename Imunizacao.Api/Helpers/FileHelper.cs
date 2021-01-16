using Microsoft.AspNetCore.Http;
using System.IO;

namespace RgCidadao.Api.Helpers
{
    public static class FileHelper
    {
        public static byte[] GetByteArrayFromFile(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }
    }
}
