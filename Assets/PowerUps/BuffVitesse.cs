using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BuffVitesse")]
public class BuffVitesse : PowerUpsEffets
{

    public float vitesse = 0.2f;

    private float originalSpeedPlayer;

    private float originalSpeedAdversaire;

    public override void Appliquer(GameObject cible){
        if (cible.tag == "Arduino" ){
            SampleMessageListener listener = cible.GetComponent<SampleMessageListener>();
            originalSpeedPlayer = listener.speed;
            listener.speed += vitesse/listener.speed;
        }
        if (cible.tag == "Adversaire" ){
            IaEnnemi ennemi = cible.GetComponent<IaEnnemi>();
            originalSpeedAdversaire = ennemi.speed;
            ennemi.speed += vitesse;
        }
    }

    public override void Desactiver(GameObject cible)
    {
        if (cible.tag == "Arduino" ){
            SampleMessageListener listener = cible.GetComponent<SampleMessageListener>();
            listener.speed = originalSpeedPlayer;
        }
        if (cible.tag == "Adversaire" ){
            IaEnnemi ennemi = cible.GetComponent<IaEnnemi>();
            ennemi.speed = originalSpeedAdversaire;
        }
    }

}
