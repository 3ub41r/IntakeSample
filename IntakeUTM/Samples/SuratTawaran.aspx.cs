using System;
using System.Configuration;
using System.Data.SqlClient;

namespace IntakeUTM.Samples
{
    public partial class SuratTawaran : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindApplications();
        }

        /// <summary>
        /// Dapatkan data daripada Application
        /// </summary>
        protected void BindApplications()
        {
            const string sql = "SELECT * FROM Application";

            // Buka sambungan ke database
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ToString();
            using (var c = new SqlConnection(connectionString))
            {
                c.Open();
                using (var cmd = new SqlCommand(sql, c))
                {
                    ApplicationRepeater.DataSource = cmd.ExecuteReader();
                    ApplicationRepeater.DataBind();
                }
            }
        }
    }
}