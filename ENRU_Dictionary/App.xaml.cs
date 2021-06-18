using Android.Media;
using ENRU_Dictionary.Helpers;
using ENRU_Dictionary.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ENRU_Dictionary
{
    public partial class App : Application
    {
        public static string FolderPath { get; private set; }
        public static Dictionary ENRUDictionary { get; private set; }

        public App()
        {
            InitializeComponent();
            LoadWordsFromDB();
            MainPage = new NavigationPage(new WordsPage());
        }

        private void LoadWordsFromDB()
        {
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            var filename = Path.Combine(FolderPath, AppSettings.DICTIONARY_FILENAME);
            ENRUDictionary = (Dictionary)FileWorker.LoadDataFromFile(filename);
            if (ENRUDictionary == null)
            {
                ENRUDictionary = new Dictionary();
            }
        }
    }
}
