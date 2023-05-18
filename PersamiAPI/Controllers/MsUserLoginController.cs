using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersamiAPI.Context;
using PersamiAPI.Models;

namespace PersamiAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class MsUserLoginController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly MsUserLoginContext _MsUserLogincontext;

        public MsUserLoginController(IConfiguration configuration, MsUserLoginContext context)
        {
            _configuration = configuration;
            _MsUserLogincontext = context;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }

        private string Generate(MsUserLoginModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private MsUserLoginModel Authenticate(UserLogin userLogin)
        {
            var currentUser = _MsUserLogincontext.MsUserLoginModels.FirstOrDefault(x => x.EmailAddress.ToLower() == userLogin.Email && x.Password == userLogin.Password);
            if(currentUser != null) return currentUser;

            return null;
        }

        public MsUserLoginModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null) return null;

            var userClaims = identity.Claims;

            return new MsUserLoginModel 
            {
                Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                EmailAddress = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                PhoneNumber = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value
            };
        }

        //// GET: api/MsUserLogin
        //[HttpGet("GetAllUserLogin")]
        //public async Task<ActionResult<IEnumerable<MsUserLoginModel>>> GetMsUserLoginModels()
        //{
        //  if (_MsUserLogincontext.MsUserLoginModels == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _MsUserLogincontext.MsUserLoginModels.ToListAsync();
        //}

        //// GET: api/MsUserLogin/5
        //[HttpGet("GetAllUserLoginByID/{id}")]
        //public async Task<ActionResult<MsUserLoginModel>> GetMsUserLoginModel(int id)
        //{
        //  if (_MsUserLogincontext.MsUserLoginModels == null)
        //  {
        //      return NotFound();
        //  }
        //    var msUserLoginModel = await _MsUserLogincontext.MsUserLoginModels.FindAsync(id);

        //    if (msUserLoginModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return msUserLoginModel;
        //}

        //// PUT: api/MsUserLogin/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMsUserLoginModel(int id, MsUserLoginModel msUserLoginModel)
        //{
        //    if (id != msUserLoginModel.UserID)
        //    {
        //        return BadRequest();
        //    }

        //    _MsUserLogincontext.Entry(msUserLoginModel).State = EntityState.Modified;

        //    try
        //    {
        //        await _MsUserLogincontext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MsUserLoginModelExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/MsUserLogin
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("AddUserLogin")]
        //public async Task<ActionResult<MsUserLoginModel>> PostMsUserLoginModel(MsUserLoginModel msUserLoginModel)
        //{
        //  if (_MsUserLogincontext.MsUserLoginModels == null)
        //  {
        //      return Problem("Entity set 'MsUserLoginContext.MsUserLoginModels'  is null.");
        //  }
        //    _MsUserLogincontext.MsUserLoginModels.Add(msUserLoginModel);
        //    await _MsUserLogincontext.SaveChangesAsync();

        //    return CreatedAtAction("GetMsUserLoginModel", new { id = msUserLoginModel.UserID, name = msUserLoginModel.Name, emailAddress = msUserLoginModel.EmailAddress, phoneNumber = msUserLoginModel.PhoneNumber, pasword = msUserLoginModel.Password, dateIn = msUserLoginModel.DateIn, dateUp = msUserLoginModel.DateUp }, msUserLoginModel);
        //}

        //// DELETE: api/MsUserLogin/5
        //[HttpDelete("DeleteUserLoginByID/{id}")]
        //public async Task<IActionResult> DeleteMsUserLoginModel(int id)
        //{
        //    if (_MsUserLogincontext.MsUserLoginModels == null)
        //    {
        //        return NotFound();
        //    }
        //    var msUserLoginModel = await _MsUserLogincontext.MsUserLoginModels.FindAsync(id);
        //    if (msUserLoginModel == null)
        //    {
        //        return NotFound();
        //    }

        //    _MsUserLogincontext.MsUserLoginModels.Remove(msUserLoginModel);
        //    await _MsUserLogincontext.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool MsUserLoginModelExists(int id)
        //{
        //    return (_MsUserLogincontext.MsUserLoginModels?.Any(e => e.UserID == id)).GetValueOrDefault();
        //}
    }
}
