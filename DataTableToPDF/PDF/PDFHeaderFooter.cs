using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DataTableToPDF.PDF
{
    public class PDFHeaderFooter : PdfPageEventHelper
    {
        private int FontSizeHeader { get; set; }
        private int FontSize { get; set; }
        public string Header { get; set; }

        private DateTime PrintTime;

        public PDFHeaderFooter()
        {
            this.Header = "PDFHeaderFooter";
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                this.FontSizeHeader = 12;
                this.FontSize = 7;
                PrintTime = DateTime.Now;
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            document.SetMargins(10, 10, 8, 10);

            //base.OnEndPage(writer, document);
            PdfPTable tbHeader = new PdfPTable(2);
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            // line one
            Defines(tbHeader, "MEU NOME É EXEMPLO", "Página " + writer.PageNumber.ToString());
            Defines(tbHeader, "Software de Gestão Mundial", "Data: " + DateTime.Now.ToString("dd/MM/yyyy"));
            Defines(tbHeader, "RELATÓRIO DE EXEMPLO", "Página " + "Hora: " + DateTime.Now.ToString("HH:mm"));

            // line
            //Line(tbHeader);
            tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40, writer.DirectContent);
        }

        private void Defines(PdfPTable tbHeader, string strLeft, string strRight)
        {
            // line three
            tbHeader.AddCell(new PdfPCell(new Paragraph(strLeft, iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, this.FontSize)))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
            });
            tbHeader.AddCell(new PdfPCell(new Paragraph(strRight, iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, this.FontSize)))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT
            });
        }

        private void Line(PdfPTable tbHeader)
        {
            tbHeader.AddCell(new PdfPCell(new Paragraph("", iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, this.FontSize)))
            {
                BorderWidthBottom = 0.1f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tbHeader.AddCell(new PdfPCell(new Paragraph("", iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, this.FontSize)))
            {
                BorderWidthBottom = 0.1f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }
}