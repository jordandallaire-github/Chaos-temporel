using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowRankings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] noms;
    [SerializeField] private TextMeshProUGUI[] time;
    [SerializeField] private Rankings rankings; 

    public void WriteScores(){
        for (int i = 0; i < noms.Length; i++)
        {
            noms[i].text = rankings.ranking[i].name;
        }

        for (int i = 0; i < time.Length; i++)
        {
            TimeSpan ts = TimeSpan.FromSeconds(rankings.time[i]);

            time[i].text = string.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
    }
}
