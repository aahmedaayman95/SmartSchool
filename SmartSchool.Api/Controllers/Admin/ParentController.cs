using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Repository;
using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;

namespace SmartSchool.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        public IParentRepo ParentRepo { get; }
        public ParentController(IParentRepo _repo)
        {
            ParentRepo = _repo;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            try
            {
                var allParents = ParentRepo.GetAll();
                return Ok(allParents);
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
                var myPnt = ParentRepo.GetbyId(id);
                if (myPnt == null)
                {
                    return NotFound();
                }
                return Ok(myPnt);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("Edit")]
        public IActionResult Edit(ParentVM pnt)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Parent P = ParentRepo.Edit(pnt);
                    return Ok(P);
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }


        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(string id)
        {
            try
            {
                ParentRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



    }
}

