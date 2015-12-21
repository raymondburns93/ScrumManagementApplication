using System.Windows;
using System.Windows.Controls;

namespace ScrumManagementApp.Client.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class RegisterView
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        //private void btnCancel_Click(object sender, RoutedEventArgs e)
        //{
        //    LoginView redirect = new LoginView();
        //    redirect.Show();
        //    this.Close();
        //}

        private void RegisterConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password1 = ((PasswordBox)sender).Password;
            }
        }

        private void RegisterPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
