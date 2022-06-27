using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;

namespace IALDashboard.helper
{
    public class CrystalReportPdfResult
    {
        public readonly byte[] _contentBytes;
        public CrystalReportPdfResult(string reportPath, object dataSet)
        {
            string base_path = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                reportDocument.Load(reportPath);

                reportDocument.SetDataSource(dataSet);
                reportDocument.SetParameterValue("base_path", base_path);
                reportDocument.SetParameterValue("img_width", 0);


                _contentBytes = StreamToBytes(reportDocument.ExportToStream(ExportFormatType.PortableDocFormat));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.ApplicationInstance.Response;
            response.Clear();
            response.Buffer = false;
            response.ClearContent();
            response.ClearHeaders();
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.ContentType = "application/pdf";

            using (var stream = new MemoryStream(_contentBytes))
            {
                stream.WriteTo(response.OutputStream);
                stream.Flush();
            }
        }

        private static byte[] StreamToBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}