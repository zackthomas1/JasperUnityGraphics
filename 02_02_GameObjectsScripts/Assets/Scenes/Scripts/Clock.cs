using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    Transform hourPivot, minutePivot, secondPivot;


    private void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;

        float hour = (float) time.TotalHours; 
        float minute = (float) time.TotalMinutes;
        float second = (float) time.TotalSeconds;

        float hourDegree = -30.0f;
        float minuteSecondDegree = hourDegree / 5.0f;

        Debug.Log(string.Format("Current Time: {0}:{1}:{2}", hour, minute, second));

        hourPivot.localRotation = Quaternion.Euler(0,0, hourDegree * hour);
        minutePivot.localRotation = Quaternion.Euler(0,0, minuteSecondDegree * minute);
        secondPivot.localRotation = Quaternion.Euler(0, 0, minuteSecondDegree * second);

    }

}
