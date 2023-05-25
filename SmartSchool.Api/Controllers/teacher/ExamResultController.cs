using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Entities;

namespace SmartSchool.Api.Controllers.teacher
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultController : ControllerBase
    {
        public IExamResultRepo ExamRepo { get; }
        public ExamResultController(IExamResultRepo examRepo)
        {
            ExamRepo = examRepo;

        }


        [HttpGet]
        [Route("generateStudentsGrades/{classid}")]
        public IActionResult generate(int classid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ExamRepo.generateStudentsGrades();
                    var result = ExamRepo.getResultsByClassRoom(classid);
                    return Ok(result);
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("SaveResults")]
        public IActionResult SaveResults(List<ExamResultVM> Result)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ExamRepo.Edit(Result);
                    return Ok();
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("upgradeStudent")]
        public IActionResult upgradeStudent()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ExamRepo.upgradeStudent();
                    return Ok();
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
