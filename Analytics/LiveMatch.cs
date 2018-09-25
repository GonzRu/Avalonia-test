using System;

namespace Avalonia_test.Analytics
{
    public class LiveMatch
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public string SportName { get; set; }

        public DateTime StartedAt { get; set; }

        public bool LastFbRobotSaveSuccess { get; set; }

        public DateTime LastFbRobotSavedAt { get; set; }

        public string CurrentTime { get; set; }

        public string CurrentScore { get; set; }

        public LiveMatchState State { get; set; }

        public bool ShowOnSite { get; set; }

        public bool IsBlocked { get; set; }

        public int? TranslationId { get; set; }

        public string Properties { get; set; }
    }
}