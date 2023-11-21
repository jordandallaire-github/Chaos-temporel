using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arriver : MonoBehaviour
{
    private int index = 0;
    public Rankings rankings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider vehicule){
    if (vehicule.tag == "Player1" || vehicule.tag == "Adversaire" || vehicule.tag == "Player2")
    {
        if (System.Array.IndexOf(rankings.ranking, vehicule.gameObject) == -1)
        {
            Debug.Log(vehicule.gameObject.name + " est à la place " + (index + 1) );
            rankings.ranking[index] = vehicule.gameObject.name;
            Debug.Log(vehicule.gameObject.GetType());
            Debug.Log(rankings.ranking[index].GetType());
            index++;
        }
    }
}
}
