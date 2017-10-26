using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;

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
            var id = int.Parse(Request.QueryString["Id"]);
            var application = GetApplication(id);

            Name.Text = application.Name;
            Email.Text = application.Email;
            AppliedProgramme.Text = GetProgramme(application.ProgrammeId).Name;
            OfferedProgramme.Text = GetProgramme(application.OfferedProgrammeId).Name;
        }

        protected Models.Application GetApplication(int id)
        {
            const string sql = "SELECT * FROM Application WHERE Id = @Id";

            using (var c = ConnectionFactory.GetConnection())
            {
                return c.QuerySingleOrDefault<Models.Application>(sql, new { Id = id });
            }
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