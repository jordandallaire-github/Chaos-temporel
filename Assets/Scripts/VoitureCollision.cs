using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoitureCollision : MonoBehaviour
{
    public float repoussement = 3000;

    private AudioSource audioSource;
    
    public bool isOnGround;
    public bool isOnGrass;
    public float rotationSpeed = 360f; // Vitesse de rotation en degrés par seconde
    public float jumpHeight = 1f; // Hauteur du saut en unités
    public float jumpDuration = 0.1f; // Durée du saut en secondes



    // Start is called before the first frame update
    void Start()
    {
       if(this.gameObject.tag == "Player1" || this.gameObject.tag == "Player2" ){
            audioSource = this.gameObject.GetComponent<AudioSource>();
       }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.tag == "Player1")
        {
            if(collision.gameObject.tag == "Adversaire" || collision.gameObject.tag == "Player2"){
                audioSource.Play();
                Rigidbody autreRb = collision.rigidbody;
                Vector3 explosionPosition = collision.contacts[0].point;
                autreRb.AddExplosionForce(repoussement, explosionPosition, 100.0f);
            }

        }

        if (this.gameObject.tag == "Player2")
        {
            if(collision.gameObject.tag == "Adversaire" || collision.gameObject.tag == "Player1"){
                audioSource.Play();
                Rigidbody autreRb = collision.rigidbody;
                Vector3 explosionPosition = collision.contacts[0].point;
                autreRb.AddExplosionForce(repoussement, explosionPosition, 100.0f);
            }

        }

        if (collision.gameObject.tag == "Barrel")
        {
            StartCoroutine(BarrelCollisionReaction());
            collision.gameObject.SetActive(false);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Sol"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Gazon"))
        {
            isOnGrass = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Sol"))
        {
            isOnGround = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Gazon"))
        {
            isOnGrass = false;
        }

    }

    IEnumerator BarrelCollisionReaction()
    {
        float elapsedTime = 0f;
        float originalY = transform.position.y;

        while (elapsedTime < jumpDuration)
        {
            // Faire tourner le joueur
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

            // Faire sauter le joueur
            float newY = originalY + jumpHeight * Mathf.Sin((elapsedTime / jumpDuration) * Mathf.PI);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }


}