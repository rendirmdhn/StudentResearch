using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace PersamiAPI.Models
{
    [Table("TrStudentReserch")]
    public class TrStudentReserchModel
    {
        [Key]
        public int? StudentResearchID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int LecturerID { get; set; }
        public string LecturerName { get; set; }
        public string StudentResearchTitle { get; set; } 
        public DateTime? DateIn { get; set; }
        public DateTime? DateUp { get; set; }
    }
}
