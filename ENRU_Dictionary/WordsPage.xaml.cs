using Android.Media;
using ENRU_Dictionary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ENRU_Dictionary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordsPage : ContentPage
    {
        MediaPlayer player;
        public WordsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = App.ENRUDictionary.ToList();
            player = new MediaPlayer();
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WordEntryPage
            {
                BindingContext = new Word()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new WordEntryPage
                {
                    BindingContext = e.SelectedItem as Word
                });
            }
        }

        public async void btOpenGTranslate(object sender, EventArgs e)
        {
            string uri = "https://www.google.com/search?q=переводчик%20" + editorWords.Text;
            await OpenBrowser(uri);
        }
        private async Task OpenBrowser(string uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        private void btSound_Clicked(object sender, EventArgs e)
        {
            Button anyButton = sender as Button;
            string text = anyButton.Text;
            player.Reset();
            try
            {
                player.SetDataSource(GetLinkForVoice(text));
                player.Prepare();
                player.Start();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK!");
            }
        }

        private string GetLinkForVoice(string _text)
        {
            _text = _text.Replace(" ", "%20");
            string result = "https://translate.google.com.vn/translate_tts?ie=UTF-8&q="
                + _text + "&tl=" + LanguageDetector(_text) + "&client=tw-ob";
            return result;
        }

        private string LanguageDetector(string _text)
        {
            char firstLetter = _text[0];
            if (((firstLetter >= 'a') && (firstLetter <= 'z')) || ((firstLetter >= 'A') && (firstLetter <= 'Z')))
            {
                return "en";
            }
            else
            {
                return "ru";
            }
        }

        async private void TestsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestsPage());
        }
    }
}