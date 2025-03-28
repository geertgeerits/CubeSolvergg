﻿using System.Diagnostics;

namespace CubeSolver
{
    internal sealed class ClassSpeech
    {
        private static IEnumerable<Locale>? locales;

        /// <summary>
        /// Initialize text to speech and fill the the array with the speech languages
        /// <para>.Country = KR ; .Id = ''  ; .Language = ko ; .Name = Korean (South Korea) ;</para>
        /// </summary>
        public static async void InitializeTextToSpeech()
        {
            // Initialize text to speech
            int nTotalItems;

            try
            {
                locales = await TextToSpeech.Default.GetLocalesAsync();

                nTotalItems = locales.Count();

                if (nTotalItems == 0)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                // Text to speech is not supported on this device
#if DEBUG
                await Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message + "\n\n" + CubeLang.TextToSpeechError_Text, CubeLang.ButtonClose_Text);
#endif
                Globals.bExplainSpeech = false;
                return;
            }

            Globals.bLanguageLocalesExist = true;

            // Put the locales in the array and sort the array
            Globals.cLanguageLocales = new string[nTotalItems];
            int nItem = 0;

            foreach (var l in locales)
            {
                Globals.cLanguageLocales[nItem] = l.Language + "-" + l.Country + " " + l.Name;
                nItem++;
            }

            Array.Sort(Globals.cLanguageLocales);
        }

        /// <summary>
        /// Search for the language after a first start or reset of the application
        /// </summary>
        /// <param name="cCultureName"></param>
        public static void SearchArrayWithSpeechLanguages(string cCultureName)
        {
            try
            {
                int nTotalItems = Globals.cLanguageLocales?.Length ?? 0;

                if (Globals.cLanguageLocales is not null)
                {
                    if (!string.IsNullOrEmpty(cCultureName))
                    {
                        // Search for the speech language as 'en-US'
                        for (int nItem = 0; nItem < nTotalItems; nItem++)
                        {
                            if (Globals.cLanguageLocales[nItem].StartsWith(cCultureName))
                            {
                                Globals.cLanguageSpeech = Globals.cLanguageLocales[nItem];
                                return;
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
                                return;
                            }
                        }
                    }
                }

                // If the language is not found use the first language in the array
                if (string.IsNullOrEmpty(Globals.cLanguageSpeech) && nTotalItems > 0)
                {
                    Globals.cLanguageSpeech = Globals.cLanguageLocales![0];
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
#endif
            }
        }

        /// <summary>
        /// Convert text to speech
        /// </summary>
        /// <param name="cTurnCubeText"></param>
        /// <returns></returns>
        public static async Task ConvertTextToSpeechAsync(string cTurnCubeText)
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
            if (cTurnCubeText is not null and not "")
            {
                Globals.bTextToSpeechIsBusy = true;

                try
                {
                    Globals.cts = new CancellationTokenSource();

                    SpeechOptions options = new()
                    {
                        Locale = locales?.Single(l => l.Language + "-" + l.Country + " " + l.Name == Globals.cLanguageSpeech)
                    };

                    await TextToSpeech.Default.SpeakAsync(cTurnCubeText, options, cancelToken: Globals.cts.Token);
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
    }
}
