﻿using System;
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
    /// Interaction logic for WebCamImage.xaml
    /// </summary>
    public partial class WebCamImage : UserControl
    {
        public ImageSource? Source { set{ image.Source = value; } }
        public WebCamImage()
        {
            InitializeComponent();
        }
    }
}
