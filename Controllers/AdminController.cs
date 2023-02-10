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
        public async Task<IActionResult> CreateField(string fieldName)
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

        [HttpGet("fields")]
        public async Task<IActionResult> GetAllFields()
        {
            try
            {
                var fields = await _fieldService.GetAllFields();
                return Ok(fields);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpDelete("field")]
        public async Task<IActionResult> DeleteField(string fieldName)
        {
            try
            {
                await _fieldService.DeleteField(fieldName);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
