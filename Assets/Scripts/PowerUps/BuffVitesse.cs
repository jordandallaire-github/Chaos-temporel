using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BuffVitesse")]
public class BuffVitesse : PowerUpsEffets
{

    public float vitesse = 4f;


    public override void Appliquer(GameObject cible){
        if (cible.tag == "Player1" ){
            Voitures voitures = cible.GetComponent<Voitures>();
            voitures.maxSpeedSol += vitesse;
        }
        if (cible.tag == "Player2" ){
            Voitures voitures = cible.GetComponent<Voitures>();
            voitures.maxSpeedSol += vitesse;
        }
        if (cible.tag == "Adversaire" ){
            IaEnnemi ennemi = cible.GetComponent<IaEnnemi>();
            ennemi.maxSpeed += vitesse;
        }
    }

    public override void Desactiver(GameObject cible)
    {
        if (cible.tag == "Player1" ){
            Voitures voitures = cible.GetComponent<Voitures>();
            voitures.maxSpeedSol = 16;
        }
        if (cible.tag == "Player2" ){
            Voitures voitures = cible.GetComponent<Voitures>();
            voitures.maxSpeedSol = 16;
        }
        if (cible.tag == "Adversaire" ){
            IaEnnemi ennemi = cible.GetComponent<IaEnnemi>();
            ennemi.maxSpeed = 20;
        }
    }

}
