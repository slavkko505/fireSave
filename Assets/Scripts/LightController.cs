using System;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private float TimeMultiplier;
    [SerializeField] private float startHour;

    [SerializeField] private Light sunLight;
    
    [SerializeField] private float suniseHour;    
    [SerializeField] private float sunsetHour;

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;
    private DateTime currentTime;

    public bool TimeNowDay = true;

    private void Start()
    {
        currentTime = new DateTime() + TimeSpan.FromHours(startHour);
        
        sunriseTime = TimeSpan.FromHours(suniseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    private void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * TimeMultiplier);
    }

    void RotateSun()
    {
        float sunLightRotate;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunset = CalculateTimeDIfference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrice = CalculateTimeDIfference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrice.TotalMinutes / sunriseToSunset.TotalMinutes;
            
            sunLightRotate = Mathf.Lerp(0, 180, (float)percentage);
            
            //day Scellet
            if (!TimeNowDay)
            {
                TimeNowDay = !TimeNowDay;
            }
        }
        else
        {
            TimeSpan sunsetToSunrice = CalculateTimeDIfference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDIfference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunrice.TotalMinutes;

            sunLightRotate = Mathf.Lerp(180, 360, (float)percentage);
            
            //night Scellet
            if (TimeNowDay)
            {
                TimeNowDay = !TimeNowDay;
            }
        }
        
        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotate, Vector3.right);
        
    }
    TimeSpan CalculateTimeDIfference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    public void SetTimeMultiplier(float timeMult)
    {
        TimeMultiplier = timeMult;
    }
}
