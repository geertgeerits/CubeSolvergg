namespace CubeSolver
{
    public sealed partial class PageAbout : ContentPage
    {
        //// DataTrigger for the FontSize depending on the UI language
        //private string _language = Globals.cLanguage;
        //public string Language
        //{
        //    get => _language;
        //    set { _language = value; OnPropertyChanged(); }
        //}

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

            //// Set the flow direction of the text elements
            Globals.SetFlowDirection(this);

            //// Set the font properties of some controls
            lblCubeSolver.FontSize = Globals.nFontSize;

            if (lblEmail.FormattedText is FormattedString formattedEmail)
            {
                foreach (var span in formattedEmail.Spans)
                {
                    span.FontSize = Globals.nFontSize;
                    span.FontAttributes = FontAttributes.Bold;
                }
            }

            if (lblWebsite.FormattedText is FormattedString formattedWebsite)
            {
                foreach (var span in formattedWebsite.Spans)
                {
                    span.FontSize = Globals.nFontSize;
                    span.FontAttributes = FontAttributes.Bold;
                }
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
            lblHelpOptionsSolveCube.Text = $"\n{CubeLang.HelpOptionsSolveCube_Text}\n{string.Format(CubeLang.AverageTurns_Text, 21, 60)}";
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
            FontSize = 18;
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