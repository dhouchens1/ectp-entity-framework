using System.Data.Entity;
using ECTPEntityFramework.Entities;

namespace ECTPEntityFramework.DataAccess
{
    public class EctpContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}