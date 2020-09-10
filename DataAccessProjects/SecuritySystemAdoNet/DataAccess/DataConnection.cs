using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
   public abstract  class DataConnection:IDisposable
    {
        protected SqlConnection Sqlconn { get; set; }

        public DataConnection()
        {
            Sqlconn = CreateConnection();
        }
        private SqlConnection CreateConnection()
        {
            var Conn = ConfigurationManager.ConnectionStrings["BuildingSecurityConnection"];
            return new SqlConnection(Conn.ConnectionString);
        }
        public void Dispose()
        {
            Sqlconn.Dispose();
        }
    }
}
