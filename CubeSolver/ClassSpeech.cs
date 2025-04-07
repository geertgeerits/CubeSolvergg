using System.Diagnostics;

namespace CubeSolver
{
    internal sealed class ClassSpeech
    {
        private static IEnumerable<Locale>? locales;

        /// <summary>
        /// Initialize text to speech
        /// </summary>
        public static async void InitializeTextToSpeech()
        {
            Globals.bExplainSpeechAvailable = await InitializeTextToSpeechAsync();

            if (!Globals.bExplainSpeechAvailable)
            {
                Globals.bExplainSpeech = false;
            }
        }

        /// <summary>
        /// Initialize text to speech and fill the the array with the speech languages
        /// <para>.Country = KR ; .Id = ''  ; .Language = ko ; .Name = Korean (South Korea) ;</para>
        /// </summary>
        public static async Task<bool> InitializeTextToSpeechAsync()
        {
            // Initialize text to speech
            int nTotalItems;

            try
            {
                locales = await TextToSpeech.Default.GetLocalesAsync();

                nTotalItems = locales.Count();

                if (nTotalItems == 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Text to speech is not supported on this device
#if DEBUG
                await Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message + "\n\n" + CubeLang.TextToSpeechError_Text, CubeLang.ButtonClose_Text);
#endif
                return false;
            }

            // Put the locales in the array and sort the array
            Globals.cLanguageLocales = new string[nTotalItems];
            int nItem = 0;

            foreach (var l in locales)
            {
                Globals.cLanguageLocales[nItem] = l.Language + "-" + l.Country + " " + l.Name;
                nItem++;
            }

            Array.Sort(Globals.cLanguageLocales);

            return true;
        }

        /// <summary>
        /// // Search the selected language in the cLanguageLocales array
        /// </summary>
        /// <param name="cCultureName"></param>
        public static int SearchArrayWithSpeechLanguages(string cCultureName)
        {
            Debug.WriteLine("SearchArrayWithSpeechLanguages - cCultureName: " + cCultureName);

            try
            {
                int nTotalItems = Globals.cLanguageLocales?.Length ?? 0;

                if (Globals.cLanguageLocales is not null)
                {
                    if (!string.IsNullOrEmpty(cCultureName))
                    {
                        // Search for the indonesian speech language as 'id' and 'in'
                        // Android generating old/wrong language code for Indonesia - https://stackoverflow.com/questions/44245959/android-generating-wrong-language-code-for-indonesia
                        if (cCultureName.StartsWith("id"))
                        {
                            for (int nItem = 0; nItem < nTotalItems; nItem++)
                            {
                                if (Globals.cLanguageLocales[nItem].StartsWith(cCultureName))
                                {
                                    Globals.cLanguageSpeech = Globals.cLanguageLocales[nItem];
                                    return nItem;
                                }
                            }

                            cCultureName = "in-ID";
                        }

                        // Search for the speech language as 'en-US'
                        for (int nItem = 0; nItem < nTotalItems; nItem++)
                        {
                            if (Globals.cLanguageLocales[nItem].StartsWith(cCultureName))
                            {
                                Globals.cLanguageSpeech = Globals.cLanguageLocales[nItem];
                                return nItem;
                            }
                        }

                        // Select the characters before the first hyphen if there is a hyphen in the string
                        cCultureName = cCultureName.Split('-')[0];

                        // Search for the speech language as 'en'
                        for (int nItem = 0; nTotalItems > nItem; nItem++)
                        {
                            if (Globals.cLanguageLocales[nItem].StartsWith(cCultureName))
                            {
                                Globals.cLanguageSpeech = Globals.cLanguageLocales[nItem];
                                return nItem;
                            }
                        }
                    }
                }

                // If the language is not found use the first language in the array
                if (string.IsNullOrEmpty(Globals.cLanguageSpeech) && nTotalItems > 0)
                {
                    Globals.cLanguageSpeech = Globals.cLanguageLocales![0];
                    return 0;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
#endif
            }

            return 0;
        }

        /// <summary>
        /// Convert text to speech
        /// </summary>
        /// <param name="cText"></param>
        /// <returns></returns>
        public static async Task ConvertTextToSpeechAsync(string cText)
        {
            /* If you do not wait long enough to press the arrow key in the Task 'MakeExplainTurnAsync()',
               an error message will sometimes appear: 'The operation was canceled'.
               This only occurs if the 'Explained by speech' setting is enabled.
               The error occurs in the method 'ConvertTextToSpeechAsync()'. */

            // Cancel the text to speech
            if (Globals.bTextToSpeechIsBusy)
            {
                if (Globals.cts?.IsCancellationRequested ?? true)
                {
                    return;
                }

                Globals.cts.Cancel();
            }

            // Start with the text to speech
            if (cText is not null and not "")
            {
                Globals.bTextToSpeechIsBusy = true;

                try
                {
                    Globals.cts = new CancellationTokenSource();

                    SpeechOptions options = new()
                    {
                        Locale = locales?.Single(static l => l.Language + "-" + l.Country + " " + l.Name == Globals.cLanguageSpeech)
                    };

                    await TextToSpeech.Default.SpeakAsync(cText, options, cancelToken: Globals.cts.Token);
                    Globals.bTextToSpeechIsBusy = false;
                }
                catch (Exception ex)
                {
#if DEBUG
                    await Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, $"{ex.Message}\n{ex.StackTrace}", CubeLang.ButtonClose_Text);
#endif
                }
            }
        }

        /// <summary>
        /// Cancel speech if a cancellation token exists and hasn't been already requested
        /// </summary>
        public static void CancelTextToSpeech()
        {
            if (Globals.bTextToSpeechIsBusy)
            {
                if (Globals.cts?.IsCancellationRequested ?? true)
                {
                    return;
                }

                Globals.cts.Cancel();
                Globals.bTextToSpeechIsBusy = false;
            }
        }

        ///// <summary>
        ///// Get the current language tag and map old language codes to new ones
        ///// </summary>
        ///// <returns></returns>
        //public static string GetCurrentLanguageTag()
        //{
        //    string languageTag = CultureInfo.CurrentCulture.Name;

        //    // Map old language codes to new ones
        //    return languageTag switch
        //    {
        //        "iw" => "he",       // Hebrew
        //        "ji" => "yi",       // Yiddish
        //        "in" => "id",       // Indonesian
        //        _ => languageTag
        //    };
        //}
    }
}
