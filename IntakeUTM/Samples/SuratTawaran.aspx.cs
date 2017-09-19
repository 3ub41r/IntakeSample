using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        protected void ApplicationRepeater_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var itemType = e.Item.ItemType;
            if (!itemType.Equals(ListItemType.Item) && !itemType.Equals(ListItemType.AlternatingItem)) return;

            var id = e.CommandArgument.ToString();
            OfferLetterText.Text = id;
        }
    }
}