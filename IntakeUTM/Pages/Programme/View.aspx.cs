using System;
using System.Linq;
using Dapper;

namespace IntakeUTM.Pages.Programme
{
    public partial class ProgrammeDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindProgramme();
        }

        protected void BindProgramme()
        {
            var id = Request.QueryString["Id"];

            if (id == null) return;

            // Dapatkan programme
            const string sql = "SELECT * FROM Programme WHERE Id = @Id";
            using (var c = ConnectionFactory.GetConnection())
            {
                var programme = c.QueryFirst<Models.Programme>(sql, new { Id = id });
                if (programme == null) return;

                ProgrammeName.Text = programme.Name;
                AddButton.NavigateUrl = "~/Pages/PagesTemplate/View.aspx?programmeId=" + programme.Id;
                AddPdfLink.NavigateUrl += "?Id=" + programme.Id;
                BindTemplates(int.Parse(id));
            }
        }

        protected void BindTemplates(int programmeId)
        {
            const string sql = "SELECT * FROM PagesTemplate WHERE ProgrammeId = @Id";
            using (var c = ConnectionFactory.GetConnection())
            {
                var templates = c.Query<Models.PagesTemplate>(sql, new { Id = programmeId });
                if (!templates.Any()) return;

                DataRepeater.DataSource = templates;
                DataRepeater.DataBind();
            }
        }
    }
}