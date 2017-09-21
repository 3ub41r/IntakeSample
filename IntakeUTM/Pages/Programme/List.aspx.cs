using System;
using Dapper;

namespace IntakeUTM.Pages.Programme
{
    public partial class Programmes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindProgrammes();
        }

        protected void BindProgrammes()
        {
            const string sql = "SELECT * FROM Programme";

            using (var c = ConnectionFactory.GetConnection())
            {
                ProgrammesRepeater.DataSource = c.Query<Models.Programme>(sql);
                ProgrammesRepeater.DataBind();
            }
        }
    }
}