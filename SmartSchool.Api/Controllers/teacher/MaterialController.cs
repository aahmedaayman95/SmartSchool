using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Helpers;
using SmartSchool.BL.Interface;
using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Context;

namespace SmartSchool.Api.Controllers.teacher
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {

        public IMaterialRepo MaterialRepo { get; }

        public MaterialsController(IMaterialRepo materialRepo)
        {
      
            MaterialRepo = materialRepo;
        }


        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<IActionResult> UploadMaterial()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                if (formCollection.Files.First().Length > 0)
                {

                    MaterialRepo.Material(formCollection);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var materials= MaterialRepo.getAll();
                return Ok(materials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("getbysubject/{subjectid}/{type}")]
        public async Task<IActionResult> GetBySubject(int subjectid, string type)
        {
            try
            {

                var Material = MaterialRepo.getBySubject(subjectid, type);
                return Ok(Material);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}