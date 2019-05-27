using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace M2MReports.Web.Viewer.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Report()
        {
            ReportViewer reportViewer = new ReportViewer();

            reportViewer.ProcessingMode = ProcessingMode.Remote;
            //reportViewer.AsyncRendering = true;


            reportViewer.ServerReport.ReportServerUrl = new Uri("http://localhost/ReportServer/");
            reportViewer.ServerReport.ReportServerCredentials = new Credentials();
            reportViewer.ServerReport.ReportPath = "/StandardLayout/RPSO";

            reportViewer.ServerReport.SetParameters(new List<ReportParameter>()
            {
                new ReportParameter("LogID", "3"),
                new ReportParameter("SystemDBConnectionString", "Data Source=APT04-22BV0N2;Initial Catalog=M2M_System;Integrated Security=True;Connect Timeout=120;"),
                new ReportParameter("CompanyDBConnectionString", "Data Source=APT04-22BV0N2;Initial Catalog=M2MDATA01;Integrated Security=True;Connect Timeout=600;"),
                new ReportParameter("AdditionalInfo", "ALL"),
            });


            ViewBag.ReportViewer = reportViewer;
            return View();

        }

       
    }


    public class Credentials : IReportServerCredentials
    {
        public WindowsIdentity ImpersonationUser => null;

        public ICredentials NetworkCredentials => new NetworkCredential("sthomas1", "!Arrow.10", "SWG");

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }
    }
}