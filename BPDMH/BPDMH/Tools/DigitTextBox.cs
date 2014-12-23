using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace BPDMH.Tools
{
    public class DigitTextBox : TextBox
    {
        //http://www.wpfsharp.com/tag/textbox/
        public DigitTextBox()
        {
            TextChanged += OnTextChanged;
            KeyDown += OnKeyDown;
        }
 
        #region Properties
        new public String Text
        {
            get { return base.Text; }
            set
            {
                base.Text = LeaveOnlyNumbers(value);
            }
        }
 
        #endregion
 
        #region Functions
        private static bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }
 
        private bool IsDelOrBackspaceOrTabKey(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back || inKey == Key.Tab;
        }
 
        private static string LeaveOnlyNumbers(String inString)
        {
            return inString.Where(c => !System.Text.RegularExpressions.Regex.IsMatch(c.ToString(CultureInfo.InvariantCulture), "^[0-9]*$"))
                .Aggregate(inString, (current, c) => current.Replace(c.ToString(CultureInfo.InvariantCulture), ""));
        }

        #endregion
 
        #region Event Functions
        protected void OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key) && !IsDelOrBackspaceOrTabKey(e.Key);
        }
 
        protected void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            base.Text = LeaveOnlyNumbers(Text);
        }
        #endregion
    }
}
