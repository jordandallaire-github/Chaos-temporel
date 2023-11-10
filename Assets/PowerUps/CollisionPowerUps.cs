using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPowerUps : MonoBehaviour
{
    public PowerUpsEffets powerUpsEffets;
    public SampleMessageListener messageListener;

    public GameObject arduino;

    private void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);

        if (!messageListener.hasPowerUp)
        {
            // Stocker le power-up
            messageListener.hasPowerUp = true;
            messageListener.currentPowerUp = powerUpsEffets;
            messageListener.arduino = arduino;
        }
        else
        {
            // Le joueur a déjà un power-up, détruire le power-up sans prendre l'effet
        }
    }

}
