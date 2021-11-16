using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.Models_OnlineFoms.DataAccess
{
    public class OnlineFormsDB
    {
        private readonly OnlineForms_Context _context = new OnlineForms_Context();

      

        public async Task<bool> InsertComplianceCertificate(ComplianceCert complianceCert)
        {
            try
            {
                _context.ComplianceCert.Add(complianceCert);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void CreatePDFDocument(ComplianceCert compl)
        {
            string pdfTemp = @"\\nisgrenada.org\Work_Files\incoming\Online_forms\Template\ComplianceCertificate.pdf"; // ---> It's the original pdf form you want to fill
            string newFile = @"\\nisgrenada.org\Work_Files\incoming\Online_forms\ComplianceCertificate\" + compl.ImportedId + ".pdf"; // ---> It will generate new pdf that you have filled from your program
                                                                                                                                      // ------ READING -------
            PdfReader pdfReader = new PdfReader(pdfTemp);
            // ------ WRITING -------
            // If you don’t specify version and append flag (last 2 params) in below line then you may receive “Extended Features” error when you open generated PDF
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create), '6', false);
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            // ------ SET YOUR FORM FIELDS ------
            pdfFormFields.SetField("Employer Number", compl.EmployerNo);
            pdfFormFields.SetField("Business Name", compl.BusinessName);
            pdfFormFields.SetField("Business Address", compl.BusinessAddress);
            pdfFormFields.SetField("Telephone Number", compl.Telephone);
            pdfFormFields.SetField("Email Address", compl.Email);
            pdfFormFields.SetField("Reason for Certificate", compl.Reason);
            pdfFormFields.SetField("Title", compl.Title);
            pdfFormFields.SetField("AppDate", compl.AppDate.ToString());
            pdfStamper.FormFlattening = true;
            // close the pdf
            pdfStamper.Close();
        }


    }
}
