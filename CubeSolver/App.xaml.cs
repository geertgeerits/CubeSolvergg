namespace CubeSolver
{
    public sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create a window and set the dimensions and location for desktop apps
        /// </summary>
        /// <param name="activationState"></param>
        /// <returns></returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell())
            //return new Window(new NavigationPage(new MainPage()))
            {
                X = 300,
                Y = 40,
                Height = 1000,
                Width = 900,
                MinimumHeight = 800,
                MinimumWidth = 900,
                MaximumHeight = 1200,
                MaximumWidth = 1100
            };
        }

        /// <summary>
        /// Copy the data files to the cache directory
        /// </summary>
        protected override async void OnStart()
        {
            base.OnStart();
            await ClassFileOperations.CopyDataFilesToCacheAsync();
        }
    }
}
