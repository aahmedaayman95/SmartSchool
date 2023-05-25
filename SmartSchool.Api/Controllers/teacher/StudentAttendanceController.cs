using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Repository;
using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Entities;
using SmartSchool.DAL.Migrations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.Api.Controllers.teacher
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAttendanceController : ControllerBase
    {

        public IStudentAttendanceRepo studentAttendance { get; }
        public StudentAttendanceController(IStudentAttendanceRepo StudentAttendanceRepo)
        {
            studentAttendance = StudentAttendanceRepo;

        }


  


        [HttpGet]
        [Route("generateAttendance/{classid}")]
        public IActionResult generate(int classid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    studentAttendance.generateAttendance();
                    var studentsAtt = studentAttendance.getAllAttendance(classid);
                    return Ok(studentsAtt);
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("addStudentAttendance")]
        public IActionResult add(IEnumerable<StudentAttendanceVM> studentAtt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    studentAttendance.addStudentAttendance(studentAtt);
                    return Ok(studentAtt);
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


