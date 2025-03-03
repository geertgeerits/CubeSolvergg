namespace CubeSolver
{
    public sealed partial class PageAbout : ContentPage
    {
        public PageAbout()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                DisplayAlert("InitializeComponent: PageAbout", ex.Message, "OK");
                return;
            }
#if WINDOWS
            //// Set the margins for the controls in the title bar for Windows
            lblTitlePage.Margin = new Thickness(80, 15, 0, 0);
#endif
            //// Put text in the chosen language in the controls and variables
            lblVersion.Text = $"{CubeLang.Version_Text} 2.0.36";
            lblCopyright.Text = $"{CubeLang.Copyright_Text} © 1981-2025 Geert Geerits";
            lblEmail.Text = $"{CubeLang.Email_Text} geertgeerits@gmail.com";
            lblWebsite.Text = $"{CubeLang.Website_Text}: ../cube-solver";
            lblPrivacyPolicy.Text = $"\n{CubeLang.PrivacyPolicyTitle_Text} {CubeLang.PrivacyPolicy_Text}";
            lblLicense.Text = $"\n{CubeLang.LicenseTitle_Text}: {CubeLang.License_Text}";
            lblCredits.Text = $"\n{CubeLang.InfoCredits_Text}";
            lblHelpOptionsSolveCube.Text = $"\n{CubeLang.HelpOptionsSolveCube_Text}\n{CubeLang.AverageTurnsKociemba_Text} 21, {CubeLang.AverageTurnsFridrich_Text} 60.";
            lblHelp.Text = $"\n{CubeLang.HelpCube_Text}";
            lblExplanation.Text = $"\n{CubeLang.InfoExplanation_Text}";
        }

        /// <summary>
        /// Open e-mail program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnBtnEmailLinkClicked(object sender, EventArgs e)
        {
            if (Email.Default.IsComposeSupported)
            {
                string subject = "Cube Solver gg";
                string body = "";
                string[] recipients = ["geertgeerits@gmail.com"];

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
                    await DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                }
            }
        }

        /// <summary>
        /// Open the website link in the default browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnBtnWebsiteLinkClicked(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new("https://geertgeerits.wixsite.com/geertgeerits/cube-solver");
                BrowserLaunchOptions options = new()
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show
                };

                await Browser.Default.OpenAsync(uri, options);
            }
            catch (Exception ex)
            {
                await DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
            }
        }
    }
}