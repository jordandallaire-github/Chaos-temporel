using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpsEffets : ScriptableObject
{

    public float cooldown = 0f; // Durée du cooldown en secondes

    public abstract void Appliquer(GameObject cible);
    public abstract void Desactiver(GameObject cible);

}
