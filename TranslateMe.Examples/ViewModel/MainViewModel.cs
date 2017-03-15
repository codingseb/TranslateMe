using System;
using System.Collections.Generic;
using System.Linq;

namespace TranslateMe.Examples
{
    public class MainViewModel : NotifyPropertyChangedBaseClass
    {
        private static MainViewModel instance = null;

        public static MainViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainViewModel();
                }

                return instance;
            }
        }

        private MainViewModel()
        {
            Languages.Init();
        }

        public TM LanguagesManager
        {
            get { return TM.Instance; }
        }

        public List<string> Labels
        {
            get
            {
                return TM.Instance
                    .TranslationsDictionary
                    .Keys.ToList<string>()
                    .FindAll(k => k.StartsWith("Text:"));
            }
        }

        private string label;
        public string Label
        {
            get { return label; }
            set
            {
                label = value;
                NotifyPropertyChanged();
            }
        }

    }
}
