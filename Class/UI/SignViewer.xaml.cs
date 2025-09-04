using Material.Icons;
using Material.Icons.WPF;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace paper_rock_scissors.Class.UI
{
    /// <summary>
    /// Interaction logic for SignViewer.xaml
    /// </summary>
    public partial class SignViewer : UserControl
    {
        private Dictionary<string, MaterialIconKind> icons = new Dictionary<string, MaterialIconKind>() { {"paper",MaterialIconKind.HandFrontLeft }, {"scissors", MaterialIconKind.ContentCut }, {"rock", MaterialIconKind.BoxingGlove } };
        public string IA { set { IASign.Content = $"IA: {value}"; IAIcon.Kind = icons[value]; } }
        public string Player { set { PlayerSign.Content = $"Player: {value}"; PlayerIcon.Kind = icons[value]; } }
        public SignViewer()
        {
            InitializeComponent();
        }
    }
}
