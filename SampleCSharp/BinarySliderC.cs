using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Effects;
namespace SampleCSharp
{
    class BinarySliderC : Grid
    {
        #region Properties
        public bool TimerActive;
        private TextBlock _firsttextchoice;
        private TextBlock _secondtextchoice;
        private RadialGradientBrush _rgbpanel;
        private Border _brdpanel;
        private int _verttextpad;
        private int _internalfontsize;
        private int _internalheight;
        private int _internalwidth;
        private Brush _firstcolorchoice = Brushes.LightGreen;
        private Brush _secondcolorchoice = Brushes.PaleVioletRed;
        private Color _firstpanelcolor = Colors.DarkBlue;
        private Color _secondpanelcolor = Colors.White;
        private bool _choiceval;
        private DispatcherTimer _slidertimer;
        private int _panelxpos;
        public bool ChoiceVal
        { get { return _choiceval; }
            set
            {
                if (TimerActive == false)
                {
                    _choiceval = value;
                    _slidertimer = new DispatcherTimer();
                    _slidertimer.Tick += SlidePanel;
                    _slidertimer.Interval = new TimeSpan(0, 0, 0, 0, 0);
                    TimerActive = true;
                    _slidertimer.Start();
                }
            }
        }
        public enum SliderSize { Small, Medium, Standard, Large, Huge };
        public enum ColorSchemes { GreenRed, White, WhiteYellow };
        #endregion

        #region Constructor

        public BinarySliderC(SliderSize InstanceSize, string Option1, string Option2, byte Extend = 1, bool ShadowOn = false,
            ColorSchemes ColorsScheme = ColorSchemes.GreenRed)
        {
            switch (InstanceSize)
            {
                case SliderSize.Small:
                    _internalheight = 20;
                    _internalwidth = 40 * Extend;
                    _internalfontsize = 8;
                    _verttextpad = 4;
                    break;
                case SliderSize.Medium:
                    _internalheight = 30;
                    _internalwidth = 60 * Extend;
                    _internalfontsize = 12;
                    _verttextpad = 6;
                    break;
                case SliderSize.Standard:
                    _internalheight = 40;
                    _internalwidth = 80 * Extend;
                    _internalfontsize = 18;
                    _verttextpad = 16;
                    break;
                case SliderSize.Large:
                    _internalheight = 60;
                    _internalwidth = 120 * Extend;
                    _internalfontsize = 18;
                    _verttextpad = 16;
                    break;
                case SliderSize.Huge:
                    _internalheight = 80;
                    _internalwidth = 160 * Extend;
                    _internalfontsize = 24;
                    _verttextpad = 20;
                    break;

                default:
                    _internalheight = 20;
                    _internalwidth = 40 * Extend;
                    _internalfontsize = 8;
                    _verttextpad = 4;
                    break;
            }
            switch (ColorsScheme)
            {
                case ColorSchemes.GreenRed:
                    {
                        var firstconverter = new BrushConverter().ConvertFromString("#FFA0FF97");
                        _firstcolorchoice = (Brush)firstconverter;
                        var secondconverter = new BrushConverter().ConvertFromString("#FFFF5959");
                        _secondcolorchoice = (Brush)secondconverter;
                        break;
                    }
                case ColorSchemes.White:
                    {
                        _firstcolorchoice = Brushes.White;
                        _secondcolorchoice = Brushes.White;
                        break;
                    }

                case ColorSchemes.WhiteYellow:
                    {
                        _firstcolorchoice = Brushes.White;
                        _secondcolorchoice = Brushes.Yellow;
                        break;
                    }
                default:
                    break;
            }
            Height = _internalheight;
            Width = _internalwidth;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            _firsttextchoice = new TextBlock
            {
                Text = Option1,
                FontSize = _internalfontsize,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = _internalwidth / 2,
                Background = _firstcolorchoice,
                Height = _internalheight,
                Padding = new Thickness(0, _verttextpad, 0, 0),
                Margin = new Thickness(0, 0, 0, 0)
            };
            _secondtextchoice = new TextBlock
            {
                Text = Option2,
                FontSize = _internalfontsize,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = _internalwidth / 2,
                Background = _secondcolorchoice,
                Height = _internalheight,
                Padding = new Thickness(0, _verttextpad, 0, 0),
                Margin = new Thickness(0, 0, 0, 0)
            };
            _rgbpanel = new RadialGradientBrush();
            GradientStop gs1 = new GradientStop { Color = _firstpanelcolor, Offset = 3 };
            GradientStop gs2 = new GradientStop { Color = _secondpanelcolor};
            _rgbpanel.GradientStops.Add(gs1);
            _rgbpanel.GradientStops.Add(gs2);
            _brdpanel = new Border { HorizontalAlignment = HorizontalAlignment.Left, Width = _internalwidth / 2, BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1, 1, 1, 1), Background = _rgbpanel };
            _brdpanel.MouseLeftButtonDown += PanelClick;
            Children.Add(_firsttextchoice);
            Children.Add(_secondtextchoice);
            Children.Add(_brdpanel);
            if (ShadowOn == true)
            {
                Effect = new DropShadowEffect { BlurRadius = 10, ShadowDepth = 5 };
            }
        }
        #endregion

        #region PrivateMethods

        private void PanelClick(object sender, RoutedEventArgs e)
        {
            if ((TimerActive == true) || (IsEnabled = false)) { }
            else { ChoiceVal = !ChoiceVal; }
            e.Handled = true;
        }
        private void SlidePanel(object sender, EventArgs e)
        {
            if (ChoiceVal == true)
            {
                _panelxpos += 1;
                    if (_panelxpos > (_internalwidth/2))
                {
                    _slidertimer.Stop();
                    _slidertimer = null;
                    TimerActive = false;

                }
                    else
                {
                    _brdpanel.Margin = new Thickness(_panelxpos, 0, 0, 0);
                }
            }
            else
            {
                _panelxpos -= 1;
                if (_panelxpos <0)
                {
                    _slidertimer.Stop();
                    _slidertimer = null;
                    TimerActive = false;
                }
                else
                {
                    _brdpanel.Margin = new Thickness(_panelxpos, 0, 0, 0);
                }
            }
        }
        
        #endregion
    }
}
