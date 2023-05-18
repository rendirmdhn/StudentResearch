using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace PersamiAPI.Models
{
    [Table("MsLecturer")]
    public class MsLecturerModel
    {
        [Key]
        public int LecturerID { get; set; }   
        public int Name { get; set; }
    }
}
