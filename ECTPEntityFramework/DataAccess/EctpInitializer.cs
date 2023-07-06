using System.Data.Entity;

namespace ECTPEntityFramework.DataAccess
{
    public class EctpInitializer : CreateDatabaseIfNotExists<EctpContext>
    {
    }
}