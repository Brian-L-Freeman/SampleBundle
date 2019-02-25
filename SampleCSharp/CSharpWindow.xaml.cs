using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SampleCSharp
{
    public partial class CSharpWindow : Window
    {
       private RadialPosition GetRad;
        public CSharpWindow()
        {
            InitializeComponent();
            BinarySliderC sld1 = new BinarySliderC(BinarySliderC.SliderSize.Huge, "Yes", "No", 1, true, BinarySliderC.ColorSchemes.GreenRed);
            BinarySliderC sld2 = new BinarySliderC(BinarySliderC.SliderSize.Medium, "Ja", "Nein", 1, true, BinarySliderC.ColorSchemes.GreenRed) { Margin = new Thickness(10, 0, 0, 0) };
            BinarySliderC sld3 = new BinarySliderC(BinarySliderC.SliderSize.Small, "Up", "Down", 1, true, BinarySliderC.ColorSchemes.White) { Margin = new Thickness(10, 0, 0, 0) };
            BinarySliderC sld4 = new BinarySliderC(BinarySliderC.SliderSize.Huge, "1", "2", 1, true, BinarySliderC.ColorSchemes.WhiteYellow) { Margin = new Thickness(10, 0, 0, 0) };
            btnOne.Click += CreateRadialButton;
            btnTwo.Click += CreateRadialButton;
            btnThree.Click += CreateRadialButton;
            btnFour.Click += CreateRadialButton;
            btnFive.Click += CreateRadialButton;
            btnSix.Click += CreateRadialButton;
            btnSeven.Click += CreateRadialButton;
            btnEight.Click += CreateRadialButton;
            CurrencyBoxC cbc = new CurrencyBoxC(80, true, BaseInputC._fontsz.Standard, "$0.00", false, false);
            wrpBinarySliders.Children.Add(sld1);
            wrpBinarySliders.Children.Add(sld2);
            wrpBinarySliders.Children.Add(sld3);
            wrpBinarySliders.Children.Add(sld4);
            grdCurrency.Children.Add(cbc);
            cbc.UserFocus();
        }

        public void CreateRadialButton(object Sender, RoutedEventArgs e)
        {
            // Create button (image element) object and add event handlers
            GetRad = new RadialPosition();
            Button b = (Button)Sender;
            string s = b.Content.ToString();
            Image img = new Image { Name = "btnRadial" + s, Height = 30, Width = 30, Stretch = Stretch.UniformToFill };
            BitmapImage bimg = new BitmapImage();
            bimg.BeginInit();
            bimg.UriSource = new Uri("pack://application:,,,/SampleCSharp;component/Resources/" + s + ".png");
            bimg.EndInit();
            img.Source = bimg;

                
            Tuple<int, int> pos = GetRad.GetPosition(Convert.ToByte(s), 8, 100);
            int tmpx = pos.Item1;
            int tmpy = pos.Item2;
            tmpx -= Convert.ToInt16(img.Width / 2);
            tmpy -= Convert.ToInt16(img.Height / 2);
            img.Margin = new Thickness(tmpx, tmpy, 0, 0);
            tbReturnVals.Text = "X: " + tmpx + "  Y:" + tmpy;
            cnvInternal.Children.Add(img);
            }
    }
}
