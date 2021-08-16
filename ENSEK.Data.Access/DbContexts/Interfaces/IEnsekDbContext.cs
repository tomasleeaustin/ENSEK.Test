using ENSEK.Data.Access.Entities;
using Microsoft.EntityFrameworkCore;

namespace ENSEK.Data.Access.DbContexts.Interfaces
{
    public interface IEnsekDbContext : IDbContext
    {
        DbSet<Account> Accounts { get; set; }

        DbSet<MeterReading> MeterReadings { get; set; }
    }
}
