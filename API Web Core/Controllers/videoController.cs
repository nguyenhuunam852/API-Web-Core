using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web;

namespace API_Web_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class videoController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public videoController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        [Route("getVideo")]
        [HttpGet]
        public  FileResult GetVideoContent()
        {
            var filePath =  _hostingEnvironment.ContentRootPath + "/wwwroot/video/test.mp4";
            return PhysicalFile(filePath, "application/octet-stream", enableRangeProcessing: true);
        }
    }
}
