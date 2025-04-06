using System.Diagnostics;

namespace CubeSolver
{
    public sealed partial class PageSettings : ContentPage
    {
        //// Local variables
        private const string cHexCharacters = "0123456789ABCDEFabcdef";
        private readonly Stopwatch stopWatch = new();

        public PageSettings()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                DisplayAlert("InitializeComponent: PageSettings", ex.Message, "OK");
                return;
            }
#if WINDOWS
            //// Set the margins for the controls in the title bar for Windows
            lblTitlePage.Margin = new Thickness(80, 15, 0, 0);
#endif
#if ANDROID
            // !!!BUG!!! in Android - Return typ 'Done' is not working on Android
            entHexColor.ReturnType = ReturnType.Next;
#endif
#if IOS
            // Workaround for !!!BUG!!! in iOS with the Slider right margin
            Slider slider = new()
            {
                Margin = new Thickness(0, 0, 25, 0)
            };

            sldColorRed.Margin = slider.Margin;
            sldColorGreen.Margin = slider.Margin;
            sldColorBlue.Margin = slider.Margin;
#endif
            //// Put text in the chosen language in the controls and variables
            SetLanguage();

            //// Set the current language in the picker
            pckLanguage.SelectedIndex = Globals.cLanguage switch
            {
                "id" => 0,      // Bahasa Indonesia
                "cs" => 1,      // Čeština - Czech
                "da" => 2,      // Dansk - Danish
                "de" => 3,      // Deutsch - German
                "es" => 5,      // Español - Spanish
                "fr" => 6,      // Français - French
                "ko" => 7,      // 한국어 (Hangugeo) - Korean
                "hi" => 8,      // हिन्दी (Hindī)
                "it" => 9,      // Italiano - Italian
                "hu" => 10,     // Magyar - Hungarian
                "nl" => 11,     // Nederlands - Dutch
                "ja" => 12,     // 日本語 (Nihongo) - Japanese
                "nb" => 13,     // Norsk Bokmål - Norwegian Bokmål
                "pl" => 14,     // Polski - Polish
                "pt" => 15,     // Português - Portuguese
                "ro" => 16,     // Română - Romanian
                "fi" => 17,     // Suomi - Finnish
                "sv" => 18,     // Svenska - Swedish
                "uk" => 19,     // Українська (Ukraїnska) - Ukrainian
                "zh-CN" => 20,  // 中文 (Zhōngwén) - Chinese simplified
                "zh-TW" => 21,  // 中文 (Zhōngguó rén) - Chinese traditional
                _ => 4,         // English
            };

            //// Fill the picker with the speech languages and set the saved language in the picker
            FillPickerWithSpeechLanguages();

            //// Set the switches to false or true
            swtUseKociembaSolution.IsToggled = Globals.bKociembaSolution;
            swtExplainText.IsToggled = Globals.bExplainText;
            swtExplainSpeech.IsToggled = Globals.bExplainSpeech;

            //// Initialize the cube colors
            plgCubeColor1.Fill = Color.FromArgb(Globals.aFaceColors[1]);
            plgCubeColor2.Fill = Color.FromArgb(Globals.aFaceColors[2]);
            plgCubeColor3.Fill = Color.FromArgb(Globals.aFaceColors[3]);
            plgCubeColor4.Fill = Color.FromArgb(Globals.aFaceColors[4]);
            plgCubeColor5.Fill = Color.FromArgb(Globals.aFaceColors[5]);
            plgCubeColor6.Fill = Color.FromArgb(Globals.aFaceColors[6]);

            // Set the first hex colorcode in the entry field and set the slider positions
            SetCubeHexColor();

            // Start the stopWatch for resetting all the settings
            stopWatch.Start();
        }

        /// <summary>
        /// Picker language clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPickerLanguageChanged(object sender, EventArgs e)
        {
            string cLanguageOld = Globals.cLanguage;

            Picker picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                Globals.cLanguage = selectedIndex switch
                {
                    0 => "id",      // Bahasa Indonesia
                    1 => "cs",      // Čeština - Czech
                    2 => "da",      // Dansk - Danish
                    3 => "de",      // Deutsch - German
                    5 => "es",      // Español - Spanish
                    6 => "fr",      // Français - French
                    7 => "ko",      // 한국어 (Hangugeo) - Korean
                    8 => "hi",      // हिन्दी (Hindī)
                    9 => "it",      // Italiano - Italian
                    10 => "hu",     // Magyar - Hungarian
                    11 => "nl",     // Nederlands - Dutch
                    12 => "ja",     // 日本語 (Nihongo) - Japanese
                    13 => "nb",     // Norsk Bokmål - Norwegian Bokmål
                    14 => "pl",     // Polski - Polish
                    15 => "pt",     // Português - Portuguese
                    16 => "ro",     // Română - Romanian
                    17 => "fi",     // Suomi - Finnish
                    18 => "sv",     // Svenska - Swedish
                    19 => "uk",     // Українська (Ukraїnska) - Ukrainian
                    20 => "zh-CN",  // 中文 (Zhōngwén) - Chinese simplified
                    21 => "zh-TW",  // 中文 (Zhōngguó rén) - Chinese traditional
                    _ => "en",      // English
                };
            }

            if (cLanguageOld != Globals.cLanguage)
            {
                Globals.bLanguageChanged = true;

                // Set the current UI culture of the selected language
                Globals.SetCultureSelectedLanguage(Globals.cLanguage);

                // Put text in the chosen language in the controls and variables
                SetLanguage();

                // Search the new language in the cLanguageLocales array and select the new speech language
                if (Globals.cLanguageLocales is not null)
                {
                    int nTotalItems = Globals.cLanguageLocales.Length;

                    for (int nItem = 0; nItem < nTotalItems; nItem++)
                    {
                        if (Globals.cLanguageLocales[nItem].StartsWith(Globals.cLanguage))
                        {
                            pckLanguageSpeech.SelectedIndex = nItem;
                            break;
                        }
                    }

                    // Search for the indonesian speech language as 'id' and 'in'
                    // Android generating old/wrong language code for Indonesia - https://stackoverflow.com/questions/44245959/android-generating-wrong-language-code-for-indonesia
                    if (Globals.cLanguage.StartsWith("id"))
                    {
                        for (int nItem = 0; nItem < nTotalItems; nItem++)
                        {
                            if (Globals.cLanguageLocales[nItem].StartsWith("in"))
                            {
                                pckLanguageSpeech.SelectedIndex = nItem;
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Put text in the chosen language in the controls and variables
        /// </summary>
        private void SetLanguage()
        {
            List<string> ThemeList =
            [
                CubeLang.ThemeSystem_Text,
                CubeLang.ThemeLight_Text,
                CubeLang.ThemeDark_Text
            ];
            pckTheme.ItemsSource = ThemeList;

            // Set the current theme in the picker
            pckTheme.SelectedIndex = Globals.cTheme switch
            {
                "Light" => 1,       // Light
                "Dark" => 2,        // Dark
                _ => 0,             // System
            };
        }

        /// <summary>
        /// Fill the picker with the speech languages from the array
        /// </summary>
        private void FillPickerWithSpeechLanguages()
        {
            // .Country = KR ; .Id = ''  ; .Language = ko ; .Name = Korean (South Korea) ;

            // If there are no locales then return
            bool bIsSetSelectedIndex = false;

            if (!Globals.bLanguageLocalesExist)
            {
                pckLanguageSpeech.IsEnabled = false;
                return;
            }

            // Put the sorted locales from the array in the picker and select the saved language
            if (Globals.cLanguageLocales is not null)
            {
                int nTotalItems = Globals.cLanguageLocales.Length;

                // Add the locales to the picker and select the language
                for (int nItem = 0; nItem < nTotalItems; nItem++)
                {
                    pckLanguageSpeech.Items.Add(Globals.cLanguageLocales[nItem]);

                    if (Globals.cLanguageSpeech == Globals.cLanguageLocales[nItem])
                    {
                        pckLanguageSpeech.SelectedIndex = nItem;
                        bIsSetSelectedIndex = true;
                    }
                }

                // If the language was not found select the language without the region
                if (!bIsSetSelectedIndex)
                {
                    // Select the characters before the first hyphen if there is a hyphen in the string
                    string cCultureName = Globals.cLanguageSpeech.Split('-')[0];

                    for (int nItem = 0; nItem < nTotalItems; nItem++)
                    {
                        if (Globals.cLanguageLocales[nItem].StartsWith(cCultureName))
                        {
                            pckLanguageSpeech.SelectedIndex = nItem;
                            return;
                        }
                    }
                }

                // Search for the indonesian speech language as 'id' and 'in'
                // Android generating old/wrong language code for Indonesia - https://stackoverflow.com/questions/44245959/android-generating-wrong-language-code-for-indonesia
                if (Globals.cLanguage.StartsWith("id"))
                {
                    for (int nItem = 0; nItem < nTotalItems; nItem++)
                    {
                        if (Globals.cLanguageLocales[nItem].StartsWith("in"))
                        {
                            pckLanguageSpeech.SelectedIndex = nItem;
                            return;
                        }
                    }
                }
            }

            // If the language is not found set the picker to the first item
            if (!bIsSetSelectedIndex)
            {
                pckLanguageSpeech.SelectedIndex = 0;
            }

            Debug.WriteLine("FillPickerWithSpeechLanguages - Globals.cLanguageSpeech: " + Globals.cLanguageSpeech);
        }

        /// <summary>
        /// Picker speech language clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPickerLanguageSpeechChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                Globals.cLanguageSpeech = picker.Items[selectedIndex];
            }
        }

        /// <summary>
        /// Picker theme clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPickerThemeChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                Globals.cTheme = selectedIndex switch
                {
                    1 => "Light",       // Light
                    2 => "Dark",        // Dark
                    _ => "System",      // System
                };

                // Set the theme
                Globals.SetTheme();
            }
        }

        /// <summary>
        /// Switch use Kociemba solution toggled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSwtUseKociembaSolution(object sender, ToggledEventArgs e)
        {
            Globals.bKociembaSolution = swtUseKociembaSolution.IsToggled;
        }

        /// <summary>
        /// Switch explain text toggled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSwtExplainTextToggled(object sender, ToggledEventArgs e)
        {
            Globals.bExplainText = swtExplainText.IsToggled;
        }

        /// <summary>
        /// Switch explain speech toggled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSwtExplainSpeechToggled(object sender, ToggledEventArgs e)
        {
            Globals.bExplainSpeech = swtExplainSpeech.IsToggled;
        }

        /// <summary>
        /// On entry HexColor text changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryHexColorTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsHex(e.NewTextValue))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }

        /// <summary>
        /// Test if the text is a hex value
        /// </summary>
        /// <param name="cText"></param>
        /// <returns></returns>
        private static bool IsHex(string cText)
        {
            foreach (char c in cText)
            {
                if (!cHexCharacters.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Radiobutton checked changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRbnCubeColorCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            SetCubeHexColor();
        }

        /// <summary>
        /// Set the hex colorcode in the entry field and set the slider positions
        /// </summary>
        private void SetCubeHexColor()
        {
            int nRed = 0;
            int nGreen = 0;
            int nBlue = 0;

            if (rbnCubeColor1.IsChecked)
            {
                entHexColor.Text = Globals.aFaceColors[1][1..];
                HexToRgbColor(Globals.aFaceColors[1], ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor2.IsChecked)
            {
                entHexColor.Text = Globals.aFaceColors[2][1..];
                HexToRgbColor(Globals.aFaceColors[2], ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor3.IsChecked)
            {
                entHexColor.Text = Globals.aFaceColors[3][1..];
                HexToRgbColor(Globals.aFaceColors[3], ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor4.IsChecked)
            {
                entHexColor.Text = Globals.aFaceColors[4][1..];
                HexToRgbColor(Globals.aFaceColors[4], ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor5.IsChecked)
            {
                entHexColor.Text = Globals.aFaceColors[5][1..];
                HexToRgbColor(Globals.aFaceColors[5], ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor6.IsChecked)
            {
                entHexColor.Text = Globals.aFaceColors[6][1..];
                HexToRgbColor(Globals.aFaceColors[6], ref nRed, ref nGreen, ref nBlue);
            }

            sldColorRed.Value = nRed;
            sldColorGreen.Value = nGreen;
            sldColorBlue.Value = nBlue;
        }

        /// <summary>
        /// Display help for Hex color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnSettingsHexColorHelpClicked(object sender, EventArgs e)
        {
            await DisplayAlert("?", $"{CubeLang.HexColorCodes_Text}\n\n{CubeLang.AllowedChar_Text}\n{cHexCharacters}", CubeLang.ButtonClose_Text);
        }

        /// <summary>
        /// Entry HexColor Unfocused event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryHexColorUnfocused(object sender, EventArgs e)
        {
            // Length must be 6 characters
            if (entHexColor.Text.Length != 6)
            {
                _ = entHexColor.Focus();
                return;
            }

            // Set the sliders position
            int nRed = 0;
            int nGreen = 0;
            int nBlue = 0;

            if (rbnCubeColor1.IsChecked)
            {
                Globals.aFaceColors[1] = $"#{entHexColor.Text}";
                HexToRgbColor(Globals.aFaceColors[1], ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor2.IsChecked)
            {
                Globals.aFaceColors[2] = $"#{entHexColor.Text}";
                HexToRgbColor(Globals.aFaceColors[2], ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor3.IsChecked)
            {
                Globals.aFaceColors[3] = $"#{entHexColor.Text}";
                HexToRgbColor(Globals.aFaceColors[3], ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor4.IsChecked)
            {
                Globals.aFaceColors[4] = $"#{entHexColor.Text}";
                HexToRgbColor(Globals.aFaceColors[4], ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor5.IsChecked)
            {
                Globals.aFaceColors[5] = $"#{entHexColor.Text}";
                HexToRgbColor(Globals.aFaceColors[5], ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor6.IsChecked)
            {
                Globals.aFaceColors[6] = $"#{entHexColor.Text}";
                HexToRgbColor(Globals.aFaceColors[6], ref nRed, ref nGreen, ref nBlue);
            }

            sldColorRed.Value = nRed;
            sldColorGreen.Value = nGreen;
            sldColorBlue.Value = nBlue;

            entDummy.IsEnabled = true;
            _ = entDummy.Focus();
        }

        /// <summary>
        /// Hide the keyboard when the dummy entry is focused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntDummyFocused(object sender, FocusEventArgs e)
        {
            entDummy.IsEnabled = false;
        }

        /// <summary>
        /// Slider color cube value change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnSliderColorValueChanged(object sender, ValueChangedEventArgs args)
        {
            int nColorRed = 0;
            int nColorGreen = 0;
            int nColorBlue = 0;

            Slider slider = (Slider)sender;

            if (slider == sldColorRed)
            {
                nColorRed = (int)args.NewValue;
                nColorGreen = (int)sldColorGreen.Value;
                nColorBlue = (int)sldColorBlue.Value;
            }
            else if (slider == sldColorGreen)
            {
                nColorRed = (int)sldColorRed.Value;
                nColorGreen = (int)args.NewValue;
                nColorBlue = (int)sldColorBlue.Value;
            }
            else if (slider == sldColorBlue)
            {
                nColorRed = (int)sldColorRed.Value;
                nColorGreen = (int)sldColorGreen.Value;
                nColorBlue = (int)args.NewValue;
            }

            string cColorFgHex = nColorRed.ToString("X2") + nColorGreen.ToString("X2") + nColorBlue.ToString("X2");
            entHexColor.Text = cColorFgHex;

            if (rbnCubeColor1.IsChecked)
            {
                plgCubeColor1.Fill = Color.FromArgb(cColorFgHex);
                Globals.aFaceColors[1] = "#" + cColorFgHex;
            }
            else if (rbnCubeColor2.IsChecked)
            {
                plgCubeColor2.Fill = Color.FromArgb(cColorFgHex);
                Globals.aFaceColors[2] = "#" + cColorFgHex;
            }
            else if (rbnCubeColor3.IsChecked)
            {
                plgCubeColor3.Fill = Color.FromArgb(cColorFgHex);
                Globals.aFaceColors[3] = "#" + cColorFgHex;
            }
            else if (rbnCubeColor4.IsChecked)
            {
                plgCubeColor4.Fill = Color.FromArgb(cColorFgHex);
                Globals.aFaceColors[4] = "#" + cColorFgHex;
            }
            else if (rbnCubeColor5.IsChecked)
            {
                plgCubeColor5.Fill = Color.FromArgb(cColorFgHex);
                Globals.aFaceColors[5] = "#" + cColorFgHex;
            }
            else if (rbnCubeColor6.IsChecked)
            {
                plgCubeColor6.Fill = Color.FromArgb(cColorFgHex);
                Globals.aFaceColors[6] = "#" + cColorFgHex;
            }
        }

        /// <summary>
        /// Convert RRGGBB Hex color to RGB color
        /// </summary>
        /// <param name="cHexColor"></param>
        /// <param name="nRed"></param>
        /// <param name="nGreen"></param>
        /// <param name="nBlue"></param>
        private static void HexToRgbColor(string cHexColor, ref int nRed, ref int nGreen, ref int nBlue)
        {
            // Remove leading # if present
            if (cHexColor[..1] == "#")
            {
                cHexColor = cHexColor[1..];
            }

            nRed = int.Parse(cHexColor[..2], NumberStyles.AllowHexSpecifier);
            nGreen = int.Parse(cHexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            nBlue = int.Parse(cHexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
        }

        /// <summary>
        /// Button save settings clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnSettingsSaveClicked(object sender, EventArgs e)
        {
            Preferences.Default.Set("SettingTheme", Globals.cTheme);
            Preferences.Default.Set("SettingLanguage", Globals.cLanguage);
            Preferences.Default.Set("SettingLanguageSpeech", Globals.cLanguageSpeech);
            Preferences.Default.Set("SettingExplainText", Globals.bExplainText);
            Preferences.Default.Set("SettingExplainSpeech", Globals.bExplainSpeech);
            Preferences.Default.Set("SettingCubeColor1", Globals.aFaceColors[1]);
            Preferences.Default.Set("SettingCubeColor2", Globals.aFaceColors[2]);
            Preferences.Default.Set("SettingCubeColor3", Globals.aFaceColors[3]);
            Preferences.Default.Set("SettingCubeColor4", Globals.aFaceColors[4]);
            Preferences.Default.Set("SettingCubeColor5", Globals.aFaceColors[5]);
            Preferences.Default.Set("SettingCubeColor6", Globals.aFaceColors[6]);
            Preferences.Default.Set("SettingKociembaSolution", Globals.bKociembaSolution);

            // Wait 500 milliseconds otherwise the settings are not saved in Android
            Task.Delay(500).Wait();

            // Restart the application
            Application.Current!.Windows[0].Page = new AppShell();
            //Application.Current!.Windows[0].Page = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Button reset settings clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSettingsResetClicked(object sender, EventArgs e)
        {
            // Get the elapsed time in milli seconds
            stopWatch.Stop();

            if (stopWatch.ElapsedMilliseconds < 2001)
            {
                // Clear all settings after the first clicked event within the first 2 seconds after opening the setting page
                Preferences.Default.Clear();
            }
            else
            {
                // Reset some settings
                Preferences.Default.Remove("SettingTheme");
                Preferences.Default.Remove("SettingLanguage");
                Preferences.Default.Remove("SettingLanguageSpeech");
                Preferences.Default.Remove("SettingExplainText");
                Preferences.Default.Remove("SettingExplainSpeech");
                Preferences.Default.Remove("SettingCubeColor1");
                Preferences.Default.Remove("SettingCubeColor2");
                Preferences.Default.Remove("SettingCubeColor3");
                Preferences.Default.Remove("SettingCubeColor4");
                Preferences.Default.Remove("SettingCubeColor5");
                Preferences.Default.Remove("SettingCubeColor6");
                Preferences.Default.Remove("SettingKociembaSolution");
            }

            // Wait 500 milliseconds otherwise the settings are not saved in Android.
            Task.Delay(500).Wait();

            // Restart the application
            Application.Current!.Windows[0].Page = new AppShell();
            //Application.Current!.Windows[0].Page = new NavigationPage(new MainPage());
        }
    }
}