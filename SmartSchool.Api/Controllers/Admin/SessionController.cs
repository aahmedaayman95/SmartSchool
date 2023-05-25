using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Entities;

namespace SmartSchool.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        public ISessionRepo SessionRepo { get; }
        public SessionController(ISessionRepo sessionRepo)
        {
            SessionRepo = sessionRepo;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            try
            {
                var allSessions = SessionRepo.GetAll();
                return Ok(allSessions);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var mySession = SessionRepo.GetById(id);
                if (mySession == null)
                {
                    return NotFound();
                }
                return Ok(mySession);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(SessionVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = SessionRepo.Create(obj);
                    if (data == null)
                    {
                        return BadRequest("Session conflict with another session");
                    }
                    return Ok(data);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                SessionRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("Edit")]
        public ActionResult Edit(SessionVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Session s = SessionRepo.Edit(obj);
                    return Ok(s);
                }
				return BadRequest();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        [HttpGet]
        [Route("Getsessions/classanddate/{classid}/{date}")]
        public IActionResult GetByClassDate(int classid, DateTime date)
        {
            try
            {
                var mySchedule = SessionRepo.GetByClassIdDate(classid, date);
                if (mySchedule == null)
                {
                    return NotFound();
                }
                return Ok(mySchedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
