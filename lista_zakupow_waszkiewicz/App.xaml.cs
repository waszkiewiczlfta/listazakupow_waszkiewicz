using ListaZakupowWaszkiewicz.Views;

namespace ListaZakupowWaszkiewicz
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
