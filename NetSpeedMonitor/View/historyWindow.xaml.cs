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
using System.Windows.Shapes;
using USTC.Software.hanyizhao.NetSpeedMonitor.Model;

namespace USTC.Software.hanyizhao.NetSpeedMonitor.View
{
    /// <summary>
    /// Interaction logic for historyWindow.xaml
    /// </summary>
    public partial class historyWindow : Window
    {
        //MedolHistory Data;
        public historyWindow()
        {
            InitializeComponent();
            Data.Initialize();
        }
    }
}
