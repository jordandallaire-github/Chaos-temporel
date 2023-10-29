using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arriver : MonoBehaviour
{
    private int index = 0;
    private GameObject[] ranking;
    // Start is called before the first frame update
    void Start()
    {
        ranking = new GameObject[4];
        
    }

    void OnTriggerEnter(Collider vehicule){
        if (vehicule.tag == "Player" || vehicule.tag == "Adversaire")
        {
            Debug.Log(vehicule + " est à la place " + (index + 1) );
            ranking[index] = vehicule.gameObject;
            index++;
        }
    }
}
