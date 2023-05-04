using UnityEngine;

public class TimePeriod
{
    [System.Serializable]
    public struct TimePeriodData
    {
        public string visibleName;
        public Color color;
        public Sprite scoreSprite;
        public AudioClip music;
        public GameObject map;
    }

    public Observed<bool> FinalBossIsBeaten { get; private set; }
    public TimePeriodData Data { get; set; }

    public Counter<TutorialType, bool> Tutorial;
    public Observed<int> TotalKill { get; private set; }
    public Observed<int> Score { get; private set; }
    public TimePeriod()
    {
        TotalKill = new Observed<int>();
        Score = new Observed<int>();
        FinalBossIsBeaten = new Observed<bool>();

        Tutorial = new Counter<TutorialType, bool>();
    }
}