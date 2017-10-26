﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    sb.Append(template.ContentText.Replace("{{ApplicantName}}", application.Name));
                    sb.Append(template.ContentText.Replace("{{Programme}}", offeredProgramme.Name));
                    sb.AppendLine("--- Page ---");
                }

                return sb.ToString();
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