using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Arriver : MonoBehaviour
{
    private int index = 0;
    [SerializeField] private Rankings rankings;
    [SerializeField] private RaceTimer timer;
    [SerializeField] private GameObject[] JoueurUIs;
    [SerializeField] private GameObject[] EcransResultat;

    void OnTriggerEnter(Collider vehicule){
    if (vehicule.tag == "Player1" || vehicule.tag == "Adversaire" || vehicule.tag == "Player2")
    {
        if (System.Array.IndexOf(rankings.ranking, vehicule.gameObject) == -1)
        {
            Debug.Log(vehicule.gameObject.name + " est à la place " + (index + 1) );
            rankings.ranking[index] = vehicule.gameObject.name;
            rankings.time[index] = timer.FetchCurrentTime();
            Debug.Log(vehicule.gameObject.GetType());
            Debug.Log(rankings.ranking[index].GetType());
            index++;
        }
    }
}
}
