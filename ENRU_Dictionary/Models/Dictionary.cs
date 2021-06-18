using ENRU_Dictionary.Helpers;
using ENRU_Dictionary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ENRU_Dictionary.Models
{
    [Serializable]
    public class Dictionary : IData
    {
        public List<Word> Words { get; private set; }

        public Dictionary()
        {
            Words = new List<Word>();
        }

        public List<Word> ToList()
        {
            return Words.ToList();
        }

        public bool Add(Word newWord)
        {
            if (newWord != null)
            {
                Words.Add(newWord);
                return true;
            }
            return false;
        }

        public bool DeleteWord(Word noteForDeleting)
        {
            if (noteForDeleting == null)
            {
                return false;
            }
            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].Date == noteForDeleting.Date && Words[i].English == noteForDeleting.English && Words[i].Russian == noteForDeleting.Russian)
                {
                    Words.Remove(Words[i]);
                    return true;
                }
            }
            return false;
        }

        public Word FindWordOrReturnNULL(Word noteForSerching)
        {
            if (noteForSerching == null)
            {
                return null;
            }

            foreach (Word anyWord in Words)
            {
                if (anyWord.Date == noteForSerching.Date && anyWord.English == noteForSerching.English && anyWord.Russian == noteForSerching.Russian)
                    return anyWord;
            }

            return null;
        }

        public void SaveDataInDB()
        {
            var FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            var filename = Path.Combine(FolderPath, AppSettings.DICTIONARY_FILENAME);
            FileWorker.SaveDataToFile(App.ENRUDictionary, filename);
        }
    }
}
