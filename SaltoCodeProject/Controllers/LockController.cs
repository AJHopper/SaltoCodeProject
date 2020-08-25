using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SaltoCodeProject.Entities;
using SaltoCodeProject.Helpers;
using SaltoCodeProject.Models;
using SaltoCodeProject.Services;

namespace SaltoCodeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LockController : ControllerBase
    {

        private readonly ILockService _lockService;
        private readonly IMapper _mapper;

        public LockController(ILockService lockService, IMapper mapper)
        {
            _lockService = lockService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("unlock/{id}")]
        public IActionResult Unlock(int id)
        {
            User user = (User)HttpContext.Items["User"];

            try
            {
                var result = _lockService.UnlockDoor(id, user.Id);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok();

        }

        [Authorize]
        [HttpPost("lock/{id}")]
        public IActionResult Lock(int id)
        {
            User user = (User)HttpContext.Items["User"];

            try
            {
                var result = _lockService.LockDoor(id, user.Id);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("{id}/adduser/{userid}")]
        public IActionResult AddUser(int id, int userid)
        {
            try
            {
                _lockService.AddUserToLock(id, userid);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult Create([FromBody] LockCreateModel model)
        {
            var lockToCreate = _mapper.Map<Lock>(model);

            try
            {
                _lockService.CreateLock(lockToCreate);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LockCreateModel model)
        {
            var lockToCreate = _mapper.Map<Lock>(model);
            lockToCreate.Id = id;

            try
            {
                _lockService.UpdateLock(lockToCreate);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _lockService.DeleteLock(id);
            return Ok();
        }
    }
}
