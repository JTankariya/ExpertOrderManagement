using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Distributor.BusinessLogic
{
    [Serializable]
    public class ReportManager
    {
        public static Font fontTitle17 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 17, Font.BOLD);
        public static Font font10 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD);
        public static Font font8 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8);
        public static Font font9 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9);
        public static Font font9bold = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.BOLD);
        public static string GetFullPDFFileName(string FileNamePart, User user)
        {
            return user.Id + FileNamePart.ToUpper() + DateTime.Now.ToString("ddMMyyyyhhmmss");
        }

        public static void DeleteOldFiles()
        {
            bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PDFReports"]));
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PDFReports"]));
            }

            DirectoryInfo info = new DirectoryInfo(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PDFReports"]));
            FileInfo[] files = info.GetFiles().Where(p => p.CreationTime <= DateTime.Now.AddDays(-1)).ToArray();
            foreach (var file in files)
            {
                file.Delete();
            }
        }

        public static void GetTable(PdfPTable table, int HeaderRows, iTextSharp.text.Document document, float[] widths)
        {
            table.HeaderRows = HeaderRows;
            table.HorizontalAlignment = 0;
            table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            table.LockedWidth = true;
            if (widths != null)
            {
                table.SetWidths(widths);
            }
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        }

        public static string GetDistributorBalanceReport(User user, IEnumerable<User> distributors)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 0f, 50f);
            var filename = GetFullPDFFileName("DistributorBalance", user);
            DeleteOldFiles();

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PdfReportLocation"]) + "/" + filename + ".PDF", FileMode.Create));
            writer.PageEvent = new PDFFooter("Prepared By : " + Convert.ToString(user.FirstName + " " + user.LastName));
            document.Open();
            float[] widths = new float[] { 50f, 60f, 60f, 70f };
            var tableColumns = 4;
            PdfPTable table = new PdfPTable(tableColumns);
            GetTable(table, 0, document, widths);
            //SetupManager.AddTableHeader(table, objReportDetails.ClubName, "Transaction Detail Report", "", "Source : " + objReportDetails.RepSettings, false);

            PdfPCell cell = new PdfPCell(new Phrase("Distributor Name", font10));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Balance", font10));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Total Company", font10));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Total Users", font10));
            table.AddCell(cell);

            foreach (var distributor in distributors)
            {
                cell = new PdfPCell(new Phrase(distributor.FirstName + " " + distributor.LastName, font8));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(distributor.Balance.ToString(), font8));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(distributor.TotalCompany.ToString(), font8));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(distributor.TotalUser.ToString(), font8));
                table.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase("Total", font10));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(distributors.Sum(x => x.Balance).ToString(), font10));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(distributors.Sum(x => x.TotalCompany).ToString(), font10));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(distributors.Sum(x => x.TotalUser).ToString(), font10));
            table.AddCell(cell);

            document.Add(table);
            document.Close();
            return filename;
        }

        public class PDFFooter : PdfPageEventHelper
        {
            private string _PreparedBy = "";
            public PDFFooter(string PreparedBy)
            {
                _PreparedBy = PreparedBy;
            }
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {

            }

            public override void OnStartPage(PdfWriter writer, Document document)
            {

            }
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                //PdfPCell cell = new PdfPCell(new Phrase("Jayesh", fontTitle17));
                //document.Add(cell)
                base.OnEndPage(writer, document);
            }
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {

            }
        }
    }
}
