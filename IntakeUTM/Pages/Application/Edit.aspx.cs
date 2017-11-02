using Dapper;
using System;
using System.Diagnostics.PerformanceData;
using System.Linq;
using IntakeUTM.Models;

namespace IntakeUTM.Pages.Application
{
    public partial class Edit : System.Web.UI.Page
    {
        private Models.Application application;

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
                application = GetApplication(id);

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

        protected void GenerateLetterBtn_Click(object sender, EventArgs e)
        {
            const string sql = @"
            SELECT * FROM PagesTemplate 
            WHERE ProgrammeId = @ProgrammeId 
            AND AppStatusListId = @AppStatusListId
            ORDER BY SortOrder";

            using (var c = ConnectionFactory.GetConnection())
            {
                var templates = c.Query<Models.PagesTemplate>(sql, new
                {
                    ProgrammeId = application.OfferedProgrammeId,
                    AppStatusListId = 2
                });

                if (!templates.Any()) return;

                // Insert into OfferLetter
                const string insert = @"
                INSERT INTO OfferLetter (IssuedDate, ApplicationId, AppStatusListId) 
                VALUES (@IssuedDate, @ApplicationId, @AppStatusListId);
                SELECT CAST(SCOPE_IDENTITY() as int)";

                var offerLetterId = c.Query<int>(insert, new
                {
                    IssuedDate = DateTime.Now,
                    ApplicationId = application.Id,
                    AppStatusListId = 2
                }).Single();

                foreach (var template in templates)
                {
                    var content = template.ContentText.Replace("{{ApplicantName}}", application.Name);

                    const string programmeSql = @"SELECT * FROM Programme WHERE Id = @OfferedProgrammeId";

                    var programme = c.QuerySingleOrDefault<Models.Programme>(programmeSql, new
                    {
                        application.OfferedProgrammeId
                    });

                    Response.Write(programme.Name);

                    content = content.Replace("{{Programme}}",programme.Code + "- Program saya " + programme.Name);

                    // Insert into Page
                    const string insertPage = @"
                    INSERT INTO Page (OfferLetterId, Content)
                    VALUES (@OfferLetterId, @Content)";

                    c.Execute(insertPage, new
                    {
                        OfferLetterId = offerLetterId,
                        Content = content
                    });
                }

                DisplayOfferLetter(application.Id);
            }
        }

        protected void DisplayOfferLetter(int applicationId)
        {
            const string sql = @"SELECT * FROM OfferLetter WHERE ApplicationId = @ApplicationId";

            using (var c = ConnectionFactory.GetConnection())
            {
                var letters = c.Query<OfferLetter>(sql, new {ApplicationId = applicationId});
                var content = "";

                foreach (var letter in letters)
                {
                    const string pagesSql = @"SELECT * FROM Page WHERE OfferLetterId = @OfferLetterId";

                    var pages = c.Query<Page>(pagesSql, new { OfferLetterId = letter.Id });

                    foreach (var page in pages)
                    {
                        content = content + page.Content;
                    }

                    OfferLetterLiteral.Text = content;
                }
            }
        }
    }
}