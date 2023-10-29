using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoitureJoueur : MonoBehaviour
{

    public float repoussement = 3000;

    public bool isOnGround;


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

        if (collision.gameObject.tag == "Adversaire")
            {

                Rigidbody autreRb = collision.rigidbody;

                autreRb.AddExplosionForce(repoussement, collision.contacts[0].point, 5);
            
            }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Sol"))
        {
            isOnGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Sol"))
        {
            isOnGround = false;
        }
    }

}
