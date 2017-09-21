﻿using Dapper;
using IntakeUTM.Models;
using System;
using System.IO;
using System.Linq;

namespace IntakeUTM.Samples
{
    public partial class Template : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindProgramme();
            BindApplicationStatus();
        }

        protected void BindProgramme()
        {
            var id = Request.QueryString["programmeId"];
            if (id == null) return;

            const string sql = "SELECT * FROM Programme WHERE Id = @Id";
            using (var c = ConnectionFactory.GetConnection())
            {
                var programme = c.QueryFirstOrDefault<Programme>(sql, new { Id = id });
                if (programme == null) return;

                ProgrammeLiteral.Text = programme.Code + " - " + programme.Name;
                BackLink.NavigateUrl = "~/Samples/ProgramDetails.aspx?Id=" + programme.Id;
            }
            SetEmptyTemplate();
        }

        protected void SetEmptyTemplate()
        {
            // Dapatkan default template
            var path = Server.MapPath("~/Templates/SuratTawaran.html");
            var template = File.ReadAllText(path);

            OfferLetterText.Text = template;
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
        }
    }
}