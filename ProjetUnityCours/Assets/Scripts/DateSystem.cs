using UnityEngine;
using TMPro;
using System;
public class DateSystem : Singleton<DateSystem>
{
    public float timeMultiplier = 1f;
    public const float constMultiplier = 100;
    public TMP_Text dayText;
    public TMP_Text hoursText;

    public int day;
    public int hours;
    public int minutes;

    public float timer;

    public event Action OnDayChange;
    public event Action OnHourChange;

    void Update()
    {
        timer += Time.deltaTime;
        minutes = (int)(timer*constMultiplier*timeMultiplier);
        if (minutes >= 60)
        {
            timer = 0;
            minutes = 0;
            hours++;
            OnHourChange?.Invoke();
            hoursText.text = hours.ToString();
            if (hours >= 24)
            {
                hours = 0;
                day++;
                dayText.text = day.ToString();
                OnDayChange?.Invoke();
            }
        }
    }

    
}
