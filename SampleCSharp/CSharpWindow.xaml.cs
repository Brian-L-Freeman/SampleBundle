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

    //Private WkChoose As WeekChooser
    //Private MonChoose As MonthChooser
    //Private YrChoose As YearChooser
        public CSharpWindow()
        {
            InitializeComponent();

            //    Dim sld1 As New BinarySlider(BinarySlider.SliderSize.Huge, "Yes", "No",, True, BinarySlider.ColorSchemes.GreenRed)
            //Dim sld2 As New BinarySlider(BinarySlider.SliderSize.Large, "Da", "Nyet",, True, BinarySlider.ColorSchemes.GreenRed) With {.Margin = New Thickness(10, 0, 0, 0)}
            //    Dim sld3 As New BinarySlider(BinarySlider.SliderSize.Medium, "Left", "Right",, True, BinarySlider.ColorSchemes.White) With {.Margin = New Thickness(10, 0, 0, 0)}
            //    Dim sld4 As New BinarySlider(BinarySlider.SliderSize.Small, "1", "2",, False, BinarySlider.ColorSchemes.WhiteYellow) With {.Margin = New Thickness(10, 0, 0, 0)}
            //    Dim curr As New CurrencyBox(80, True, BaseInput.FontSz.Standard)
            //WkChoose = New WeekChooser(1, 5, 1)
            //MonChoose = New MonthChooser(WkChoose, 1, 12, Month(Now)) With {.Width = 530}
            //    YrChoose = New YearChooser(MonChoose, 2018, 2020, 2019) With {.Width = 180}
            //    MonChoose.RelatedYearObject = YrChoose
            //wrpBinarySliders.Children.Add(sld1)
            //wrpBinarySliders.Children.Add(sld2)
            //wrpBinarySliders.Children.Add(sld3)
            //wrpBinarySliders.Children.Add(sld4)
            //grdYear.Children.Add(YrChoose)
            //grdMonth.Children.Add(MonChoose)
            
            btnOne.Click += CreateRadialButton;
            btnTwo.Click += CreateRadialButton;
            btnThree.Click += CreateRadialButton;
            btnFour.Click += CreateRadialButton;
            btnFive.Click += CreateRadialButton;
            btnSix.Click += CreateRadialButton;
            btnSeven.Click += CreateRadialButton;
            btnEight.Click += CreateRadialButton;
            CurrencyBoxC cbc = new CurrencyBoxC(80, true, BaseInputC._fontsz.Standard, "$0.00", false, false);
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
