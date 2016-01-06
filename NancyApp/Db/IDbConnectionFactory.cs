namespace NancyApp.Db
{
    using System.Data;
    
    public interface IDbConnectionFactory
    {
        IDbConnection CreateSqlConnection();
    }
}
