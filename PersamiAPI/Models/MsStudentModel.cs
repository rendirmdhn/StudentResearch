using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace PersamiAPI.Models
{
    [Table("MsStudent")]
    public class MsStudentModel
    {
        [Key]
        public int StudentID { get; set; } 
        public string Name { get; set; }
    }
}
