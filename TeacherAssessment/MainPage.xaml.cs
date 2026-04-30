namespace TeacherAssessment
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartClicked(object? sender, EventArgs e) //Gjør til async
        {
            //String name = nameInput.Text;
            //Console.WriteLine("Simulation Started");

            await Navigation.PushAsync(new Simulation()); //Redirect til Simulation.xaml
            /**
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            */
        }
    }
}
