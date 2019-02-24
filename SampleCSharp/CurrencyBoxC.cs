using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Windows.Input;

namespace SampleCSharp
{
    class CurrencyBoxC : BaseInputC
    {
        #region Properties
        public bool Highlight;
        public TextBox TB;
        private bool _debitonly;
        private bool _creditonly;
        public bool _systemchange;
        private string _heldvalue;
        private bool _creditv;
        public bool Credit
        { get { return _creditv; }
            set { _creditv = value; }
        }
        private bool _debitv;
        public bool Debit
        {
            get { return _debitv; }
            set { _debitv = value; }
        }
        private double _setamountv;
        public double SetAmount
        {
            get { return _setamountv; }
            set { _setamountv = value; TB.Text = value.ToString(); }
        }
        private bool _fieldineditmodev;
        public bool FieldInEditMode
        {
            get { return _fieldineditmodev; }
            set { _fieldineditmodev = value; }
        }
        #endregion

        #region Constructor
        public CurrencyBoxC(int FieldWidth, bool SelectAllUponEnteringField, _fontsz FontSize, string DefaultText = "$0.00", bool ForceCredit = false,
            bool ForceDebit = false) : base(FieldWidth, VerticalAlignment.Center, HorizontalAlignment.Center, FontSize, TextAlignment.Right,
                DefaultText, TextWrapping.NoWrap)
        {
            Credit = !ForceDebit;
            Debit = !ForceCredit;
            _debitonly = ForceDebit;
            _creditonly = ForceCredit;
            _systemchange = false;
            _heldvalue = "";
            Highlight = SelectAllUponEnteringField;
            TB = base.BaseTextBox;
            TB.KeyDown += EnterKeyIsTab;
            TB.GotFocus += EnterField;
            TB.LostFocus += ExitField;
            TB.TextChanged += ValidateText;
        }

        #endregion

        #region PrivateMethods
        private void EnterKeyIsTab(object sender, KeyEventArgs k)
        {
            if (k.Key == Key.Enter)
            {
                TraversalRequest dr = new TraversalRequest(FocusNavigationDirection.Next);
                TB.MoveFocus(dr);
                k.Handled = true;
            }
        }

        private void EnterField(object sender, EventArgs e)
        {
            FieldInEditMode = true;
            _heldvalue = TB.Text;
            _systemchange = true;

            // Remove currency symbol, if present  
            TB.Text = TB.Text.Replace("$", "");
            
            // Remove negative parentheses, if present
            if( TB.Text.IndexOf("(") > 0)
            {
                TB.Text = TB.Text.Replace("(", "");
                TB.Text = TB.Text.Replace(")", "");
                TB.Text = "-" + TB.Text;
            }

            if (Highlight == true)
            {
                TB.SelectAll();
            }
            else
            {
                TB.CaretIndex = TB.Text.Length;
            }

            _systemchange = false;
        }

        private void ExitField(object sender, EventArgs e)
        {
            FieldInEditMode = false;
            _systemchange = true;
            try
            {
                double cval = Convert.ToDouble(TB.Text);
                if ((Debit == false && cval > 0) || (Credit = false && cval < 0))
                {
                   
                    Flare = true;
                }
                else
                {
                    Flare = false;
                }
                SetAmount = cval;
            }
            catch (Exception)
            {
                Flare = true;
                this.Focus();
            }

            try
            {
                double cval = Convert.ToDouble(TB.Text);
                if ((_debitonly == true && cval < 0) || (_creditonly = true && cval > 0))
                {
                    Flare = false;
                    cval = -cval;
                    SetAmount = cval;
                }
            }
            catch (Exception)
            {
                Flare = true;
                this.Focus();
            }
            if (TB.Text != _heldvalue)
            {
                //ValueChanged();
            }
            try
            {
                double cval = Convert.ToDouble(TB.Text);
                TB.Text = cval.ToString("C", CultureInfo.CurrentCulture);
            }
            catch
            { }
            _systemchange = false;
            //If t.Text = _heldvalue Then Exit Sub
            //RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(“Amountchanged”))
        }


        private void ValidateText(object sender, EventArgs e)
        {
            Flare = false;
            if (_systemchange == false)
            {
                try
                {
                    double cval = Convert.ToDouble(TB.Text);
                }
                catch (Exception)
                {
                    Flare = true;
                }
            }
        }
        #endregion
    }

}
