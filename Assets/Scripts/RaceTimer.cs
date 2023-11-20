using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RaceTimer : MonoBehaviour
{
    private bool started = false;
    private float timeElapsed = 0;
    [SerializeField] private TextMeshProUGUI UITime;

    // Update is called once per frame
    void Update()
    {
        if(started){
            timeElapsed += Time.deltaTime;
            FormatTimer();
        }
    }

    public void StartTimer(){
        started = true;
    }
    
    public void Stoptimer(){
        started = false;
    }

    public float FetchCurrentTime(){
        return timeElapsed;
    }

    public void ResetTimer(){
        timeElapsed = 0;
    }

    private void FormatTimer(){
        float time = FetchCurrentTime();

        TimeSpan ts = TimeSpan.FromSeconds(time);

        int roundedMilliseconds = (int)Math.Floor(ts.Milliseconds / 10.0);
        UITime.text = string.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, roundedMilliseconds);

    }
}
