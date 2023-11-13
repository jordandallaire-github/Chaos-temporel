using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPowerUps : MonoBehaviour
{

    public PowerUpsEffets[] powerUpsDisponibles;

    public PowerUpsEffets powerUpsActuel;
    public SampleMessageListener messageListener;

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

        if(col.tag == "Player"){

            if (!messageListener.hasPowerUp)
            {
                // Stocker le power-up
                messageListener.hasPowerUp = true;
                messageListener.currentPowerUp = powerUpsActuel;
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
