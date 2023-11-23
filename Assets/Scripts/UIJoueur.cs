using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIJoueur : MonoBehaviour
{
    [SerializeField] private RaceTimer Compteur;
    private TextMeshProUGUI Temps;
    // Start is called before the first frame update
    void Start()
    {
        Temps = this.transform.Find("Temps").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        AfficherCompteur();
    }

    void AfficherCompteur(){
        float time = Compteur.FetchCurrentTime();

        TimeSpan ts = TimeSpan.FromSeconds(time);

        int roundedMilliseconds = (int)Math.Floor(ts.Milliseconds / 10.0);
        Temps.text = string.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, roundedMilliseconds);
    }
}
