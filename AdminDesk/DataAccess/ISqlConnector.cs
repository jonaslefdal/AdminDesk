using AdminDesk.Entities;
using System.Data;

namespace AdminDesk.DataAccess
{
    public interface ISqlConnector
    {
        IDbConnection GetDbConnection();
    }
}