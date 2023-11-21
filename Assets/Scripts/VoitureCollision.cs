using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoitureCollision : MonoBehaviour
{
    public float repoussement = 3000;
    public bool isOnGround;
    public bool isOnGrass;
    public float rotationSpeed = 360f; // Vitesse de rotation en degrés par seconde
    public float jumpHeight = 1f; // Hauteur du saut en unités
    public float jumpDuration = 0.1f; // Durée du saut en secondes

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

        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            Rigidbody autreRb = collision.rigidbody;
            autreRb.AddExplosionForce(repoussement, collision.contacts[0].point, 5);
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

        // Assurez-vous que le joueur revient à sa position Y originale à la fin
        transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
    }


}