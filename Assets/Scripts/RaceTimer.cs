using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceTimer : MonoBehaviour
{
    private bool started = false;
    private float timeElapsed = 0;
    [SerializeField] private TextMesh UITime;

    // Update is called once per frame
    void Update()
    {
        if(started){
            timeElapsed += Time.deltaTime;
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
}
