using System;
using System.IO;
using Dapper;
using IntakeUTM.Models;

namespace IntakeUTM.Pages.Template
{
    public partial class Template : System.Web.UI.Page
    {
        protected int Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindProgramme();
            BindApplicationStatus();
        }

        protected void BindProgramme()
        {
            var id = Request.QueryString["programmeId"];
            if (id == null) return;

            Id = int.Parse(id);

            const string sql = "SELECT * FROM Programme WHERE Id = @Id";
            using (var c = ConnectionFactory.GetConnection())
            {
                var programme = c.QueryFirstOrDefault<Models.Programme>(sql, new { Id = id });
                if (programme == null) return;

                ProgrammeLiteral.Text = programme.Code + " - " + programme.Name;
                BackLink.NavigateUrl = "~/Samples/ProgrammeDetails.aspx?Id=" + programme.Id;
            }
            SetEmptyTemplate();
        }

        protected void SetEmptyTemplate()
        {
            // Dapatkan default template
            var path = Server.MapPath("~/Templates/SuratTawaran.html");
            var template = File.ReadAllText(path);

            OfferLetterText.Text = template.Trim();
        }

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

        protected void SaveButton_OnClick(object sender, EventArgs e)
        {
            const string sql = @"
            INSERT INTO PagesTemplate (Name, Language, AppStatusListId, ProgrammeId, ContentText, SortOrder)
            VALUES (@Name, @Language, @AppStatusListId, @ProgrammeId, @ContentText, @SortOrder)";
            using (var c = ConnectionFactory.GetConnection())
            {
                c.Execute(sql, new PagesTemplate()
                {
                    Name = TemplateName.Text,
                    Language = Language.SelectedValue,
                    AppStatusListId = int.Parse(AppStatusListId.SelectedValue),
                    ProgrammeId = Id,
                    ContentText = OfferLetterText.Text,
                    SortOrder = int.Parse(SortOrder.Text)
                });
            }
        }
    }
}