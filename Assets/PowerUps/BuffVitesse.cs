using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BuffVitesse")]
public class BuffVitesse : PowerUpsEffets
{

    public float vitesse;

    public override void Appliquer(GameObject cible){
        cible.GetComponent<SampleMessageListener>().speed += vitesse;
    }

    public override void Desactiver(GameObject cible)
    {
        cible.GetComponent<SampleMessageListener>().speed -= vitesse;
    }
}
