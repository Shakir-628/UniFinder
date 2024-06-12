using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using UniFinder;

namespace UniFinder.Controllers
{

    public class MobileController : ApiController
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();
        [Route("api/mobile/studentreport")]
        [HttpGet]
        public IHttpActionResult GetReport()
        {

            string connectionString = @"data source =MUHAMMADSHAKIRJ; initial catalog = Uni_Find_DB_310324; integrated security=True";

            // Inline query to retrieve data
            string query = @"Select [Name] as Namee, 'Male' as gender, [type] as Typee from RegisterTbl where [Type] = 'Student' and [status] = 1";

            // Create a DataTable to hold the query results
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    // Execute the query and load the results into the DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            DataTable dt = new DataTable();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
            dt = JsonConvert.DeserializeObject<DataTable>(json);

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "i")
                    {
                        i = i + 1;
                        row.Add(col.ColumnName, i);
                    }
                    else
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                }
                rows.Add(row);
            }
            string json1 = Newtonsoft.Json.JsonConvert.SerializeObject(rows);

            dynamic sss = JsonConvert.DeserializeObject(json1);

            return Ok(sss);

        }


        [Route("api/mobile/universityreport")]
        [HttpGet]
        public IHttpActionResult GetUniversityReport()
        {

            string connectionString = @"data source =MUHAMMADSHAKIRJ; initial catalog = Uni_Find_DB_310324; integrated security=True";

            // Inline query to retrieve data
            string query = @"Select u.UniName, t.TradeName, p.ProgramName,p.Degree from UniversityTbl u inner join Tradetbl t on t.UniID = u.UniID inner join ProgramTbl p on p.TradeID = t.ID Where u.Status = 1";

            // Create a DataTable to hold the query results
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    // Execute the query and load the results into the DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            DataTable dt = new DataTable();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
            dt = JsonConvert.DeserializeObject<DataTable>(json);

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "i")
                    {
                        i = i + 1;
                        row.Add(col.ColumnName, i);
                    }
                    else
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                }
                rows.Add(row);
            }
            string json1 = Newtonsoft.Json.JsonConvert.SerializeObject(rows);

            dynamic sss = JsonConvert.DeserializeObject(json1);

            return Ok(sss);

        }
        // GET api/<controller>/5
        [Route("api/mobile/checklogin/{username}/{password}")]
        [HttpGet]
        public IHttpActionResult CheckLogin(string username, string password)
        {

            string connectionString = @"data source =MUHAMMADSHAKIRJ; initial catalog = Uni_Find_DB_310324; integrated security=True";

            // Inline query to retrieve data
            string query = @"select stdID from RegisterTbl where Name = '" + username + "' and Password = '" + password + "'";

            // Create a DataTable to hold the query results
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    // Execute the query and load the results into the DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            DataTable dt = new DataTable();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
            dt = JsonConvert.DeserializeObject<DataTable>(json);

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "i")
                    {
                        i = i + 1;
                        row.Add(col.ColumnName, i);
                    }
                    else
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                }
                rows.Add(row);
            }
            string json1 = Newtonsoft.Json.JsonConvert.SerializeObject(rows);
            if (json1 == "[]")
            {
                return NotFound();
            }

            return Ok(json1);


        }

        // POST api/<controller>

    }
}