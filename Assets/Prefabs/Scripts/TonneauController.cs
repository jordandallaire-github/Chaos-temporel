using UnityEngine;

public class TonneauController : MonoBehaviour
{
    public float speed = 10f; // Vitesse de déplacement du tonneau
    public float bounceForce = 10f; // Force de rebond sur les murs

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Déplacer le tonneau vers l'avant en local
        Vector3 localForward = transform.localRotation * Vector3.right;
        Vector3 localPosition = transform.localPosition + localForward * speed * Time.fixedDeltaTime;
        rb.MovePosition(localPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Rebondir sur les murs
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 reflection = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
            rb.velocity = reflection.normalized * bounceForce;
        }
    }
}
