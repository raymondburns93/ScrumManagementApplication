using System;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using ScrumManagementApp.Client.Helpers;
using ScrumManagementApp.Client.ViewModels;

namespace ScrumManagementApp.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            //Messenger.Default.Register<NavigationMessage>(this, p =>
            //{
            //    // Create a URI to the target page
            //    var uri = new Uri(p.TargetPage, UriKind.Relative);

            //    // Find the frame we are currently in using the ModernUI "NavigationHelper" - THIS WILL NOT WORK IN THE VIEWMODEL
            //    var frame = NavigationHelper.FindFrame(null, this);

            //    // Set the frame source, which initiates navigation
            //    frame.Source = uri;
            //});

        }
    }
}
