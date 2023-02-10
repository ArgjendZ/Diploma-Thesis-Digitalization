using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public AdminController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpPost("field")]
        public async Task<IActionResult> PostComment(string fieldName)
        {
            try
            {
                await _fieldService.CreateField(fieldName);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
