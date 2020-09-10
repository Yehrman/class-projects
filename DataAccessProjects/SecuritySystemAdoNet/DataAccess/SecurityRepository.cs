using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SecurityRepository : DataConnection
    {
        public void AddCredential(SecurityModel model)
        {
            Sqlconn.Open();
            SqlCommand cmd = new SqlCommand("insert  SecurityDevice(SecurityDeviceType)values(@DeviceType);select scope_identity()", Sqlconn);
            cmd.Parameters.AddWithValue("@DeviceType", model.Credential);
            cmd.ExecuteNonQuery();
            Sqlconn.Close();
        }
        public IEnumerable<SecurityModel> GetActivity(DateTime from ,DateTime to)
        {
            Sqlconn.Open();
            SqlCommand cmd = new SqlCommand(@"select ah.AccessHistoryId,ah.AttemptDate,e.EmployeeId, e.FirstName,e.LastName,d.DoorId,d.RoomName,ah.Result from AccessHistory ah join Employee
e on ah.EmployeeId = e.EmployeeId join Door d on ah.DoorId = d.DoorId", Sqlconn);
            cmd.Parameters.AddWithValue("@From", from);
            cmd.Parameters.AddWithValue("@To", to);
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                yield return new SecurityModel
                {
                    AccessHistoryId = reader.GetInt32(0),
                    AccessAttempt = reader.GetDateTime(1),
                    EmployeeId = reader.GetInt32(2),
                    FirstName = reader.GetString(3),
                    LastName = reader.GetString(4),
                    DoorId=reader.GetInt32(5),
                   Door=reader.GetString(6),
                    Result = reader.GetBoolean(7)
                };
            }
            Sqlconn.Close();
        }
        public IEnumerable<SecurityModel> GetDoorActivity(DateTime from, DateTime to, int doorId)
        {
            Sqlconn.Open();
            SqlCommand cmd = new SqlCommand(@"select ah.AccessHistoryId,ah.AttemptDate,e.EmployeeId, e.FirstName,e.LastName,ah.Result from AccessHistory ah join Employee
e on ah.EmployeeId = e.EmployeeId where ah.DoorId=@doorId" , Sqlconn);
            cmd.Parameters.AddWithValue("@From", from);
            cmd.Parameters.AddWithValue("@To", to);
            cmd.Parameters.AddWithValue("@doorId", doorId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new SecurityModel
                {
                    AccessHistoryId = reader.GetInt32(0),
                    AccessAttempt = reader.GetDateTime(1),
                    EmployeeId = reader.GetInt32(2),
                    FirstName = reader.GetString(3),
                    LastName = reader.GetString(4),
                    Result = reader.GetBoolean(5),

                };
            }
            Sqlconn.Close();
        }
    }
}
