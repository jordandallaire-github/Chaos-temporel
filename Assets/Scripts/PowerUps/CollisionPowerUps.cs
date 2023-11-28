using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPowerUps : MonoBehaviour
{

    public PowerUpsEffets[] powerUpsDisponibles;

    public PowerUpsEffets powerUpsActuel;
    public Voitures j1;

    public Voitures j2;

    private IaEnnemi ennemi;

    void Start()
    {
        // Choisir un power-up aléatoire
        int indexAleatoire = Random.Range(0, powerUpsDisponibles.Length);
        powerUpsActuel = powerUpsDisponibles[indexAleatoire];

        if(GameObject.Find("J1") != null ){
            j1 = GameObject.Find("J1").GetComponent<Voitures>();
        }
         if(GameObject.Find("J2") != null ){
            j2 = GameObject.Find("J2").GetComponent<Voitures>();
         }
    }
    private void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);

 
        if(col.tag == "Player1"){

               if(GameObject.Find("J1") != null ){
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

        }

        if(col.tag == "Player2"){

         if(GameObject.Find("J2") != null ){
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

        }

        if(col.tag == "Adversaire"){
             ennemi = col.gameObject.GetComponent<IaEnnemi>();
            if (ennemi != null && !ennemi.hasPowerUp)
            {
                // Stocker le power-up
                ennemi.hasPowerUp = true;
                ennemi.currentPowerUp = powerUpsActuel;
            }
        }
  


    }

}
