using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using HotelSQL;


namespace HotelSQL
{
    class DBClient
    {
     
string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private int GetMaxHotelNo(SqlConnection connection)
        {
            Console.WriteLine("Calling -> GetMaxFacilityNo");

           
            string queryStringMaxFacilityNo = "SELECT  MAX(Facility_No)  FROM dbo.Hotel";
            Console.WriteLine($"SQL applied: {queryStringMaxFacilityNo}");

           
            SqlCommand command = new SqlCommand(queryStringMaxFacilityNo, connection);
            SqlDataReader reader = command.ExecuteReader();

            
            int MaxFacility_No = 0;

            
            if (reader.Read())
            {
                
                MaxFacility_No = reader.GetInt32(0);
            }

         
            reader.Close();

            Console.WriteLine($"Max facility#: {MaxFacility_No}");
            Console.WriteLine();

           
            return MaxFacility_No;
        }

        private int DeleteFacility(SqlConnection connection, int facility_no)
        {
            Console.WriteLine("Calling -> DeleteFacility");

           
            string deleteCommandString = $"DELETE FROM dbo.DemoFacility  WHERE Facility_No = {facility_no}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            
            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"DELETE FROM dbo.DemoFacility  WHERE Facility_No =  {facility_no}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

                       return numberOfRowsAffected;
        }

        private int UpdateFacility(SqlConnection connection, Facility facility)
        {
            Console.WriteLine("Calling -> UpdateFacility");

            
            string updateCommandString = $"UPDATE dbo.DemoFacility SET Name='{facility.Name}', ID='{facility.FacilityId}' WHERE Facility_No = {facility.FacilityNo}";
            Console.WriteLine($"SQL applied: {updateCommandString}");

            
            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Updating facility #{facility.FacilityNo}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

         
            return numberOfRowsAffected;
        }

        private int InsertFacility(SqlConnection connection, Facility facility)
        {
            Console.WriteLine("Calling -> InsertFacility");

            
            string insertCommandString = $"INSERT INTO dbo.DemoFacility VALUES({facility.FacilityNo}, '{facility.Name}', '{facility.FacilityId}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

           
            SqlCommand command = new SqlCommand(insertCommandString, connection);

            Console.WriteLine($"Creating faciltiy #{facility.FacilityNo}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

           
            return numberOfRowsAffected;
        }

        private List<Facility> ListAllFacilities(SqlConnection connection)
        {
            Console.WriteLine("Calling -> ListAllFacilities");

          
            string queryStringAllFacilities = "SELECT * FROM dbo.DemoFacility";
            Console.WriteLine($"SQL applied: {queryStringAllFacilities}");

           
            SqlCommand command = new SqlCommand(queryStringAllFacilities, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Listing all facilities:");

            
            if (!reader.HasRows)
            {
            
                Console.WriteLine("No facilties in database");
                reader.Close();

                
                return null;
            }

            
            List<Facility> facilities = new List<Facility>();
            while (reader.Read())
            {
               
                Facility nextFacility = new Facility()
                {
                    FacilityNo = reader.GetInt32(0), 
                    Name = reader.GetString(1),   

                };

              
                facilities.Add(nextFacility);

                Console.WriteLine(nextFacility);
            }

         
            reader.Close();
            Console.WriteLine();

           
            return facilities;
        }

        private Facility GetFacility(SqlConnection connection, int facility_no)
        {
            Console.WriteLine("Calling -> GetHotel");

           
            string queryStringOneFacility = $"SELECT * FROM dbo.DemoFacility WHERE facility_no = {facility_no}";
            Console.WriteLine($"SELECT * FROM dbo.DemoFacility WHERE facility_no =  {facility_no}");

            SqlCommand command = new SqlCommand(queryStringOneFacility, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Finding facility#: {facility_no}");

            
            if (!reader.HasRows)
            {
                
                Console.WriteLine("No facility in database");
                reader.Close();

              
                return null;
            }

            
            Facility facility = null;
            if (reader.Read())
            {
                facility = new Facility()
                {
                    FacilityNo = reader.GetInt32(0), 
                    Name = reader.GetString(1),    

                };

                Console.WriteLine(facility);
            }

            reader.Close();
            Console.WriteLine();

            return facility;
        }
        public void Start()
        {
          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

               
                List<Facility> facilities = ListAllFacilities(connection);
                foreach (var facility in facilities)
                {
                    Console.WriteLine(facility);
                }

                DeleteFacility(connection, 1);

                Console.WriteLine("List after changes");
                facilities = ListAllFacilities(connection);
                foreach (var facility in facilities)
                {
                    Console.WriteLine(facility);
                }
            }
        }
    }
}
