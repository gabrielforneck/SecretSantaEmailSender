﻿using SecretSantaEmailSender.Frontend.Views.MainView.ViewModel;
using System.Windows;

namespace SecretSantaEmailSender.Frontend.Views.MainView.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(IMainViewModel mainViewModel)
    {
        InitializeComponent();
        DataContext = mainViewModel;
    }
}