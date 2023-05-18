using Microsoft.EntityFrameworkCore;
using PersamiAPI.Models;

namespace PersamiAPI.Context
{
    public class MsLecturerContext : DbContext
    {
        public MsLecturerContext(DbContextOptions<MsLecturerContext> options)
        : base(options)
        {
        }

        public DbSet<MsLecturerModel> MsLecturerModelModels { get; set; } = null!;
    }
}
