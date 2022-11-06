namespace Spring.Infrastructure.Database;
public class SpringDbConnection : ISpringDbConnection
{
    private string _connectionString;
    public SpringDbConnection(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SpringConnectionString");
    }
    public IDbConnection GetDbConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
