using Dapper;
using IntakeUTM.Models;
using System;
using System.IO;
using Microsoft.Ajax.Utilities;

namespace IntakeUTM.Pages.PagesTemplate
{
    public partial class Template : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            // Jika butang 'Save' tidak ditekan
            BindProgramme();
            BindApplicationStatus();
        }

        /// <summary>
        /// Paparkan maklumat program pada page.
        /// </summary>
        protected void BindProgramme()
        {
            Models.Programme programme = null;

            var programmeId = Request.QueryString["programmeId"];
            if (programmeId != null)
            {
                // Dapatkan maklumat program
                programme = GetProgramme(int.Parse(programmeId));
                ProgrammeIdHidden.Value = programmeId;
            }

            // Dapatkan Id bagi template
            var templateId = Request.QueryString["templateId"];
            if (templateId == null)
            {
                // Jika tiada template yang ditentukan sediakan template kosong
                SetEmptyTemplate();
                return;
            }

            var template = GetTemplate(int.Parse(templateId));
            BindTemplate(template);

            if (template.ProgrammeId != null)
                programme = GetProgramme((int) template.ProgrammeId);

            if (programme == null) return;

            ProgrammeLiteral.Text = programme.Code + " - " + programme.Name;
            BackLink.NavigateUrl = "~/Pages/Programme/View.aspx?Id=" + programme.Id;
        }

        /// <summary>
        /// Dapatkan maklumat template.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected Models.PagesTemplate GetTemplate(int id)
        {
            const string sql = "SELECT * FROM PagesTemplate WHERE Id = @Id";
            using (var c = ConnectionFactory.GetConnection())
            {
                return c.QueryFirstOrDefault<Models.PagesTemplate>(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Dapatkan maklumat program.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected Models.Programme GetProgramme(int id)
        {
            const string sql = "SELECT * FROM Programme WHERE Id = @Id";
            using (var c = ConnectionFactory.GetConnection())
            {
                return c.QueryFirstOrDefault<Models.Programme>(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Paparkan maklumat template page page.
        /// </summary>
        /// <param name="template"></param>
        protected void BindTemplate(Models.PagesTemplate template)
        {
            TemplateName.Text = template.Name;
            OfferLetterText.Text = template.ContentText;
            Language.Text = template.Language;
            AppStatusListId.SelectedValue = template.AppStatusListId.ToString();
            SortOrder.Text = template.SortOrder.ToString();

            ProgrammeIdHidden.Value = template.ProgrammeId.ToString();
            TemplateIdHidden.Value = template.Id.ToString();
        }

        /// <summary>
        /// Tetapkan template kosong.
        /// </summary>
        protected void SetEmptyTemplate()
        {
            // Dapatkan default template
            var path = Server.MapPath("~/Templates/SuratTawaran.html");
            var template = File.ReadAllText(path);

            OfferLetterText.Text = template.Trim();
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

        /// <summary>
        /// Butang 'Save' telah ditekan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_OnClick(object sender, EventArgs e)
        {
            // Declare template untuk disimpan ke DB
            var template = new Models.PagesTemplate()
            {
                Name = TemplateName.Text,
                Language = Language.SelectedValue,
                AppStatusListId = int.Parse(AppStatusListId.SelectedValue),
                ProgrammeId = int.Parse(ProgrammeIdHidden.Value),
                ContentText = OfferLetterText.Text,
                SortOrder = int.Parse(SortOrder.Text)
            };

            string sql;

            // Kemaskini template sedia ada
            if (TemplateIdHidden.Value != "")
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

                template.Id = int.Parse(TemplateIdHidden.Value);
            }
            else
            {
                // Template baru
                sql = @"
                INSERT INTO PagesTemplate (Name, Language, AppStatusListId, ProgrammeId, ContentText, SortOrder)
                VALUES (@Name, @Language, @AppStatusListId, @ProgrammeId, @ContentText, @SortOrder)";
            }

            using (var c = ConnectionFactory.GetConnection())
            {
                c.Execute(sql, template);
            }

            Response.Redirect("~/Pages/Programme/View.aspx?Id=" + template.ProgrammeId);
        }
    }
}