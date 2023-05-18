using Microsoft.EntityFrameworkCore;
using PersamiAPI.Models;

namespace PersamiAPI.Context
{
    public class MsStudentContext : DbContext
    {
        public MsStudentContext(DbContextOptions<MsStudentContext> options)
        : base(options)
        {
        }

        public DbSet<MsStudentModel> MsStudentModels { get; set; } = null!;
    }
}
