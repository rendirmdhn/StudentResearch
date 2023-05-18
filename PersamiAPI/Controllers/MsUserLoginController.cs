using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersamiAPI.Context;
using PersamiAPI.Models;

namespace PersamiAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class MsUserLoginController : ControllerBase
    {
        private readonly MsUserLoginContext _MsUserLogincontext;

        public MsUserLoginController(MsUserLoginContext context)
        {
            _MsUserLogincontext = context;
        }

        // GET: api/MsUserLogin
        [HttpGet("GetAllUserLogin")]
        public async Task<ActionResult<IEnumerable<MsUserLoginModel>>> GetMsUserLoginModels()
        {
          if (_MsUserLogincontext.MsUserLoginModels == null)
          {
              return NotFound();
          }
            return await _MsUserLogincontext.MsUserLoginModels.ToListAsync();
        }

        // GET: api/MsUserLogin/5
        [HttpGet("GetAllUserLoginByID/{id}")]
        public async Task<ActionResult<MsUserLoginModel>> GetMsUserLoginModel(int id)
        {
          if (_MsUserLogincontext.MsUserLoginModels == null)
          {
              return NotFound();
          }
            var msUserLoginModel = await _MsUserLogincontext.MsUserLoginModels.FindAsync(id);

            if (msUserLoginModel == null)
            {
                return NotFound();
            }

            return msUserLoginModel;
        }

        // PUT: api/MsUserLogin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMsUserLoginModel(int id, MsUserLoginModel msUserLoginModel)
        {
            if (id != msUserLoginModel.UserID)
            {
                return BadRequest();
            }

            _MsUserLogincontext.Entry(msUserLoginModel).State = EntityState.Modified;

            try
            {
                await _MsUserLogincontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MsUserLoginModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MsUserLogin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddUserLogin")]
        public async Task<ActionResult<MsUserLoginModel>> PostMsUserLoginModel(MsUserLoginModel msUserLoginModel)
        {
          if (_MsUserLogincontext.MsUserLoginModels == null)
          {
              return Problem("Entity set 'MsUserLoginContext.MsUserLoginModels'  is null.");
          }
            _MsUserLogincontext.MsUserLoginModels.Add(msUserLoginModel);
            await _MsUserLogincontext.SaveChangesAsync();

            return CreatedAtAction("GetMsUserLoginModel", new { id = msUserLoginModel.UserID, name = msUserLoginModel.Name, emailAddress = msUserLoginModel.EmailAddress, phoneNumber = msUserLoginModel.PhoneNumber, pasword = msUserLoginModel.Password, dateIn = msUserLoginModel.DateIn, dateUp = msUserLoginModel.DateUp }, msUserLoginModel);
        }

        // DELETE: api/MsUserLogin/5
        [HttpDelete("DeleteUserLoginByID/{id}")]
        public async Task<IActionResult> DeleteMsUserLoginModel(int id)
        {
            if (_MsUserLogincontext.MsUserLoginModels == null)
            {
                return NotFound();
            }
            var msUserLoginModel = await _MsUserLogincontext.MsUserLoginModels.FindAsync(id);
            if (msUserLoginModel == null)
            {
                return NotFound();
            }

            _MsUserLogincontext.MsUserLoginModels.Remove(msUserLoginModel);
            await _MsUserLogincontext.SaveChangesAsync();

            return NoContent();
        }

        private bool MsUserLoginModelExists(int id)
        {
            return (_MsUserLogincontext.MsUserLoginModels?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
