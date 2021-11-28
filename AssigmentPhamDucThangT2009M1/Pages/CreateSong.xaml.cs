using AssigmentPhamDucThangT2009M1.Entities;
using AssigmentPhamDucThangT2009M1.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateSong : Page
    {
        private FileService fileService;
        private AccountService accountService;
        private SongService songService;
        private static string _accessToken;
        public CreateSong()
        {
            this.InitializeComponent();
            fileService = new FileService();
            this.Loaded += CreateSong_LoadedAsync;

        }

        private async void CreateSong_LoadedAsync(object sender, RoutedEventArgs e)
        {
            fileService = new FileService();
            accountService = new AccountService();
            songService = new SongService();
            var credential = await accountService.LoadAccessTokenFromFile();
            _accessToken = credential.access_token;
        }

        private async void Create_SongAsync(object sender, RoutedEventArgs e)
        {
            var song = new Song
            {
                name = Name.Text,
                author = Author.Text,
                singer = Singer.Text,
                thumbnail = Thumbnail.Text,
                link = Link.Text,
                message = Message.Text,
                description = Description.Text
            };
            var result = await songService.CreateSongAsync(song);
            if (result != null)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action success";
                contentDialog.Content = $"Register success.";
                contentDialog.PrimaryButtonText = "Okie";
                await contentDialog.ShowAsync();
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action fails";
                contentDialog.Content = $"Register fails. Please try again!";
                contentDialog.PrimaryButtonText = "Okie";
                await contentDialog.ShowAsync();
            }
        }

        private async void Handle_Click_UploadImg(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                //config picker
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Downloads
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".mp3");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                pr_ProgressRing1.IsActive = true;
                // pr_ProgressRing1.Visibility = Visibility.Visible;
                var result = await fileService.UploadFile(file);
                pr_ProgressRing1.IsActive = false;
                // pr_ProgressRing1.Visibility = Visibility.Collapsed;               
                Thumbnail.Text = result;
            }
        }

        private async void Handle_Click_UploadMp3(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                // config picker.
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Downloads
            };          
            picker.FileTypeFilter.Add(".mp3");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                pr_ProgressRing2.IsActive = true;
                var result = await fileService.UploadFile(file);
                pr_ProgressRing2.IsActive = false;
                Link.Text = result;
            }
        }
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.ListSong));
        }
    }
}
