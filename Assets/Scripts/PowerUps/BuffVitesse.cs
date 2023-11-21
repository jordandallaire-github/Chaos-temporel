using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BuffVitesse")]
public class BuffVitesse : PowerUpsEffets
{

    public float vitesse = 4f;


    public override void Appliquer(GameObject cible){
        if (cible.tag == "Arduino1" ){
            SampleMessageListener listener = cible.GetComponent<SampleMessageListener>();
            listener.maxSpeedSol += vitesse;
        }
        if (cible.tag == "Arduino2" ){
            SampleMessageListener listener = cible.GetComponent<SampleMessageListener>();
            listener.maxSpeedSol += vitesse;
        }
        if (cible.tag == "Adversaire" ){
            IaEnnemi ennemi = cible.GetComponent<IaEnnemi>();
            ennemi.maxSpeed += vitesse;
        }
    }

    public override void Desactiver(GameObject cible)
    {
        if (cible.tag == "Arduino1" ){
            SampleMessageListener listener = cible.GetComponent<SampleMessageListener>();
            listener.maxSpeedSol = 12;
        }
        if (cible.tag == "Arduino2" ){
            SampleMessageListener listener = cible.GetComponent<SampleMessageListener>();
            listener.maxSpeedSol = 12;
        }
        if (cible.tag == "Adversaire" ){
            IaEnnemi ennemi = cible.GetComponent<IaEnnemi>();
            ennemi.maxSpeed = 20;
        }
    }

}
