using GymManager.AplicationServices.Members;
using GymManager.Core.Attendance;
using GymManager.Core.Members;
using GymManager.DataAccess.Reports;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace GymManager.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IGeneratePdf _generatePdf;
        private readonly IWebHostEnvironment _environment;
        private readonly IMembersAppService _membersAppService;

        public ReportsController(IMembersAppService membersAppService, 
            IWebHostEnvironment environment, IGeneratePdf generatePdf)
        {
            _membersAppService = membersAppService;
            _generatePdf = generatePdf;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Attendance()
        {
            string path = System.IO.Path.Combine(_environment.ContentRootPath, "Reports\\Attendance.rdlc");
            Stream reportDefinition = System.IO.File.OpenRead(path);

            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;
            report.LoadReportDefinition(reportDefinition);


            MembersDataSet dataSet = new MembersDataSet();

            List<Member> members = new List<Member>();
            members = await _membersAppService.GetMembersAsync(20);

            for (int i = 0; i < members.Count; i++)
            {
                var row = dataSet.Member.NewMemberRow();
                row.Name = $"{members[i].Name} {members[i].LastName}";
                row.RegisteredOn = members[i].DateRegistration;
                row.Membershiptype = members[i].MembershipType.Name;
                dataSet.Member.Rows.Add(row);
            }

            WeekDataSet weekDataSet = new WeekDataSet();
            List<DateTime> days = GetWeek();
            List<int> attendance = GetAttendance(days, members);

            var dayOfWeek = GetDaysOfWeek(days);

            for (int i = 0; i < dayOfWeek.Count; i++)
            {
                var row = weekDataSet.Week.NewWeekRow();
                row.Day = dayOfWeek.ElementAt(i);
                row.Count = attendance.ElementAt(i);
                weekDataSet.Week.Rows.Add(row);
            }


            byte[] streamBytes = null;
            string mimetype = "";
            string encoding = "";
            string filenameExtension = "";
            string reportName = "Attendance";
            string[] streamids = null;
            Warning[] warnings = null;
            
            report.SetParameters(new ReportParameter[] {
                new ReportParameter("DateFrom", DateTime.Today.AddMonths(-1).ToString()),
                new ReportParameter("DateTo", DateTime.Today.ToString())}
            );

            report.DataSources.Add(new ReportDataSource("Members", (System.Data.DataTable)dataSet.Member));
            report.DataSources.Add(new ReportDataSource("Week", (System.Data.DataTable)weekDataSet.Week));

            streamBytes = report.Render("PDF", null, out mimetype, out encoding, out filenameExtension, out streamids, out warnings);

            return File(streamBytes, mimetype, $"{reportName}.{filenameExtension}");
        }

        List<DateTime> GetWeek()
        {
            List<DateTime> days = new List<DateTime>();

            for (int i = 0; i < 7; i++)
            {
                days.Add(DateTime.Today.AddDays(-i));
            }

            return days;
        }

        List<string> GetDaysOfWeek(List<DateTime> week)
        {
            List<string> daysOfWeek = new List<string>();
            foreach (DateTime day in week)
            {
                daysOfWeek.Add(day.DayOfWeek.ToString());
            }

            return daysOfWeek;
        }

        List<int> GetAttendance(List<DateTime> week, List<Member> members)
        {
            List<int> attendance = new List<int>();

            int a = 0;

            List<Check> checks = new List<Check>();

            foreach (Member member in members)
            {
                foreach (Check check in member.Checks)
                {
                    checks.Add(check);
                }
            }

            foreach (DateTime day in week)
            {
                foreach (Check check in checks)
                {
                    if (check.DateCheck.Date == day.Date && check.CheckType.Equals("CheckIn"))
                    {
                        a++;
                    }
                }
                attendance.Add(a);
                a = 0;
            }

            return attendance;

        }

        public IActionResult NewMembers()
        {
            string path = System.IO.Path.Combine(_environment.ContentRootPath, "Reports\\NewMembers.rdlc");
            Stream reportDefinition = System.IO.File.OpenRead(path);

            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;
            report.LoadReportDefinition(reportDefinition);

            MembersDataSet dataSet = new MembersDataSet();
            Random random = new Random();

            string[] membershipTypes = new string[] { "Basic", "Family", "Gold" };

            for (int i = 0; i < 28; i++)
            {
                var row = dataSet.Member.NewMemberRow();
                row.Name = $"Member Pérez {i}";
                int day = random.Next(1, 10) * -1;
                int membershipIndex = random.Next(0, 2);

                row.RegisteredOn = DateTime.Today.AddDays(day);
                row.Membershiptype = membershipTypes[membershipIndex];

                dataSet.Member.Rows.Add(row);
            }

            byte[] streamBytes = null;
            string mimetype = "";
            string encoding = "";
            string filenameExtension = "";
            string reportName = "NewMembers";
            string[] streamids = null;
            Warning[] warnings = null;

            report.SetParameters(new ReportParameter[] {
                new ReportParameter("DateFrom", DateTime.Today.AddDays(-10).ToString()),
                new ReportParameter("DateTo", DateTime.Today.ToString())}
            );

            report.DataSources.Add(new ReportDataSource("Members", (System.Data.DataTable)dataSet.Member));

            streamBytes = report.Render("PDF", null, out mimetype, out encoding, out filenameExtension, out streamids, out warnings);

            return File(streamBytes, mimetype, $"{reportName}.{filenameExtension}");
        }


    }
}
