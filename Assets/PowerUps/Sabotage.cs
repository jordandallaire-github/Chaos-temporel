using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Sabotage")]
public class Sabotage : PowerUpsEffets
{


    public override void Appliquer(GameObject cible)
    {
    
    }

    public override void Desactiver(GameObject cible)
    {
       cible.SetActive(false);
    }
}
