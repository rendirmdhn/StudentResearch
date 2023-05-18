using Microsoft.EntityFrameworkCore;
using PersamiAPI.Models;

namespace PersamiAPI.Context
{
    public class TrStudentReserchContext : DbContext
    {
        public TrStudentReserchContext(DbContextOptions<TrStudentReserchContext> options)
        : base(options)
        {
        }

        public DbSet<TrStudentReserchModel> TrStudentReserchModels { get; set; } = null!;
    }
}
