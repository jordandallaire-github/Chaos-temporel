using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoitureJoueur : MonoBehaviour
{

    public float repoussement = 3000;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.tag == "Player")
        {

            Rigidbody autreRb = collision.rigidbody;

            autreRb.AddExplosionForce(repoussement, collision.contacts[0].point, 5);
        
        }
    }

}
