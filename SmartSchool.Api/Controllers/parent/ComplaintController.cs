using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.ViewModel;

namespace SmartSchool.Api.Controllers.parent
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        public IComplaintRepo complaint;

        public ComplaintController(IComplaintRepo comp)
        {
            complaint = comp;
        }

        [HttpPost]
        public IActionResult SendEmail(ComplaintVM request)
        {
            try
            {
                complaint.SendEmail(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
