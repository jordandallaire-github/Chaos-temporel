using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Arriver : MonoBehaviour
{
    private int index = 0;

    [SerializeField] Configs configuration;
    [SerializeField] private Rankings rankings;
    [SerializeField] private RaceTimer timer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject[] JoueurUIs;
    [SerializeField] private GameObject[] EcransResultat;

    [SerializeField] public AudioSource audioSourceMusique;
    public UnityEvent events;


    void OnTriggerEnter(Collider vehicule){
        if (vehicule.tag == "Player1" || vehicule.tag == "Adversaire" || vehicule.tag == "Player2")
        {
            if (System.Array.IndexOf(rankings.ranking, vehicule.gameObject.name) == -1)
            {
                Debug.Log(vehicule.gameObject.name + " est à la place " + (index + 1) );
                rankings.ranking[index] = vehicule.gameObject.name;
                rankings.time[index] = timer.FetchCurrentTime();
                Debug.Log(vehicule.gameObject.GetType());
                Debug.Log(rankings.ranking[index].GetType());
                index++;

                if(IsEveryone()){
                    if(true){
                        audioSource.enabled = true;
                        audioSourceMusique.enabled = false;
                    }
                    else{
                        events.Invoke();
                    }
                }
            }
        }
    }

    bool IsEveryone(){

        int NbJoueurs = 0;

        for (int i = 0; i < configuration.playerStarted.Length; i++)
        {
            if(configuration.playerStarted[i]){
                NbJoueurs++;
            }
        }

        if(NbJoueurs == 1){
            return true;
        }else{
            if(Array.Exists(rankings.ranking, element => element == "J1" && Array.Exists(rankings.ranking, element => element == "J2"))){
                return true;
            }else{
                return false;
            }
        }
    }
}
