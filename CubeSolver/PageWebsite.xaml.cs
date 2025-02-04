namespace CubeSolver
{
    public partial class PageWebsite : ContentPage
    {
        public PageWebsite()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                DisplayAlert("InitializeComponent: PageWebsite", ex.Message, "OK");
                return;
            }

            //// Set WebView properties
            wvWebpage.Source = "https://geertgeerits.wixsite.com/geertgeerits/rubiks-cube";
            wvWebpage.Navigating += OnNavigating;
            wvWebpage.Navigated += OnNavigated;
        }

        /// <summary>
        /// Navigating event that's raised when page navigation starts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            // If 'mailto' link in webpage then open the e-mail app
            if (e.Url.StartsWith("mailto"))
            {
                await Launcher.TryOpenAsync(e.Url);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Navigated event that's raised when page navigation completes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            // Enable or disable the back and forward buttons
            btnGoBack.IsEnabled = wvWebpage.CanGoBack;
            btnGoForward.IsEnabled = wvWebpage.CanGoForward;

            // Changes the target of all the links in _self
            string result = "";

            try
            {
                result = await wvWebpage.EvaluateJavaScriptAsync(@"(function() {
                var links = document.getElementsByTagName('a');
                for (var i = 0; i < links.length; i++)
                {
                    links[i].setAttribute('target', '_self');
                }
            })()");
            }
            catch (Exception ex)
            {
#if DEBUG
                await DisplayAlert($"PageWebsite, OnNavigated, result = {result}", ex.Message, "OK");
#endif        
            }
        }

        /// <summary>
        /// Go backwards, if allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGoBackClicked(object sender, EventArgs e)
        {
            if (wvWebpage.CanGoBack)
            {
                wvWebpage.GoBack();
            }
        }

        /// <summary>
        /// Go forwards, if allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGoForwardClicked(object sender, EventArgs e)
        {
            if (wvWebpage.CanGoForward)
            {
                wvWebpage.GoForward();
            }
        }
    }
}