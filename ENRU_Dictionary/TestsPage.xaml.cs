using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ENRU_Dictionary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestsPage : ContentPage
    {
        string[] answers = new string[7];
        int result;
        public TestsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetRandomWords();
        }

        private void SetRandomWords()
        {
            SetWord(word1, 0);
            SetWord(word2, 1);
            SetWord(word3, 2);
            SetWord(word4, 3);
            SetWord(word5, 4);
            SetWord(word6, 5);
            SetWord(word7, 6);
        }

        private void SetWord(Label wordX, int answerIndex)
        {
            Random rnd = new Random();
            int randomIndex = rnd.Next(App.ENRUDictionary.Words.Count);
            int englishOrRussian = rnd.Next(0,100);
            if (englishOrRussian < 50)
            {
                wordX.Text = App.ENRUDictionary.Words[randomIndex].English;
                answers[answerIndex] = App.ENRUDictionary.Words[randomIndex].Russian;
            }
            else
            {
                wordX.Text = App.ENRUDictionary.Words[randomIndex].Russian;
                answers[answerIndex] = App.ENRUDictionary.Words[randomIndex].English;
            }
        }

        public void CheckAnswers(object sender, EventArgs e)
        {
            result = 0; 
            CheckWord(edWord1, imgWord1, answers[0]);
            CheckWord(edWord2, imgWord2, answers[1]);
            CheckWord(edWord3, imgWord3, answers[2]);
            CheckWord(edWord4, imgWord4, answers[3]);
            CheckWord(edWord5, imgWord5, answers[4]);
            CheckWord(edWord6, imgWord6, answers[5]);
            CheckWord(edWord7, imgWord7, answers[6]);

            tbResult.Text = $"{result}/7";
        }

        private void CheckWord(Editor edwordX, Image imgWordX, string answer)
        {
            if (edwordX.Text == answer)
            {
                imgWordX.Source = "ok_small.png";
                edwordX.TextColor = Color.Green;
                result++;
            }
            else
            {
                edwordX.Text = answer;
                edwordX.TextColor = Color.Red;
                imgWordX.Source = "wrong_small.png";
            }
        }
    }
}