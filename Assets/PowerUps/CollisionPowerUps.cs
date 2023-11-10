using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPowerUps : MonoBehaviour
{
    public PowerUpsEffets powerUpsEffets;

    public GameObject arduino;

    void Start(){
        arduino = arduino.GetComponent<SampleMessageListener>();
    }

    private void OntriggerEnter(Collider col){
        Destroy(gameObject);
        powerUpsEffets.Appliquer(col, arduino);
    }

}
