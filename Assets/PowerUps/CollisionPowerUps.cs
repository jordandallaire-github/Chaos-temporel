using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPowerUps : MonoBehaviour
{
    public PowerUpsEffets powerUpsEffets;
    public SampleMessageListener messageListener;

    public IaEnnemi ennemi;


    private void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);

        if(col.tag == "Player"){

            if (!messageListener.hasPowerUp)
            {
                // Stocker le power-up
                messageListener.hasPowerUp = true;
                messageListener.currentPowerUp = powerUpsEffets;
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
                ennemi.currentPowerUp = powerUpsEffets;
            }
            else
            {
                
            }
        }
  


    }

}
