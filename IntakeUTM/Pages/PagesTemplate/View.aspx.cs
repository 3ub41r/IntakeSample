using Dapper;
using IntakeUTM.Models;
using System;
using System.IO;

namespace IntakeUTM.Pages.PagesTemplate
{
    public partial class Template : System.Web.UI.Page
    {
        protected int? ProgrammeId;
        protected int? TemplateId;

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["programmeId"];
            if (id != null) ProgrammeId = int.Parse(id);

            var templateId = Request.QueryString["templateId"];
            if (templateId != null) TemplateId = int.Parse(templateId);
            
            BindProgramme();
            BindApplicationStatus();

            if (Page.IsPostBack) return;
            if (TemplateId != null)
            {
                BindTemplate();
            }
            else
            {
                SetEmptyTemplate();
            }
        }

        protected void BindProgramme()
        {
            const string sql = "SELECT * FROM Programme WHERE Id = @Id";
            using (var c = ConnectionFactory.GetConnection())
            {
                var programme = c.QueryFirstOrDefault<Models.Programme>(sql, new { Id = ProgrammeId });
                if (programme == null) return;

                ProgrammeLiteral.Text = programme.Code + " - " + programme.Name;
                BackLink.NavigateUrl = "~/Pages/Programme/View.aspx?Id=" + programme.Id;
            }
            
        }

        protected void BindTemplate()
        {
            const string sql = "SELECT * FROM PagesTemplate WHERE Id = @Id";
            using (var c = ConnectionFactory.GetConnection())
            {
                var template = c.QueryFirstOrDefault<Models.PagesTemplate>(sql, new { Id = TemplateId });
                if (template == null) return;

                TemplateName.Text = template.Name;
                OfferLetterText.Text = template.ContentText;
                Language.Text = template.Language;
                AppStatusListId.SelectedValue = template.AppStatusListId.ToString();
                SortOrder.Text = template.SortOrder.ToString();
            }
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
            var template = new Models.PagesTemplate()
            {
                Name = TemplateName.Text,
                Language = Language.SelectedValue,
                AppStatusListId = int.Parse(AppStatusListId.SelectedValue),
                ProgrammeId = ProgrammeId,
                ContentText = OfferLetterText.Text,
                SortOrder = int.Parse(SortOrder.Text)
            };

            string sql;
            // Kemaskini
            if (TemplateId != null)
            {
                sql = @"
                UPDATE PagesTemplate
                SET Name = @Name,
                Language = @Language,
                AppStatusListId = @AppStatusListId,
                ProgrammeId = @ProgrammeId,
                ContentText = @ContentText,
                SortOrder = @SortOrder
                WHERE Id = @Id";

                template.Id = (int)TemplateId;
            }
            else
            {
                sql = @"
                INSERT INTO PagesTemplate (Name, Language, AppStatusListId, ProgrammeId, ContentText, SortOrder)
                VALUES (@Name, @Language, @AppStatusListId, @ProgrammeId, @ContentText, @SortOrder)";
            }

            using (var c = ConnectionFactory.GetConnection())
            {
                c.Execute(sql, template);
            }
        }
    }
}