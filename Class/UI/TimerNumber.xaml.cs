using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace paper_rock_scissors.Class.UI
{
    /// <summary>
    /// Interaction logic for TimerNumber.xaml
    /// </summary>
    public partial class TimerNumber : UserControl
    {
        Dictionary<string, Brush> brushs = new Dictionary<string, Brush>() { {"1", Brushes.Green}, { "2", Brushes.Orange }, { "3", Brushes.Red }, {"4", Brushes.Black } };
        public TimerNumber()
        {
            InitializeComponent();
        }

        public void Refresh(string number)
        {
            counter.Content = number;
            counter.Foreground = brushs[number];
            StartAnimation();
        }
        private void StartAnimation()
        {
            var FontSizeAnimation = new DoubleAnimation() { To = 100, Duration = TimeSpan.FromSeconds(0.2) };
            FontSizeAnimation.Completed += (s,e) => StopAnimation();
            counter.BeginAnimation(Label.FontSizeProperty, FontSizeAnimation);
        }
        private void StopAnimation()
        {
            var FontSizeAnimation = new DoubleAnimation() { To = 72, Duration = TimeSpan.FromSeconds(0.2) };
            counter.BeginAnimation(Label.FontSizeProperty, FontSizeAnimation);
        }
    }
}
