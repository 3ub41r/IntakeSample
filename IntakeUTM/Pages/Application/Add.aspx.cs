using Dapper;
using System;
using System.IO;
using System.Text;

namespace IntakeUTM.Pages.Application
{
    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindProgrammes();
        }

        protected void BindProgrammes()
        {
            using (var c = ConnectionFactory.GetConnection())
            {
                // Dapatkan semua Programme
                const string sql = "SELECT * FROM Programme";
                var programmes = c.Query<Models.Application>(sql);

                // Letakkan senarai Programme dalam DropDownList
                ProgrammeId.DataSource = programmes;
                ProgrammeId.DataTextField = "Name";
                ProgrammeId.DataValueField = "Id";
                ProgrammeId.DataBind();

                // Letakkan senarai Programme dalam DropDownList untuk Programme yang ditawarkan
                OfferedProgrammeId.DataSource = programmes;
                OfferedProgrammeId.DataTextField = "Name";
                OfferedProgrammeId.DataValueField = "Id";
                OfferedProgrammeId.DataBind();
            }
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            var application = new Models.Application
            {
                Name = Name.Text,
                ProgrammeId = int.Parse(ProgrammeId.SelectedValue),
                Email = Email.Text,
                OfferedProgrammeId = int.Parse(OfferedProgrammeId.SelectedValue),
                AppStatusListId = 1
            };

            // Jana surat tawaran
            application.OfferLetterText = GenerateLetter(application);

            const string sql = @"
            INSERT INTO Application (
                Name, 
                ProgrammeId, 
                Email, 
                OfferedProgrammeId, 
                OfferLetterText, 
                AppStatusListId
            ) VALUES (
                @Name, 
                @ProgrammeId, 
                @Email, 
                @OfferedProgrammeId, 
                @OfferLetterText, 
                @AppStatusListId
            );
            SELECT CAST(SCOPE_IDENTITY() AS INT)";

            using (var c = ConnectionFactory.GetConnection())
            {
                // Insert dan dapatkan Application.Id
                var id = c.QuerySingleOrDefault<int>(sql, application);
                Response.Redirect("~/Pages/Application/Edit.aspx?Id=" + id);
            }
        }

        /// <summary>
        /// Jana surat tawaran untuk permohonan.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        protected string GenerateLetter(Models.Application application)
        {
            // Dapatkan semua PagesTemplate berkenaan
            const string sql = @"
            SELECT * FROM PagesTemplate 
            WHERE ProgrammeId = @ProgrammeId 
            AND AppStatusListId = @AppStatusListId
            ORDER BY SortOrder";

            using (var c = ConnectionFactory.GetConnection())
            {
                // Query
                var templates = c.Query<Models.PagesTemplate>(sql, new
                {
                    ProgrammeId = application.OfferedProgrammeId,
                    application.AppStatusListId
                });

                var offeredProgramme = GetProgramme(application.OfferedProgrammeId);
                var sb = new StringBuilder();

                // Pergi setiap template dan masukkan nilai ke dalam template
                foreach (var template in templates)
                {
                    // Jika file PDF dimuat naik atau URL ditetapkan
                    if (template.FileUrl != null || template.FileLocation != null)
                    {
                        var url = template.FileUrl ?? GetGoogleDocsEmbeddedUrl(template.FileLocation);
                        sb.AppendLine(
                            $"<iframe src=\"http://docs.google.com/gview?url={url}&embedded=true\" style=\"width:100%; height:700px;\" frameborder=\"0\"></iframe>");

                        continue;
                    }

                    sb.Append(template.ContentText.Replace("{{ApplicantName}}", application.Name));
                    sb.Append(template.ContentText.Replace("{{Programme}}", offeredProgramme.Name));
                    sb.AppendLine("--- Page ---");
                }

                return sb.ToString();
            }
        }

        public string GetGoogleDocsEmbeddedUrl(string physicalPath)
        {
            // Dapatkan nama file
            var fileName = Path.GetFileName(physicalPath);

            // URL bagi file yang telah dimuat naik
            return Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "Uploads/" + fileName;
        }

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