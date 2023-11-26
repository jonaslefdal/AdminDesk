using AdminDesk.Entities;//Importerer entitetsklasser fra AdminDesk
using MySqlConnector;//Gjør det mulig å tilkoble til MySQL-databaser
using System.Data;//Importerer klasser for håndtering av databaser
using System.Data.Common;//Importerer for å forenkle databasetilkobling

namespace AdminDesk.DataAccess//Ærklerer navneområde for klasser som samhandler med databasen
{
    public class SqlConnector : ISqlConnector// SqlConnectorklasse som impleneter interface av ISqlConnector.
    {
        private readonly IConfiguration config;//Privat felt for å holde konfigurering 

        public SqlConnector(IConfiguration config)//Initialiserer SqlConnector med konfigurasjon som gjør det mulig å koble til databasen
        {
            this.config = config;
        }

        //Oppretter og returnerer en ny MySqlConnection
        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(config.GetConnectionString("MariaDb"));
        }

    }
}