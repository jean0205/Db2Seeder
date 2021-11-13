using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ShareModels.Helpers
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
        public  static List<TextBox> FindAllTextBoxIterative( Control parent)
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
        public static List<Control> FindAllControlsIterative(Control parent, string  type)
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
                    if (child.GetType().Name==type)
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
            else if (e.KeyChar== '.' && ((TextBox)sender).Text.Length == 0)
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

    }
}
