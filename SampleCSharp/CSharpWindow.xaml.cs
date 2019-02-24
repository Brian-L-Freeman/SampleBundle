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
    /// <summary>
    /// Interaction logic for CSharpWindow.xaml
    /// </summary>
    public partial class CSharpWindow : Window
    {
        public CSharpWindow()
        {
            InitializeComponent();
            CurrencyBoxC cbc = new CurrencyBoxC(80, true, BaseInputC._fontsz.Standard, "$0.00", false, false);
            grdCurrency.Children.Add(cbc);
        }
    }
}
