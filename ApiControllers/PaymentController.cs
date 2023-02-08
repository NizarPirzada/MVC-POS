using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVCPOS.DataLayer;
using MVCPOS.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using System.Web;

namespace MVCPOS.ApiControllers
{

    public class PaymentController : ApiController
    {
        CustomerRepository repo = new CustomerRepository();
        CompanyRepository companyRepo = new CompanyRepository();
        InvoiceRepository invoiceRepo = new InvoiceRepository();

        [HttpGet]
        public bool GenerateReceipt(string invoice)
        {
           // invoiceRepo.GetInvoice
            var company = companyRepo.GetCompanyDetails();
            var invoiceDetails = invoiceRepo.GetInvoiceDetail( Convert.ToInt32(invoice));
            InvoiceReceiptModel invoiceReceiptModel = new InvoiceReceiptModel();
            List<ReceiptItem> itemList = new List<ReceiptItem>();

            invoiceReceiptModel.CompanyName = company.StrCompanyName;
            invoiceReceiptModel.CompanyEamil = company.StrCompanyEmail;
            invoiceReceiptModel.CompanyPhone = company.StrCompanyPhone;
            invoiceReceiptModel.CompanyFooter = company.StrInvoiceFooter;
            invoiceReceiptModel.CompanyAddress = company.StrCompanyAddress;
            invoiceReceiptModel.InvoiceId = invoice;

            if (invoiceDetails?.Count > 0)
            {
                invoiceDetails.ForEach(x => {
                    ReceiptItem item = new ReceiptItem();
                    item.Name = x.Product;
                    item.Price = x.LineTotal;
                    item.Quantity = x.QuantityShipped;
                    itemList.Add(item);
                });
            }
            invoiceReceiptModel.ReceiptItems = itemList;
            CreateNewPDF(invoiceReceiptModel);            
            return true;
        }

        static public void CreateNewPDF(InvoiceReceiptModel invoiceReceiptModel)
        {
            var pdfDoc = new Document(PageSize.LETTER, 230f, 230f, 60f, 60f);

            var basePath = HttpContext.Current.Server.MapPath("~/");

            var pdffileDirectory = Path.Combine(basePath, "content\\receipts\\");

            string pathUriPDF = new Uri(pdffileDirectory).LocalPath;

            bool existsPathUriPDF = Directory.Exists(pathUriPDF);

            if (!existsPathUriPDF)
                Directory.CreateDirectory(pathUriPDF);
            PdfWriter.GetInstance(pdfDoc, new FileStream(pathUriPDF + "Invoice"+ invoiceReceiptModel.InvoiceId + ".pdf", FileMode.OpenOrCreate));
            pdfDoc.Open();
            
            var logofileDirectory = Path.Combine(basePath, "content\\companylogo\\");
            string pathUriLogoImage = new Uri(logofileDirectory).LocalPath;

            using (FileStream fs = new FileStream(pathUriLogoImage + "companylogo.png", FileMode.Open))
            {
                var png = Image.GetInstance(System.Drawing.Image.FromStream(fs), ImageFormat.Png);
                png.ScalePercent(65f);
                png.SetAbsolutePosition(pdfDoc.Left+10, pdfDoc.Top-20);
                pdfDoc.Add(png);
            }
            var contactInfoStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, Font.BOLD);
            var contactInfo = new Chunk("Contact Info", contactInfoStyle);


            var spacer = new Paragraph("")
            {
                SpacingBefore = 5f,
                SpacingAfter = 5f,
            };

            pdfDoc.Add(spacer);
            pdfDoc.Add(contactInfo);
            pdfDoc.Add(spacer);

            var contactValueStyle = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, BaseColor.BLACK);
            var address = new Paragraph("Address : "+ invoiceReceiptModel.CompanyAddress, contactValueStyle);
            var email = new Paragraph("Email : " + invoiceReceiptModel.CompanyEamil, contactValueStyle);
            var phone = new Paragraph("Phone : "+ invoiceReceiptModel.CompanyPhone, contactValueStyle);
            pdfDoc.Add(address);
            pdfDoc.Add(email);
            pdfDoc.Add(phone);
            //pdfDoc.Add(spacer);


            PdfPTable table = new PdfPTable(3);
            table.TotalWidth = 150f;
            table.LockedWidth = true;
            float[] widths = new float[] { 2f, 1f, 1f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            //leave a gap before and after the table

            table.SpacingBefore = 20f;

            table.SpacingAfter = 10f;


            #region Table Header
            var headerFontStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6, Font.BOLD);
            //table.DefaultCell.Border = Rectangle.NO_BORDER;
            var cell = new PdfPCell(new Phrase("Items", headerFontStyle))
            {
                Colspan = 1,    //columnCount,
                HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                MinimumHeight = 20f,
                PaddingTop = 5,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(239, 240, 242)

            };
            var cell2 = new PdfPCell(new Phrase("Qty", headerFontStyle))
            {
                Colspan = 1,    //columnCount,
                HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                MinimumHeight = 20f,
                PaddingTop = 5,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(239, 240, 242)

            };
            var cell3 = new PdfPCell(new Phrase("Sub Total", headerFontStyle))
            {
                Colspan = 1,    //columnCount,
                HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                MinimumHeight = 20f,
                PaddingTop = 5,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(239, 240, 242)

            };
            table.AddCell(cell);
            table.AddCell(cell2);
            table.AddCell(cell3);
            #endregion

            // list of items binding 
            #region Table Body List of items
            //List<InvoiceReceiptModel> listItems = GetItems();
            if(invoiceReceiptModel?.ReceiptItems.Count>0)
            {
                foreach (var item in invoiceReceiptModel.ReceiptItems)
                {
                    var rowCellStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6, Font.NORMAL);
                    var firstCellColumn = new PdfPCell(new Phrase(item.Name, rowCellStyle))
                    {
                        Colspan = 1,    //columnCount,
                        HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                        MinimumHeight = 20f,
                        PaddingTop = 5,
                        Border = Rectangle.BOTTOM_BORDER,
                        BorderColor = new BaseColor(240, 240, 240),
                        BackgroundColor = new BaseColor(255, 255, 255)

                    };
                    table.AddCell(firstCellColumn);
                    var secondCellColumn = new PdfPCell(new Phrase(item.Quantity.ToString(), rowCellStyle))
                    {
                        Colspan = 1,    //columnCount,
                        HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                        MinimumHeight = 20f,
                        PaddingTop = 5,
                        Border = Rectangle.BOTTOM_BORDER,
                        BorderColor = new BaseColor(240, 240, 240),
                        BackgroundColor = new BaseColor(255, 255, 255)

                    };
                    table.AddCell(secondCellColumn);
                    var thirdCellColumn = new PdfPCell(new Phrase(item.Price.ToString(), rowCellStyle))
                    {
                        Colspan = 1,    //columnCount,
                        HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                        MinimumHeight = 20f,
                        PaddingTop = 5,
                        Border = Rectangle.BOTTOM_BORDER,
                        BorderColor = new BaseColor(240, 240, 240),
                        BackgroundColor = new BaseColor(255, 255, 255)

                    };
                    table.AddCell(thirdCellColumn);
                }
            }
            
            #endregion
            //footer generation of reciept
            #region Table Footer
            var emptyCell = new PdfPCell(new Phrase("", headerFontStyle))
            {
                Colspan = 1,    //columnCount,
                HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                MinimumHeight = 15f,
                PaddingTop = 5,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(239, 240, 242)

            };
            var taxCell2 = new PdfPCell(new Phrase("Tax 1", headerFontStyle))
            {
                Colspan = 1,    //columnCount,
                HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                MinimumHeight = 15f,
                PaddingTop = 5,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(239, 240, 242)

            };           
            var taxCell3 = new PdfPCell(new Phrase("10", headerFontStyle)) //temporary value
            {
                Colspan = 1,    //columnCount,
                HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                MinimumHeight = 15f,
                PaddingTop = 5,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(239, 240, 242)

            };

            table.AddCell(emptyCell);
            table.AddCell(taxCell2);
            table.AddCell(taxCell3);

            var emptyCellTax2 = new PdfPCell(new Phrase("", headerFontStyle))
            {
                Colspan = 1,    //columnCount,
                HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                MinimumHeight = 15f,
                PaddingTop = 5,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(239, 240, 242)

            };
            var taxCell2Tax2 = new PdfPCell(new Phrase("Tax 2", headerFontStyle))
            {
                Colspan = 1,    //columnCount,
                HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                MinimumHeight = 15f,
                PaddingTop = 5,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(239, 240, 242)

            };
            var taxCell3Tax2 = new PdfPCell(new Phrase("20", headerFontStyle)) //temporary value
            {
                Colspan = 1,    //columnCount,
                HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                MinimumHeight = 15f,
                PaddingTop = 5,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(239, 240, 242)

            };

            table.AddCell(emptyCellTax2);
            table.AddCell(taxCell2Tax2);           
            table.AddCell(taxCell3Tax2);


            var totalCell2 = new PdfPCell(new Phrase("Total", headerFontStyle))
            {
                Colspan = 1,    //columnCount,
                HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                MinimumHeight = 15f,
                PaddingTop = 5,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(239, 240, 242)

            };
            var totalCell3 = new PdfPCell(new Phrase("1050", headerFontStyle)) //temporary value
            {
                Colspan = 1,    //columnCount,
                HorizontalAlignment = 0,  //0=Left, 1=Centre, 2=Right
                MinimumHeight = 15f,
                PaddingTop = 5,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new BaseColor(239, 240, 242)

            };

            table.AddCell(emptyCell);
            table.AddCell(totalCell2);
            table.AddCell(totalCell3);
            #endregion

            pdfDoc.Add(table);
            var thanksParaStyle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD);
            var thanksPara = new Paragraph("Thank you for your bussines!", thanksParaStyle);
            var paymentValidation = new Paragraph(invoiceReceiptModel.CompanyFooter, contactValueStyle);
            pdfDoc.Add(thanksPara);
            pdfDoc.Add(paymentValidation);
            pdfDoc.Close();

        }

       

    }
}
