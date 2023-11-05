using AdminDesk.DataAccess;
using AdminDesk.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;

namespace AdminDesk.Repositories
{
    public class DapperIbenRepository : IDIbenRepository
    {
        private readonly ISqlConnector _sqlConnector;

        public DapperIbenRepository(ISqlConnector sqlConnector)
        {
            _sqlConnector = sqlConnector;
        }
        public Iben Get(int id)
        {
            using (var connection = _sqlConnector.GetDbConnection())
            {
                return connection.QueryFirstOrDefault<Iben>("Select id, Name from iben where id like @idParam; ", new { idParam = id });
            }
        }

        public List<Iben> GetAll()
        {
            using (var connection = _sqlConnector.GetDbConnection())
            {
                //    connection.Open();
                return connection.Query<Iben>("Select id, Name from iben; ").ToList();
            }
        }

        public void Upsert(Iben iben)
        {
            var existing = Get(iben.Id);
            using (var connection = _sqlConnector.GetDbConnection())
            {
                if (existing == null)
                {
                    iben.Id = 0;
                    connection.Insert<Iben>(iben);
                    return;
                }
                existing.Name = iben.Name;
                connection.Update<Iben>(existing); //Dapper.Contrib
            }
        }
    }
}
