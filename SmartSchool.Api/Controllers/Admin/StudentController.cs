using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Entities;

namespace SmartSchool.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public IStudentRepo Repo { get; }
        public StudentController(IStudentRepo _repo)
        {
            Repo = _repo;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            try
            {
                var allStudents = Repo.GetAll();
                return Ok(allStudents);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var myStud = Repo.GetbyId(id);
                if (myStud == null)
                {
                    return NotFound();
                }
                return Ok(myStud);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetByClass/{id}")]
        public IActionResult GetStudentByClass(int id)
        {
            try
            {
                var myStud = Repo.GetStudentByClass(id);
                if (myStud == null)
                {
                    return NotFound();
                }
                return Ok(myStud);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetByGradeYear/{id}")]
        public IActionResult GetStudentByGradeYear(int id)
        {
            try
            {
                var myStud = Repo.GetStudentByGradeYear(id);
                if (myStud == null)
                {
                    return NotFound();
                }
                return Ok(myStud);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Edit(StudentVM std)
        {
            Student S = Repo.Edit(std);
            return Ok(S);
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(string id)
        {
            try
            {
                Repo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetAbsenceStudents")]
        public IActionResult GetAbsenceStudents()
        {
            try
            {
                var allStudents = Repo.getAbsenceStudents();
                return Ok(allStudents);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

     
    }
}
