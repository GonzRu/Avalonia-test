using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using avalonia_test2.Analytics;
using ReactiveUI;

namespace avalonia_test2.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Hello World!";
        
        public MainWindowViewModel()
        {
            Add = ReactiveCommand.Create(AddLiveMatch);
            LiveMathes = new ReactiveList<LiveMatchViewModel>()
            {
                new LiveMatchViewModel(1, "team1-team 2"),
            };

            InitAsync();
        }

        private async void InitAsync()
        {
            await AnalyticsClient.OpenSessionAsync();
            
            Observable.Timer(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5))
                .Subscribe(SendLiveMatches);
        }

        public ReactiveList<LiveMatchViewModel> LiveMathes { get; }

        public ReactiveCommand Add { get; }

        private void AddLiveMatch()
        {
            var id = LiveMathes.Max(x => x.Id) + 1;
            LiveMathes.Add(new LiveMatchViewModel(id, $"Team{id}-Team{id}"));
        }
        
        private async void SendLiveMatches(long obj)
        {
            var liveMatches = LiveMathes.Select(x => new LiveMatch()
            {
                EventName = x.Name,
                SportName = x.Sport,
                CurrentTime = x.CurrentTime,
                EventId = x.Id,
                State = x.State,
                StartedAt = x.StartedAt,
            }).ToArray();
            await AnalyticsClient.SendMatches(liveMatches);
        }
    }
}
