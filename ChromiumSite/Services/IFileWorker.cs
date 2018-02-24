using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Services
{
    public interface IFileWorker
    {
        Task<string> SaveImgAsync(string path, IFormFile file);
    }
}
