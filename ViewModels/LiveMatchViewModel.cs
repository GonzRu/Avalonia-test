using System;
using System.Reactive.Linq;
using avalonia_test2.Analytics;
using ReactiveUI;

namespace avalonia_test2.ViewModels
{
    public partial class MainWindowViewModel
    {
        public class LiveMatchViewModel : ViewModelBase
        {
            private string _currentTime;

            public LiveMatchViewModel(int id, string name)
            {
                Id = id;
                Name = name;
                NextState = ReactiveCommand.Create(SetNextState);
            }

            public int Id { get; }

            public string Name { get; }

            public string Sport { get; } = "Rugby";
            
            public DateTime StartedAt { get; } = DateTime.UtcNow;

            public LiveMatchState State { get; private set; }

            public string CurrentTime
            {
                get => _currentTime;
                private set => this.RaiseAndSetIfChanged(ref _currentTime, value);
            }

            public string CurrentScore { get; private set; }

            public ReactiveCommand NextState { get; }

            private IDisposable _timerDisposable;

            private void SetNextState()
            {
                State = State == LiveMatchState.NotStarted
                    ? LiveMatchState.Live
                    : LiveMatchState.End;

                if (State == LiveMatchState.Live)
                {
                    _timerDisposable = Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
                        .Subscribe(l =>
                        {
                            var currentTime = TimeSpan.FromSeconds(l);
                            var minutes = (int) currentTime.TotalMinutes;
                            var seconds = (int) currentTime.Seconds;
                            CurrentTime = $"{minutes:D2}:{seconds:D2}";
                        });
                }

                if (State == LiveMatchState.End)
                {
                    _timerDisposable.Dispose();
                }
            }
        }
    }
}