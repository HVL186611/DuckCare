using Microsoft.Extensions.DependencyInjection;

namespace TeacherAssessment
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new MainPage())); //Returnerer et nytt Window Objekt som blir et nytt NavigationPage for en ny MainPage
            //Når du klikker knapp på hoved
        }
    }
}