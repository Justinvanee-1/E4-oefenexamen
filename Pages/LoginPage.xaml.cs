using E4_Project.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace E4_Project.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text?.Trim();
            var password = PasswordBoxControl.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await ShowMessageAsync("Vul zowel gebruikersnaam als wachtwoord in.");
                return;
            }

            // Uitschakelen om dubbelklikken te voorkomen
            LoginButton.IsEnabled = false;
            try
            {
                bool isValid = await AuthenticateAsync(username, password);
                if (isValid)
                {
                    Frame.Navigate(typeof(HomePage));
                }
                else
                {
                    await ShowMessageAsync("Onjuiste gebruikersnaam of wachtwoord.");
                }
            }
            finally
            {
                LoginButton.IsEnabled = true;
            }
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateAccountPage));
        }

        // Voorbeeld: vervang door echte check (API / DB). Gebruik async voor netwerk/db.
        private async Task<bool> AuthenticateAsync(string username, string password)
        {
            using var db = new AppDbContext();

            var user = db.userLogins.FirstOrDefault(u => u.Username == username);

            if (user == null)
                return false;

            if (user.Password != password)
                return false;

            return true;
        }

        private async Task ShowMessageAsync(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Inloggen",
                Content = message,
                CloseButtonText = "OK"
            };

            // Zorg dat we een XamlRoot instellen
            var xamlRoot = this.XamlRoot ?? (Microsoft.UI.Xaml.Window.Current?.Content as FrameworkElement)?.XamlRoot;
            if (xamlRoot == null)
            {
                // Fallback: geen visual root beschikbaar — log en stop netjes
                System.Diagnostics.Debug.WriteLine("Geen XamlRoot beschikbaar voor ContentDialog: " + message);
                return;
            }

            dialog.XamlRoot = xamlRoot;
            await dialog.ShowAsync();
        }
    }
}
