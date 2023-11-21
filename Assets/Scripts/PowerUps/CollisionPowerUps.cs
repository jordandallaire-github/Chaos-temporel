using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPowerUps : MonoBehaviour
{

    public PowerUpsEffets[] powerUpsDisponibles;

    public PowerUpsEffets powerUpsActuel;
    public SampleMessageListener messageListener1;

    public SampleMessageListener messageListener2;

    public IaEnnemi ennemi;

    void Start()
    {
        // Choisir un power-up aléatoire
        int indexAleatoire = Random.Range(0, powerUpsDisponibles.Length);
        powerUpsActuel = powerUpsDisponibles[indexAleatoire];
    }
    private void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);

        if(col.tag == "Player1"){

            if (!messageListener1.hasPowerUp)
            {
                // Stocker le power-up
                messageListener1.hasPowerUp = true;
                messageListener1.currentPowerUp = powerUpsActuel;
            }
            else
            {
                // Le joueur a déjà un power-up, détruire le power-up sans prendre l'effet
            }
        }

        if(col.tag == "Player2"){

            if (!messageListener2.hasPowerUp)
            {
                // Stocker le power-up
                messageListener2.hasPowerUp = true;
                messageListener2.currentPowerUp = powerUpsActuel;
            }
            else
            {
                // Le joueur a déjà un power-up, détruire le power-up sans prendre l'effet
            }
        }

        if(col.tag == "Adversaire"){
            
            if (!ennemi.hasPowerUp)
            {
                // Stocker le power-up
                ennemi.hasPowerUp = true;
                ennemi.currentPowerUp = powerUpsActuel;
            }
            else
            {
                
            }
        }
  


    }

}
