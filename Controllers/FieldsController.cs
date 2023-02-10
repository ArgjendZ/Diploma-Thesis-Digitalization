using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaThesisDigitalization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldsController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpPost("field")]
        public async Task<IActionResult> CreateField(string fieldName, int departmentId)
        {
            try
            {
                await _fieldService.CreateField(fieldName, departmentId);
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
