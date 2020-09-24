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
        public IEnumerable<SecurityModel> GetActivity(DateTime from, DateTime to)
        {
            Sqlconn.Open();
            SqlCommand cmd = new SqlCommand(@"select ah.AccessHistoryId,ah.AttemptDate,e.EmployeeId, e.FirstName,e.LastName,d.DoorId,d.RoomName,ah.Result from AccessHistory ah join Employee
e on ah.EmployeeId = e.EmployeeId join Door d on ah.DoorId = d.DoorId", Sqlconn);
            cmd.Parameters.AddWithValue("@From", from);
            cmd.Parameters.AddWithValue("@To", to);
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
                    DoorId = reader.GetInt32(5),
                    Door = reader.GetString(6),
                    Result = reader.GetBoolean(7)
                };
            }
            Sqlconn.Close();
        }
        public IEnumerable<SecurityModel> GetDoorActivity(DateTime from, DateTime to, int doorId)
        {
            Sqlconn.Open();
            SqlCommand cmd = new SqlCommand(@"select ah.AccessHistoryId,ah.AttemptDate,e.EmployeeId, e.FirstName,e.LastName,ah.Result from AccessHistory ah join Employee
e on ah.EmployeeId = e.EmployeeId where ah.DoorId=@doorId", Sqlconn);
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
        //private IEnumerable<SecurityModel> GetUnseccessfulAttempts(DateTime from, DateTime to)
        //{
        //    SecurityModel model = new SecurityModel();
        //    Sqlconn.Open();
        //    SqlCommand cmd = new SqlCommand("select * from AccessHistory ah where ah.AttemptDate >=from and ah.AttemptDate <= to and ah.Result=0",Sqlconn);
        //    cmd.Parameters.AddWithValue("@from", model.AccessAttempt);
        //    cmd.Parameters.AddWithValue("@to", model.AccessAttempt);
        //    SqlDataReader reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        yield return new SecurityModel
        //        {
        //            AccessHistoryId = reader.GetInt32(0),
        //            AccessAttempt = reader.GetDateTime(1),
        //            Result = reader.GetBoolean(2)
        //        };
        //    }
        //}
        //private IEnumerable<SecurityModel> GetSuccessfulAttempts2(DateTime from, DateTime to)
        //{
        //    SecurityModel model = new SecurityModel();
        //    Sqlconn.Open();
        //    SqlCommand cmd = new SqlCommand("select * from AccessHistory ah where ah.AttemptDate >=from and ah.AttemptDate <= to and ah.Result=1",Sqlconn);
        //    cmd.Parameters.AddWithValue("@from", model.AccessAttempt);
        //    cmd.Parameters.AddWithValue("@to", model.AccessAttempt);
        //    SqlDataReader reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        yield return new SecurityModel
        //        {
        //            AccessHistoryId = reader.GetInt32(0),
        //            AccessAttempt = reader.GetDateTime(1),
        //            Result = reader.GetBoolean(2)
        //        };
        //    }
        //}
        //public IEnumerable<SecurityModel> GetSuspiciousAttempts(DateTime from, DateTime to)
        //{
        //    SecurityModel model = new SecurityModel();

        //    var Query1 = GetUnseccessfulAttempts(from, to);
        //    var Query2 = GetSuccessfulAttempts2(from, to);
        //    foreach (var failure in Query1)
        //    {

        //        foreach (var success in Query2)
        //        {
        //            if(failure.EmployeeId==success.EmployeeId&&failure.DoorId==success.DoorId&&failure.AccessAttempt<success.AccessAttempt.AddMinutes(2))
        //            {
        //                yield return new SecurityModel
        //                {
        //                    AccessHistoryId = failure.AccessHistoryId,
        //                    AccessAttempt = failure.AccessAttempt,
        //                    Door = failure.Door,
        //                    FirstName = failure.FirstName,
        //                    LastName = failure.LastName,
        //                    Result = failure.Result
        //                };
        //            }
        //        }

        //  }
        //Continue this we need to compare the 2 queries and see if the 2nd 1 has a true less then 2 minutes after a false on the 1st list and exclude it from the result
        public IEnumerable<SecurityModel>GetSuspiciousAttempts(DateTime from, DateTime to)
        {
            SecurityModel model = new SecurityModel();
            Sqlconn.Open();
            SqlCommand cmd = new SqlCommand("select * from AccessHistory ah where ah.AttemptDate >=from and ah.AttemptDate <= to and ah.Result=0", Sqlconn);
            cmd.Parameters.AddWithValue("@from", model.AccessAttempt);
            cmd.Parameters.AddWithValue("@to", model.AccessAttempt);
            SqlDataReader reader = cmd.ExecuteReader();
           while(reader.Read())
            {
                model.AccessHistoryId = reader.GetInt32(0);
            var Attempt= model.AccessAttempt= reader.GetDateTime(1);
               var EmpId1= model.EmployeeId = reader.GetInt32(2);
                model.FirstName = reader.GetString(3);
                model.LastName = reader.GetString(4);
                var door1 = model.DoorId = reader.GetInt32(5);
                model.Result = reader.GetBoolean(6);
                var Plus2minutes = Attempt.AddMinutes(2);
                SqlCommand cmnd = new SqlCommand("select * from AccessHistory ah where ah.AttemptDate >=from and ah.AttemptDate <= to", Sqlconn);
                SqlDataReader rdr = cmnd.ExecuteReader();
                while(rdr.Read())
                {
                   model.AccessHistoryId = reader.GetInt32(0);
                    var Attempt2= model.AccessAttempt = reader.GetDateTime(1);
                   var EmpId2= model.EmployeeId = reader.GetInt32(2);
                    model.FirstName = reader.GetString(3);
                    model.LastName = reader.GetString(4);
                    var door2 = model.DoorId = reader.GetInt32(5);
                   var Result= model.Result = reader.GetBoolean(6);
                   //We also need if there is no result 
                   if(door1==door2&&EmpId1==EmpId2&& Attempt2>Plus2minutes||Attempt2<Plus2minutes&& Result==false)
                    {

                    }
                }
            }
        }
    }
}


