using System;

namespace TranslateMe
{
    public class TMLanguageChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="oldLanguageId">The Language Id before the change</param>
        /// <param name="newLanguageId">The Language Id after the change</param>
        public TMLanguageChangedEventArgs(string oldLanguageId, string newLanguageId)
        {
            OldLanguageId = oldLanguageId;
            NewLanguageId = newLanguageId;
        }

        /// <summary>
        /// The Language Id before the change
        /// </summary>
        public string OldLanguageId { get; private set; }

        /// <summary>
        /// The Language Id after the change
        /// </summary>
        public string NewLanguageId { get; private set; }
    }
}
