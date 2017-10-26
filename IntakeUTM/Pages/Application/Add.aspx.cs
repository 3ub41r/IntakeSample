using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;

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
                const string sql = "SELECT * FROM Programme";
                var programmes = c.Query<Models.Application>(sql);

                ProgrammeId.DataSource = programmes;
                ProgrammeId.DataTextField = "Name";
                ProgrammeId.DataValueField = "Id";
                ProgrammeId.DataBind();

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
                OfferedProgrammeId = int.Parse(OfferedProgrammeId.SelectedValue)
            };

            const string sql = @"
            INSERT INTO Application (Name, ProgrammeId, Email, OfferedProgrammeId)
            VALUES (@Name, @ProgrammeId, @Email, @OfferedProgrammeId);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";

            using (var c = ConnectionFactory.GetConnection())
            {
                var id = c.QuerySingleOrDefault<int>(sql, application);
                Response.Redirect("~/Pages/Application/Edit.aspx?Id=" + id);
            }
        }
    }
}