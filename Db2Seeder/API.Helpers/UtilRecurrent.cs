using Microsoft.Extensions.Configuration;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Db2Seeder.API.Helpers
{
    public static class UtilRecurrent
    {
        #region Messages
        public static bool yesOrNot(string message, string title)
        {
            bool response = false;
            try
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    response = true;
                }
                else
                {
                    response = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            return response;
        }
        public static void ErrorMessage(string message)
        {
            try
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        public static void InformationMessage(string message, string title)
        {
            try
            {
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion

        #region Controls
        public static List<TextBox> FindAllTextBoxIterative(Control parent)
        {
            List<TextBox> list = new List<TextBox>();
            Stack<Control> ContainerStack = new Stack<Control>();
            ContainerStack.Push(parent);
            while (ContainerStack.Count > 0)
            {
                foreach (Control child in ContainerStack.Pop().Controls)
                {
                    if (child.HasChildren)
                        ContainerStack.Push(child);
                    if (child.GetType() == typeof(TextBox))
                        list.Add((TextBox)child);
                }
            }
            return list;
        }
        public static List<Control> FindAllControlsIterative(Control parent, string type)
        {
            List<Control> list = new List<Control>();
            Stack<Control> ContainerStack = new Stack<Control>();
            ContainerStack.Push(parent);
            while (ContainerStack.Count > 0)
            {
                foreach (Control child in ContainerStack.Pop().Controls)
                {
                    if (child.HasChildren)
                        ContainerStack.Push(child);
                    if (child.GetType().Name == type)
                        list.Add(child);
                }
            }
            return list;
        }

        #endregion

        // ################TXT KEY PRESS########################
        public static void txtOnlyIntegersNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        public static void txtOnlyDecimalNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (char.IsControl(e.KeyChar))
                e.Handled = false;
            else if (e.KeyChar == '.' && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
            else if (e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
                e.Handled = false;
            else
                e.Handled = true;
        }
        public static void txtOnlyLetters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.Handled = false;
            else if (char.IsControl(e.KeyChar))
                e.Handled = false;
            else if (char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else if (char.IsWhiteSpace(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }


        //############### DGV
        public static void addBottomColumns(DataGridView dgv, string colName, string colText)
        {
            try
            {
                if (!dgv.Columns.Contains(colName))
                {
                    DataGridViewButtonColumn btnOForm = new DataGridViewButtonColumn();
                    btnOForm.HeaderText = colText;
                    btnOForm.Name = colName;
                    btnOForm.UseColumnTextForButtonValue = true;
                    dgv.Columns.Add(btnOForm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    IdnMapping idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static async Task<byte[]> ConvertImagetoPDF(byte[] imagerray, string extention)
        {
            try
            {

                Stream imageStream = new MemoryStream(imagerray);
                byte[] result = Array.Empty<byte>();
                PdfDocument document2 = new PdfDocument();
                PdfPage page = document2.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                System.Drawing.Image img = System.Drawing.Image.FromStream(imageStream);
                page.Width = img.Width;
                page.Height = img.Height;

                DrawImage(gfx, imageStream, 0, 0, (int)page.Width, (int)page.Height);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var enc1252 = Encoding.GetEncoding(1252);
                MemoryStream stream = new MemoryStream();
                document2.Save(stream, false);
                result = new byte[stream.Length];
                await stream.ReadAsync(result, 0, (int)stream.Length);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static void DrawImage(XGraphics gfx, Stream jpegSamplePath, int x, int y, int width, int height)
        {
            try
            {
                XImage image = XImage.FromStream(jpegSamplePath);
                gfx.DrawImage(image, x, y, width, height);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //############ Email

       
        public async static Task SendMail(string to, string subject, string body)
        {
            try
            {
                var _configuration= ConfigurationManager.AppSettings;
                string smtp = _configuration["Smtp"];
                MailMessage Email = new MailMessage();
                using (SmtpClient client = new SmtpClient(smtp))
                {
                    client.Port = 587;
                    client.EnableSsl = true;
                    Email.From = new MailAddress(_configuration["From"], "DB2-Seeder");
                    Email.To.Add(new MailAddress(to));
                    Email.Subject = subject;
                    Email.Body = body;
                    Email.IsBodyHtml = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.TargetName = "STARTTLS/smtp.office365.com";
                    client.Credentials = new NetworkCredential(_configuration["User"], _configuration["Password"]);
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (object se, System.Security.Cryptography.X509Certificates.X509Certificate cert, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslerror) => true;
                   await  client.SendMailAsync(Email);
                    Email.Dispose();
                    client.Dispose();
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
