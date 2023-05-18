using Microsoft.EntityFrameworkCore;
using PersamiAPI.Models;

namespace PersamiAPI.Context
{
    public class MsUserLoginContext : DbContext
    {
        public MsUserLoginContext(DbContextOptions<MsUserLoginContext> options)
        : base(options)
        {
        }

        public DbSet<MsUserLoginModel> MsUserLoginModels { get; set; } = null!;
    }
}
