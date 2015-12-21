using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using ScrumManagementApp.Client.ViewModels;

namespace ScrumManagementApp.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow win = new MainWindow();
            ApplicationViewModel context = new ApplicationViewModel();
            win.DataContext = context;
            win.Show();



            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB"); ;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-GB"); ;

            FrameworkElement.LanguageProperty.OverrideMetadata(
              typeof(FrameworkElement),
              new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);
        }
    }

}
