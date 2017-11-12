using Dapper;
using IntakeUTM.Models;
using System;
using System.IO;
using System.Web.UI.WebControls;

namespace IntakeUTM.Pages.Programme
{
    public partial class AddPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindApplicationStatus();
        }

        /// <summary>
        /// Paparkan senarai status untuk aplikasi.
        /// Setiap surat tawaran terikat dengan status permohonan.
        /// </summary>
        protected void BindApplicationStatus()
        {
            const string sql = "SELECT * FROM AppStatusList ORDER BY SortOrder";
            using (var c = ConnectionFactory.GetConnection())
            {
                AppStatusListId.DataSource = c.Query<AppStatusList>(sql);
                AppStatusListId.DataTextField = "Name";
                AppStatusListId.DataValueField = "Id";
                AppStatusListId.DataBind();
            }
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            // Upload file
            var originalFileNamefileName = Path.GetFileName(PdfUpload.FileName);

            // Get file extension
            if (string.IsNullOrEmpty(originalFileNamefileName)) return;

            var ext = originalFileNamefileName.Substring(originalFileNamefileName.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower();
            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + ext;

            // This can be moved to Web.config
            var uploadPath = Server.MapPath("~/Uploads/");

            // Create directory if it does not exists
            var directoryInfo = new FileInfo(uploadPath).Directory;
            directoryInfo?.Create();

            var uploadLocation = uploadPath + newFileName;

            // Save file
            PdfUpload.SaveAs(uploadLocation);

            const string sql = @"
            INSERT INTO PagesTemplate (Name, Language, AppStatusListId, ProgrammeId, FileLocation, FileType, SortOrder)
            VALUES (@Name, @Language, @AppStatusListId, @ProgrammeId, @FileLocation, @FileType, @SortOrder)";

            using (var c = ConnectionFactory.GetConnection())
            {
                var programmeId = Request.QueryString["Id"];

                // Simpan template
                c.Execute(sql, new
                {
                    Name = TemplateName.Text,
                    Language = Language.SelectedValue,
                    AppStatusListId = int.Parse(AppStatusListId.SelectedValue),
                    ProgrammeId = int.Parse(programmeId),
                    FileLocation = uploadLocation,
                    FileType = ext,
                    SortOrder = int.Parse(SortOrder.Text)
                });
            }
        }
    }
}