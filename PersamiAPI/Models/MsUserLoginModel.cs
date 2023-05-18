using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace PersamiAPI.Models
{
    [Table("MsUserLogin")]
    public class MsUserLoginModel
    {
        [Key]
        public int UserID { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateUp { get; set; }
    }
}
