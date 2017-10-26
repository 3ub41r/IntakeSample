using Dapper;
using System;

namespace IntakeUTM.Pages.Application
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindApplication();
        }

        protected void BindApplication()
        {
            try
            {
                // Dapatkan Application daripada Id yang dihantar melalui URL
                var id = int.Parse(Request.QueryString["Id"]);
                var application = GetApplication(id);

                Name.Text = application.Name;
                Email.Text = application.Email;
                AppliedProgramme.Text = GetProgramme(application.ProgrammeId).Name;
                OfferedProgramme.Text = GetProgramme(application.OfferedProgrammeId).Name;
                OfferLetterText.Text = application.OfferLetterText;
            }
            catch (Exception)
            {
                // Jika rekod tidak ditemui
                Details.Visible = false;
                NotFound.Visible = true;
            }
        }

        protected Models.Application GetApplication(int id)
        {
            const string sql = "SELECT * FROM Application WHERE Id = @Id";

            using (var c = ConnectionFactory.GetConnection())
            {
                return c.QuerySingleOrDefault<Models.Application>(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Dapatkan maklumat Programme.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected Models.Programme GetProgramme(int? id)
        {
            const string sql = "SELECT * FROM Programme WHERE Id = @Id";

            using (var c = ConnectionFactory.GetConnection())
            {
                return c.QuerySingleOrDefault<Models.Programme>(sql, new { Id = id });
            }
        }
    }
}