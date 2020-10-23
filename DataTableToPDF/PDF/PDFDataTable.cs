using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DataTableToPDF.PDF
{
    public class PDFDataTable : IDisposable
    {
        private List<Field> Fields { get; set; }
        private string filePdf = Path.Combine(Environment.CurrentDirectory, "CreatePDF.pdf");

        /// <summary>
        /// PDFDataTable
        /// </summary>
        public PDFDataTable()
        {
        }

        /// <summary>
        /// Create pdf
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="columns"></param>
        public void CreatePdf(DataTable dataTable, ObservableCollection<DataGridColumn> columns)
        {
            this.LoadFields(columns);

            //using (Document document = new Document())
            //using (Document document = new Document(iTextSharp.text.PageSize.A4, 10f, 10f, 100f, 40f))
            using (Document document = new Document(PageSize.A4, 10f, 10f, 50f, 10f))
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePdf, FileMode.Create));
                writer.PageEvent = new PDFHeaderFooter();
                //writer.PageEvent = new ITextEvents();
                document.Open();

                PdfPTable table = new PdfPTable(Fields.Count);
                table.WidthPercentage = 100;

                //this.DoAddMetaData(document);
                this.AddColumns(table);
                this.AddRows(dataTable, table);
                this.DefineWidths(table);

                document.Add(table);
            }
        }

        /// <summary>
        /// Open Pdf
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="columns"></param>
        public void OpenPdf(DataTable dataTable, ObservableCollection<DataGridColumn> columns)
        {
            this.CreatePdf(dataTable, columns);
            System.Diagnostics.Process.Start(filePdf);
        }

        /// <summary>
        /// Add rows to pdf
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="dataTable"></param>
        /// <param name="table"></param>
        private void AddRows(DataTable dataTable, PdfPTable table)
        {
            //Add values of DataTable in pdf file
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (Field column in this.Fields)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(row[column.Title].ToString(), this.GetFont()));

                    //Align the cell in the center
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;// PdfPCell.ALIGN_CENTER;
                    SetCellBorder(cell);
                    table.AddCell(cell);
                }
            }
        }

        /// <summary>
        /// Add columns in PdfPTable
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="table"></param>
        private void AddColumns(PdfPTable table)
        {
            //Set columns names in the pdf file
            foreach (Field c in this.Fields)
            {
                PdfPCell cell = new PdfPCell(new Phrase(c.Title, this.GetFont()));

                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                //cell.BackgroundColor = new iTextSharp.text.BaseColor(51, 102, 102);
                SetCellBorder(cell);
                cell.BorderWidthBottom = 0.1f;
                table.AddCell(cell);
            }
        }

        /// <summary>
        /// Get font
        /// </summary>
        /// <returns></returns>
        private Font GetFont()
        {
            return iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 7);
        }

        /// <summary>
        /// Define width columns
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="table"></param>
        private void DefineWidths(PdfPTable table)
        {
            int numColumnsInPDF = this.Fields.Count;
            float[] columnWidthInPct = new float[numColumnsInPDF];
            int col;

            //--- see if we have width data for the Fields in XmlStore
            float widthTotal = numColumnsInPDF;
            for (col = 0; col < numColumnsInPDF; col++)
            {
                if (widthTotal == 0f)
                {
                    //--- equal widths (UGH!)
                    columnWidthInPct[col] = 100f / (float)numColumnsInPDF;
                }
                else
                {
                    float widthCol = DoGetColumnWidth(col);
                    columnWidthInPct[col] = widthCol;
                }
            }

            //float mfWidthScaleFactor = 1.0f;
            ////--- set the total width of the table
            //if (mfWidthScaleFactor <= 0 || widthTotal == 0f)
            //    table.WidthPercentage = 100; // percentage
            //else
            //    table.WidthPercentage = widthTotal * mfWidthScaleFactor; // percentage

            table.SetWidths(columnWidthInPct);
        }

        /// <summary>
        /// Define cell border
        /// </summary>
        /// <param name="cell"></param>
        private void SetCellBorder(PdfPCell cell)
        {
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthRight = 0f;
        }

        /// <summary>
        /// Load fields
        /// </summary>
        /// <param name="columns"></param>
        private void LoadFields(ObservableCollection<DataGridColumn> columns)
        {
            this.Fields = new List<Field>();
            foreach (DataGridColumn column in columns.Where(c => c.Visibility == System.Windows.Visibility.Visible))
                this.Fields.Add(new Field(column.Header.ToString(), DataType.String, this.GetWidth(column)));
        }

        /// <summary>
        /// Get width from column
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private double GetWidth(DataGridColumn column)
        {
            if (column.Width.ToDecimal() > 0)
                return column.Width.Value;
            return 100f;
        }

        /// <summary>
        /// Get column width
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public float DoGetColumnWidth(int col)
        {
            float widthCol = 0f;
            try
            {
                if (0 <= col && col < Fields.Count
                    && Fields[col].Width > 0)
                    widthCol = (float)Convert.ToDouble(Fields[col].Width);
            }
            catch (Exception ex)
            {
                widthCol = 0f;
            }
            return widthCol;
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private bool DoAddMetaData(Document document)
        {
            bool bRet = true;

            if (document != null)
            {
                // step 3: we add some metadata and open the document
                document.AddTitle("AddTitle");
                document.AddSubject("AddSubject");
                document.AddKeywords("AddKeywords");
                //document.AddCreator("VVX.PDF");
                document.AddAuthor("AddAuthor");
                document.AddHeader("AddHeader", "0");
            }
            else
            {
                bRet = false;
            }

            return bRet;
        }

        public void Dispose()
        {

        }
    }
}