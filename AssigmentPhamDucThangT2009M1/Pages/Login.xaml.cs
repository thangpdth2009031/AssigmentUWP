using AssigmentPhamDucThangT2009M1.Entities;
using AssigmentPhamDucThangT2009M1.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AssigmentPhamDucThangT2009M1.Pages
{
    public sealed partial class Login : Page
    {
        private AccountService accountService = new AccountService();
        public Login()
        {
            this.InitializeComponent();
            SharedShadow.Receivers.Add(BackgroundGrid);
            AddForm.Translation += new Vector3(120, 0, 32);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var loginInformation = new LoginInformation()
            {
                email = txtEmail.Text,
                password = txtPassword.Password.ToString()
            };
            var credential = await accountService.LoginAsync(loginInformation);
            if (credential == null)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action fails";
                contentDialog.Content = $"Please try again later!";
                contentDialog.PrimaryButtonText = "Got it";
                await contentDialog.ShowAsync();
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action success";
                contentDialog.Content = $"Welcome back";
                contentDialog.PrimaryButtonText = "Got it";
                App.CurrentAccount = await accountService.GetInformationAsync();
                await contentDialog.ShowAsync();
                this.Frame.Navigate(typeof(Pages.NavigationView));
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Register));
        }
    }
}
