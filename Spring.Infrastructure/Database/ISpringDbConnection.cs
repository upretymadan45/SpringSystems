using System.Data;

namespace Spring.Infrastructure.Database;
public interface ISpringDbConnection
{
    IDbConnection GetDbConnection();
}
