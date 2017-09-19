using System;
using System.Configuration;
using System.Data.SqlClient;
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
        /// Dapatkan sambungan ke DB.
        /// </summary>
        /// <returns></returns>
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ToString());
        }

        /// <summary>
        /// Dapatkan data daripada table Application.
        /// </summary>
        protected void BindApplications()
        {
            const string sql = "SELECT * FROM Application";

            // Buka sambungan ke database
            using (var c = GetConnection())
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
            // Dapatkan 'row' data sahaja
            var itemType = e.Item.ItemType;
            if (!itemType.Equals(ListItemType.Item) && !itemType.Equals(ListItemType.AlternatingItem)) return;

            // Dapatkan id bagi 'row' tersebut
            var id = int.Parse(e.CommandArgument.ToString());
            OfferLetterText.Text = GetOfferLetterText(id);
            HiddenId.Value = id.ToString();
        }

        /// <summary>
        /// Dapatkan kandungan surat tawaran.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetOfferLetterText(int id)
        {
            var offerLetterText = string.Empty;

            const string sql = @"SELECT OfferLetterText FROM Application WHERE Id = @Id";
            using (var c = GetConnection())
            {
                c.Open();
                using (var cmd = new SqlCommand(sql, c))
                {
                    cmd.Parameters.AddWithValue("Id", id);
                    using (var result = cmd.ExecuteReader())
                    {
                        if (!result.HasRows) return offerLetterText;
                        result.Read();
                        offerLetterText = result["OfferLetterText"].ToString();
                    }
                }
            }
            return offerLetterText;
        }

        /// <summary>
        /// Kemaskini kadungan surat tawaran.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        protected void UpdateOfferLetterText(int id, string text)
        {
            const string sql = @"
            UPDATE Application
            SET OfferLetterText = @OfferLetterText
            WHERE Id = @Id";

            using (var c = GetConnection())
            {
                c.Open();
                using (var cmd = new SqlCommand(sql, c))
                {
                    cmd.Parameters.AddWithValue("OfferLetterText", text);
                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void UpdateButton_OnClick(object sender, EventArgs e)
        {
            var id = int.Parse(HiddenId.Value);
            UpdateOfferLetterText(id, OfferLetterText.Text);
        }
    }
}