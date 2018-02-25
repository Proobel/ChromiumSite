using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Services
{
    public class FileWorker : IFileWorker
    {
        IHostingEnvironment _hostingEnvironment;

        public FileWorker(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> SaveImgAsync(string path, IFormFile file)
        {
            string DBpath = path + DateTime.Now.ToString("yyyyMMddHHmmssfff") + file.FileName;
            using (var fileStream = new System.IO.FileStream(_hostingEnvironment.WebRootPath + DBpath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return DBpath;
        }
    }
}
