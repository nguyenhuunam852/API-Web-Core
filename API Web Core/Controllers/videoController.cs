using API_Web_Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using System.Web;

namespace API_Web_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class videoController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IBackGroundRepo _iBackGroundRepo;

        public videoController(
            IWebHostEnvironment hostingEnvironment,
            IBackGroundRepo iBackGroundRepo
            )
        {
            _hostingEnvironment = hostingEnvironment;
            _iBackGroundRepo = iBackGroundRepo;
        }

        [AllowAnonymous]
        [Route("getVideo")]
        [HttpGet]
        public  FileResult GetVideoContent()
        {
            var filePath =  _hostingEnvironment.ContentRootPath + "/wwwroot/video/test.mp4";
            return PhysicalFile(filePath, "application/octet-stream", enableRangeProcessing: true);
        }


        [AllowAnonymous]
        [Route("testRedis")]
        [HttpGet]
        public int? testRedis()
        {
            return this._iBackGroundRepo.getCurrentValue();
        }

    }
}
