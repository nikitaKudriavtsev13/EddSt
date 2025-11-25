namespace EddSt
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
        
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new MainPage())) { Height=800, Width=400 };
        }
    }
}