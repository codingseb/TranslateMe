using System;

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
    }
}
