using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers
{
    [Route("api/images")]
    public sealed class ImagesController : ControllerBase
    {
        [HttpGet("{fileName}", Name = nameof(GetImage))]
        public IActionResult GetImage(
            [FromRoute] string fileName)
        {
            if (System.IO.File.Exists($"images/{fileName}"))
            {
                Byte[] b = System.IO.File.ReadAllBytes($"images/{fileName}");
                return File(b, "image/png");
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UploadImage()
        {
            if (!Directory.Exists("images"))
            {
                Directory.CreateDirectory("images");
            }
            try
            {
                var upload = Request.Form.Files[0];
                var fileName = Guid.NewGuid().ToString() + ".png";
                var path = Path.Combine($"images/{fileName}");
                string errorMsg = string.Empty;
                var stream = new FileStream(path, FileMode.Create);
                await upload.CopyToAsync(stream);
                await stream.FlushAsync();
                stream.Dispose();
                return Ok(fileName);
                //var fileName = Guid.NewGuid().ToString() + ".png";
                //string path = Path.Combine($"images/{fileName}");
                //using (Stream stream = new FileStream(path, FileMode.Create))
                //{
                //    image.CopyTo(stream);
                //}
                //return Ok(fileName);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
