using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Windows.Input;

namespace Assessment
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> RealTimeLog { get; set; } = new();

        private readonly HttpClient _http = new();

        private string _status = string.Empty;
        private Timer _timer;
        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }
        public ICommand SendCommand { get; }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            _timer = new System.Threading.Timer(async _ => await PollLogAsync(),
                null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

            SendCommand = new Command(async () => await SendFeedbackAsync());
        }

        private async Task PollLogAsync()
        {
            try
            {
                var Response = await _http.GetStringAsync("TODO");
                MainThread.BeginInvokeOnMainThread(() => RealTimeLog.Add(Response));
            }
            catch (Exception e)
            {
                MainThread.BeginInvokeOnMainThread(() => RealTimeLog.Add($"ERR: {e.Message}"));
            }
        }

        private async Task SendFeedbackAsync()
        {
            var text = FeedbackEntry.Text?.Trim();
            if (string.IsNullOrEmpty(text)) return;

            var payload = new
            {
                message = text,
                timestamp = DateTime.UtcNow
            };

            try
            {
                var response = await _http.PostAsJsonAsync("TODO", payload);
                Status = response.IsSuccessStatusCode ? "sent" : $"ERR: {response.StatusCode}";
                if (response.IsSuccessStatusCode) FeedbackEntry.Text = string.Empty;
            }
            catch (Exception e)
            {
                Status = $"Failed: {e.Message}";
            }

            OnPropertyChanged(nameof(Status));
        }
    }
}
