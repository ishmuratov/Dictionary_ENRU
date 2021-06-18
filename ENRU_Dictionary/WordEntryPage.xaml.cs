using ENRU_Dictionary.Helpers;
using ENRU_Dictionary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ENRU_Dictionary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordEntryPage : ContentPage
    {
        public WordEntryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetButtonDeleteText();
        }

        private void SetButtonDeleteText()
        {
            var newWord = (Word)BindingContext;
            Word newOrModifiedNote = App.ENRUDictionary.FindWordOrReturnNULL(newWord);
            if (newOrModifiedNote == null)
            {
                Title = "New Word Entry";
                btDelete.Text = "Cancel";
            }
            else
            {
                Title = "Edit Word";
                btDelete.Text = "Delete";
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var newWord = (Word)BindingContext;

            Word newOrModifiedNote = App.ENRUDictionary.FindWordOrReturnNULL(newWord);
            if (newOrModifiedNote == null)
            {
                // Save
                if (App.ENRUDictionary.Add(newWord))
                {
                    App.ENRUDictionary.SaveDataInDB();
                }
            }
            else
            {
                // Update in DB
                newOrModifiedNote.English = newWord.English;
                newOrModifiedNote.Russian = newWord.Russian;
                App.ENRUDictionary.SaveDataInDB();

            }

            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var wordForDeleting = (Word)BindingContext;
            if (App.ENRUDictionary.DeleteWord(wordForDeleting))
            {
                App.ENRUDictionary.SaveDataInDB();
            }
            await Navigation.PopAsync();
        }
    }
}