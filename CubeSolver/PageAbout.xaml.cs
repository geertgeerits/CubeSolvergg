namespace CubeSolver
{
    public sealed partial class PageAbout : ContentPage
    {
        public PageAbout()
        {
            try
            {
                InitializeComponent();
                BindingContext = this;
            }
            catch (Exception ex)
            {
                DisplayAlert("InitializeComponent: PageAbout", ex.Message, "OK");
                return;
            }

            //// Set the flow direction to right-to-left for Arabic
            if (Globals.cLanguage == "ar" || Globals.cLanguage == "ar-SA")
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
#if WINDOWS
            //// Set the margins for the controls in the title bar for Windows
            lblTitlePage.Margin = new Thickness(80, 15, 0, 0);
#endif
            //// Put text in the chosen language in the controls and variables
            lblVersion.Text = $"{CubeLang.Version_Text} 2.0.40";
            lblCopyright.Text = $"{CubeLang.Copyright_Text} © 1981-2025 Geert Geerits";
            lblPrivacyPolicy.Text = $"\n{CubeLang.PrivacyPolicyTitle_Text} {CubeLang.PrivacyPolicy_Text}";
            lblLicense.Text = $"\n{CubeLang.LicenseTitle_Text}: {CubeLang.License_Text}";
            lblCredits.Text = $"\n{CubeLang.InfoCredits_Text}";
            lblHelpOptionsSolveCube.Text = $"\n{CubeLang.HelpOptionsSolveCube_Text}\n{CubeLang.AverageTurnsKociemba_Text} 21, {CubeLang.AverageTurnsFridrich_Text} 60.";
            lblHelp.Text = $"\n{CubeLang.HelpCube_Text}";
            lblTextToSpeech.Text = $"\n{CubeLang.InfoTextToSpeech_Text}";
            lblLanguage.Text = $"\n{CubeLang.InfoLanguage_Text}";
            lblContact.Text = $"\n{CubeLang.InfoContact_Text}";
        }
    }

    /// <summary>
    /// Open e-mail app and open webpage (reusable hyperlink class)
    /// </summary>
    public sealed partial class HyperlinkSpan : Span
    {
        public static readonly BindableProperty UrlProperty =
            BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkSpan), null);

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public HyperlinkSpan()
        {
            FontFamily = "OpenSansRegular";
            FontAttributes = FontAttributes.Bold;
            FontSize = 16;
            TextDecorations = TextDecorations.Underline;

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                // Launcher.OpenAsync is provided by Essentials
                //Command = new Command(async () => await Launcher.OpenAsync(Url))
                Command = new Command(async () => await OpenHyperlink(Url))
            });
        }

        /// <summary>
        /// Open the e-mail program or the website link
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task OpenHyperlink(string url)
        {
            if (url.StartsWith("mailto:"))
            {
                await OpenEmailLink(url[7..]);
            }
            else
            {
                await OpenWebsiteLink(url);
            }
        }

        /// <summary>
        /// Open the e-mail program
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task OpenEmailLink(string url)
        {
            if (Email.Default.IsComposeSupported)
            {
                string subject = "Cube Solver gg";
                string body = "";
                string[] recipients = [url];

                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    BodyFormat = EmailBodyFormat.PlainText,
                    To = [.. recipients]
                };

                try
                {
                    await Email.Default.ComposeAsync(message);
                }
                catch (Exception ex)
                {
                    await Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                }
            }
        }

        /// <summary>
        /// Open the website link in the default browser
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task OpenWebsiteLink(string url)
        {
            // Check the internet connection
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType != NetworkAccess.Internet)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.InternetConnectionNo_Text, CubeLang.ButtonClose_Text);
                return;
            }

            // Open the website link in the default browser
            try
            {
                Uri uri = new(url);
                BrowserLaunchOptions options = new()
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show
                };

                await Browser.Default.OpenAsync(uri, options);
            }
            catch (Exception ex)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
            }
        }
    }
}