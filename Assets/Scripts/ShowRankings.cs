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

        CopyToScreen1();
    }

    private void CopyToScreen1(){
        // Clone the GameObject this script is attached to
        GameObject clonedObject = Instantiate(this.gameObject);
        clonedObject.name = "EcranResultat02";

        // Get the Canvas component from the cloned object
        Canvas canvas = clonedObject.GetComponent<Canvas>();

        // Check if the Canvas component exists
        if (canvas != null)
        {
            // Set the target display of the cloned canvas to display 2
            canvas.targetDisplay = 1; // Display indices are 0-based
        }
        else
        {
            Debug.LogError("No Canvas component found in the cloned object.");
        }
    }
}
