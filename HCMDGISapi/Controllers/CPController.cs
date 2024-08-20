using HCMDGISapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace HCMDGISapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CPController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public CPController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllCP")]
        public string GetAllCP()
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("GISOnlineConnection").ToString());

            string strsql = "SELECT ID, Name, Description,"
               + " Nearest_Address, Type,"
               + " 'pin_' + Type AS icon, Phase,"
               + " Department, Project_Manager,"
               + " '' AS contactEmail, '' AS contactPhone,"
               + " CapitalProject, Date_Start,  Date_End,"
               + " cast(geom.Long as decimal(10, 8))AS X, cast(geom.Lat as decimal(10, 8))AS Y"
               + " FROM Construction_Projects WHERE Phase = 'Active'"
               + " ORDER BY Name";

            SqlCommand command = new SqlCommand(strsql, conn);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<CP> cpList = new List<CP>();

            Response response = new Response();

            string json = "";

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CP cp = new CP();
                    cp.id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    cp.type = Convert.ToString(dt.Rows[i]["Type"]);
                    cp.title = Convert.ToString(dt.Rows[i]["Name"]);
                    cp.description = Convert.ToString(dt.Rows[i]["Description"]);
                    cp.location = Convert.ToString(dt.Rows[i]["Nearest_Address"]);
                    cp.department = Convert.ToString(dt.Rows[i]["Department"]);
                    cp.status = Convert.ToString(dt.Rows[i]["Phase"]);
                    cp.icon = Convert.ToString(dt.Rows[i]["icon"]);
                    cp.projectManager = Convert.ToString(dt.Rows[i]["Project_Manager"]);
                    cp.contactEmail = Convert.ToString(dt.Rows[i]["contactEmail"]);
                    cp.contactPhone = Convert.ToString(dt.Rows[i]["contactPhone"]);
                    cp.CapitalProject = Convert.ToString(dt.Rows[i]["CapitalProject"]);
                    cp.Date_Start = Convert.ToString(dt.Rows[i]["Date_Start"]);
                    cp.Date_End = Convert.ToString(dt.Rows[i]["Date_End"]);
                    cp.X = Convert.ToString(dt.Rows[i]["X"]);
                    cp.Y = Convert.ToString(dt.Rows[i]["Y"]);
                    cpList.Add(cp);
                }

                if (cpList.Count > 0)
                    json = JsonConvert.SerializeObject(cpList);
                else
                {
                    response.StatusCode = 100;
                    response.ErrorMessage = "No data found";
                    json = JsonConvert.SerializeObject(response);
                }
            }
            return json;
        }

        [HttpGet]
        [Route("GetCPbyID")]
        public string GetCPbyID(int id)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("GISOnlineConnection").ToString());

            string strsql = "SELECT ID, Name, Description,"
               + " Nearest_Address, Type,"
               + " 'pin_' + Type AS icon, Phase,"
               + " Department, Project_Manager,"
               + " '' AS contactEmail, '' AS contactPhone,"
               + " CapitalProject, Date_Start,  Date_End,"
               + " cast(geom.Long as decimal(10, 8))AS X, cast(geom.Lat as decimal(10, 8))AS Y"
               + " FROM Construction_Projects WHERE Phase = 'Active'"
               + " AND ID = @ID ORDER BY Name";

            SqlCommand command = new SqlCommand(strsql, conn);
            command.Parameters.Add("@ID", SqlDbType.Int);
            command.Parameters["@ID"].Value = id;

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<CP> cpList = new List<CP>();

            Response response = new Response();

            string json = "";

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CP cp = new CP();
                    cp.id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    cp.type = Convert.ToString(dt.Rows[i]["Type"]);
                    cp.title = Convert.ToString(dt.Rows[i]["Name"]);
                    cp.description = Convert.ToString(dt.Rows[i]["Description"]);
                    cp.location = Convert.ToString(dt.Rows[i]["Nearest_Address"]);
                    cp.department = Convert.ToString(dt.Rows[i]["Department"]);
                    cp.status = Convert.ToString(dt.Rows[i]["Phase"]);
                    cp.icon = Convert.ToString(dt.Rows[i]["icon"]);
                    cp.projectManager = Convert.ToString(dt.Rows[i]["Project_Manager"]);
                    cp.contactEmail = Convert.ToString(dt.Rows[i]["contactEmail"]);
                    cp.contactPhone = Convert.ToString(dt.Rows[i]["contactPhone"]);
                    cp.CapitalProject = Convert.ToString(dt.Rows[i]["CapitalProject"]);
                    cp.Date_Start = Convert.ToString(dt.Rows[i]["Date_Start"]);
                    cp.Date_End = Convert.ToString(dt.Rows[i]["Date_End"]);
                    cp.X = Convert.ToString(dt.Rows[i]["X"]);
                    cp.Y = Convert.ToString(dt.Rows[i]["Y"]);
                    cpList.Add(cp);
                }

                if (cpList.Count > 0)
                    json = JsonConvert.SerializeObject(cpList);
                else
                {
                    response.StatusCode = 100;
                    response.ErrorMessage = "No data found";
                    json = JsonConvert.SerializeObject(response);
                }
            }
            return json;
        }
    }
}
