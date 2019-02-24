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
namespace SampleCSharp
{
    class BaseInputC : Grid
    {
        #region Properties
        public TextBox BaseTextBox;
        
        public enum _showflare { Show, Hide };
        public enum _fontsz { Small, Smaller, Standard, Medium, Large, VeryLarge};
        private bool _flare;
        public bool Flare
        {
            get { return _flare; }
            set
            {
                _flare = value;
                if(_flare == false)
                {
                    myrct.Visibility = Visibility.Hidden;
                    BaseTextBox.Opacity = 1;
                }
                else
                {
                    myrct.Visibility = Visibility.Visible;
                    BaseTextBox.Opacity = 0.9;
                }
            }

           }
        private Rectangle myrct;
        #endregion

        #region Constructor
        public BaseInputC(int FieldWidth = 80, VerticalAlignment VertAlign = VerticalAlignment.Center, HorizontalAlignment HorAlign = HorizontalAlignment.Center, _fontsz FontSize = _fontsz.Standard, TextAlignment TA = TextAlignment.Left,
            string S = "", TextWrapping TW = TextWrapping.Wrap)

        {
            HorizontalAlignment = HorAlign;
            VerticalAlignment = VertAlign;
            Width = FieldWidth;
            BlurEffect ErrorFlare = new BlurEffect { KernelType = KernelType.Gaussian, Radius = 10, RenderingBias = RenderingBias.Performance };
            myrct = new Rectangle { Name = "ErrorRectangle", Fill = Brushes.Red, Opacity = 0.33, StrokeThickness = 1,
                Effect = ErrorFlare, Visibility = Visibility.Hidden };
            BaseTextBox = new TextBox { Name = "TextBox", BorderBrush = Brushes.LightGray, Text = S, TextAlignment = TA, TextWrapping = TW,
                HorizontalAlignment = HorAlign, VerticalAlignment = VertAlign};
            switch(FontSize)
            {
                case _fontsz.Small:
                    Height = 24;
                    BaseTextBox.Height = 16;
                    BaseTextBox.Width = FieldWidth - 8;
                    BaseTextBox.Margin = new Thickness(4, 4, 0, 0);
                    BaseTextBox.FontSize = 8;
                    break;

                case _fontsz.Smaller:
                    Height = 26;
                    BaseTextBox.Height = 18;
                    BaseTextBox.Width = FieldWidth - 8;
                    BaseTextBox.Margin = new Thickness(4, 4, 0, 0);
                    BaseTextBox.FontSize = 10;
                    break;
                                       
                case _fontsz.Standard:
                    Height = 28;
                    BaseTextBox.Height = 20;
                    BaseTextBox.Width = FieldWidth - 8;
                    BaseTextBox.Margin = new Thickness(4, 4, 0, 0);
                    BaseTextBox.FontSize = 12;
                    break;

                case _fontsz.Medium:
                    Height = 34;
                    BaseTextBox.Height = 26;
                    BaseTextBox.Width = FieldWidth - 8;
                    BaseTextBox.Margin = new Thickness(4, 4, 0, 0);
                    BaseTextBox.FontSize = 16;
                    break;

                case _fontsz.Large:
                    Height = 36;
                    BaseTextBox.Height = 28;
                    BaseTextBox.Width = FieldWidth - 8;
                    BaseTextBox.Margin = new Thickness(4, 4, 0, 0);
                    BaseTextBox.FontSize = 18;
                    break;

                case _fontsz.VeryLarge:
                    Height = 44;
                    BaseTextBox.Height = 36;
                    BaseTextBox.Width = FieldWidth - 8;
                    BaseTextBox.Margin = new Thickness(4, 4, 0, 0);
                    BaseTextBox.FontSize = 24;
                    break;

                default:
                    break;
            }
            Children.Add(myrct);
            Children.Add(BaseTextBox);
        }
        #endregion

        #region PublicMethods
        public void UserFocus()
        {
            BaseTextBox.Focus();
            BaseTextBox.SelectAll();
        }
        #endregion
    }
}
