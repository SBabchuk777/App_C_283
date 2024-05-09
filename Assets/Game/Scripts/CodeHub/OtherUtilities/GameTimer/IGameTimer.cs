using System;

namespace CodeHub.OtherUtilities
{
    public interface IGameTimer
    {
        event Action OnEndTime;
        float SecondsFromStart { get; }
        void Initialize(int seconds);
        string GetStringVisualizeTime(float time);
        void PauseTimer();
        void StartTimer();
    }
}