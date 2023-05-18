using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersamiAPI.Context;
using PersamiAPI.Models;

namespace PersamiAPI.Controllers
{
    [Route("api/StudentResearch")]
    [ApiController]
    public class TrStudentReserchController : Controller
    {
        private readonly MsStudentContext _MsStudentContext;
        private readonly MsLecturerContext _MsLecturerContext;
        private readonly TrStudentReserchContext _TrStudentReserchContext;

        public TrStudentReserchController(MsStudentContext msStudentContext, MsLecturerContext msLecturerContext, TrStudentReserchContext trStudentResearchContext)
        {
            this._MsStudentContext = msStudentContext;
            this._MsLecturerContext = msLecturerContext;
            this._TrStudentReserchContext = trStudentResearchContext;
        }

        [HttpGet("GetAllStudentResearch")]
        [Authorize]
        public IActionResult GetTrStudentReserch()
        {
            if (_TrStudentReserchContext.TrStudentReserchModels == null)
            {
                return NotFound();
            }
            return Json(_TrStudentReserchContext.TrStudentReserchModels.ToList());
        }

        [HttpGet("GetStudentResearchByID")]
        public IActionResult GetStudentReserch([FromBody] int id)
        {
            if (GetTrStudentResearchById(id) == null)
            {
                return NotFound();
            }
            var StudentReserch = _TrStudentReserchContext.TrStudentReserchModels.FirstOrDefault(x => x.StudentResearchID == id);

            if (StudentReserch == null)
            {
                return NotFound();
            }

            return Json(StudentReserch);
        }

        [HttpPost("AddStudentResearch")]
        public IActionResult PostStudentReserch([FromBody] TrStudentReserchModel StudentReserch)
        {
            if (StudentReserch == null)
            {
                return Problem("Entity set 'MsUserLoginContext.TrStudentReserchModels'  is null.");
            }
            StudentReserch.DateIn = DateTime.Now;
            _TrStudentReserchContext.TrStudentReserchModels.Add(StudentReserch);
             _TrStudentReserchContext.SaveChanges();

            return GetTrStudentReserch();
        }

        [HttpPost("UpdateStudentResearchById")]
        public IActionResult UpdateStudentResearch([FromBody] TrStudentReserchModel StudentReserch)
        {
            if (StudentReserch == null)
            {
                return Problem("Entity set 'MsUserLoginContext.TrStudentReserchModels'  is null.");
            }

            var GetStudentResearch = _TrStudentReserchContext.TrStudentReserchModels.FirstOrDefault(x => x.StudentResearchID == StudentReserch.StudentResearchID);
            if (GetStudentResearch != null) 
            {
                GetStudentResearch.StudentID = StudentReserch.StudentID;
                GetStudentResearch.StudentName = StudentReserch.StudentName;
                GetStudentResearch.LecturerID = StudentReserch.LecturerID;
                GetStudentResearch.LecturerName = StudentReserch.LecturerName;
                GetStudentResearch.StudentResearchTitle = StudentReserch.StudentResearchTitle;
                GetStudentResearch.DateUp = DateTime.Now;
            }

            _TrStudentReserchContext.SaveChanges();

            return GetTrStudentReserch();
        }

        [HttpDelete("DeleteUserLoginByID")]
        public IActionResult DeleteStudentReserch([FromBody] int id)
        {
            if (GetTrStudentResearchById(id) == null)
            {
                return NotFound();
            }
            var StudentReserch =  _TrStudentReserchContext.TrStudentReserchModels.FirstOrDefault(x => x.StudentResearchID == id);
            if (StudentReserch == null)
            {
                return NotFound();
            }

            _TrStudentReserchContext.TrStudentReserchModels.Remove(StudentReserch);
             _TrStudentReserchContext.SaveChangesAsync();

            return NoContent();
        }

        private TrStudentReserchModel GetTrStudentResearchById(int id) 
        {
            return _TrStudentReserchContext.TrStudentReserchModels.FirstOrDefault(x => x.StudentResearchID == id);
        }
    }
}
