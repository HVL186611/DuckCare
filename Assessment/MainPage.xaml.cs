using System.Collections.ObjectModel;
using DuckLib;

namespace Assessment
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> RealTimeLog { get; set; } = new();
        private Timer _timer;
        private string _status = string.Empty;

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        private int _activeCaseId = -1;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            var activeCase = DuckAPI.GetSimulationCases().FirstOrDefault(c => c.IsActive == 1);
            if (activeCase != null) _activeCaseId = activeCase.Id;

            _timer = new System.Threading.Timer(_ => PollLog(),
                null, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(3));
        }

        private void PollLog()
        {
            if (_activeCaseId == -1) return;
            try
            {
                var logs = DuckAPI.GetCaseLogs(_activeCaseId);
                var lines = logs.Select(l => l.Text ?? "").ToList();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    RealTimeLog.Clear();
                    foreach (var line in lines)
                        RealTimeLog.Add(line);
                });
            }
            catch (Exception e)
            {
                MainThread.BeginInvokeOnMainThread(() => Status = $"Poll ERR: {e.Message}");
            }
        }

        private void OnSendClicked(object sender, EventArgs e)
        {
            var text = FeedbackEntry.Text?.Trim();
            if (string.IsNullOrEmpty(text)) { Status = "No text entered"; return; }
            if (_activeCaseId == -1) { Status = "No active case found"; return; }

            Status = "Sending...";
            try
            {
                DuckAPI.AddFeedback(_activeCaseId, text);
                Status = "Sent";
                FeedbackEntry.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Status = $"Failed: {ex.Message}";
            }
        }
    }
}
