using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPowerUps : MonoBehaviour
{

    public PowerUpsEffets[] powerUpsDisponibles;

    public PowerUpsEffets powerUpsActuel;
    public Voitures j1;

    public Voitures j2;

    public IaEnnemi ennemi;

    void Start()
    {
        // Choisir un power-up aléatoire
        int indexAleatoire = Random.Range(0, powerUpsDisponibles.Length);
        powerUpsActuel = powerUpsDisponibles[indexAleatoire];
        j1 = GameObject.Find("J1").GetComponent<Voitures>();
        j2 = GameObject.Find("J2").GetComponent<Voitures>();
        ennemi = GameObject.Find("CPU").GetComponent<IaEnnemi>();
    }
    private void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);

        if(col.tag == "Player1"){

            if (!j1.hasPowerUp)
            {
                // Stocker le power-up
                j1.hasPowerUp = true;
                j1.currentPowerUp = powerUpsActuel;
            }
            else
            {
                // Le joueur a déjà un power-up, détruire le power-up sans prendre l'effet
            }
        }

        if(col.tag == "Player2"){

            if (!j2.hasPowerUp)
            {
                // Stocker le power-up
                j2.hasPowerUp = true;
                j2.currentPowerUp = powerUpsActuel;
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
