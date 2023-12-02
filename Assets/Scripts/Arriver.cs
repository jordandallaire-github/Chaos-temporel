using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Arriver : MonoBehaviour
{
    private int index = 0;

    [SerializeField] float duration = 10.0f;
    [SerializeField] Configs configuration;
    [SerializeField] private Rankings rankings;
    [SerializeField] private RaceTimer timer;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] public AudioSource audioSourceMusique;
    public UnityEvent events;
    private int NbJoueurs;

    void Start(){
        for (int i = 0; i < configuration.playerStarted.Length; i++)
        {
            if(configuration.playerStarted[i]){
                NbJoueurs++;
            }
        }
    }

    void OnTriggerEnter(Collider vehicule){
        if (vehicule.tag == "Player1" || vehicule.tag == "Adversaire" || vehicule.tag == "Player2")
        {
            if (System.Array.IndexOf(rankings.ranking, vehicule.gameObject) == -1)
            {
                rankings.ranking[index] = vehicule.gameObject;
                rankings.time[index] = timer.FetchCurrentTime();
                index++;

            }
        }

        if(vehicule.tag == "Player1" || vehicule.tag == "Player2"){
            if(IsEveryone()){
                Invoke("BackToTitleScreen", duration);
                events.Invoke();
                audioSource.enabled = true;
                audioSourceMusique.enabled = false;
            }
        }
    }

    bool IsEveryone(){

        if(NbJoueurs == 1){
            return true;
        }else if(Array.Exists(rankings.ranking, element => element.name == "J1" && Array.Exists(rankings.ranking, element => element.name == "J2"))){
                return true;
        }
        return false;
    }

    void BackToTitleScreen(){
        SceneManager.LoadScene(0);
    }
}
