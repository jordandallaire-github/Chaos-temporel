using UnityEngine;

public class TonneauController : MonoBehaviour
{
    public float speed = 10f; // Vitesse de déplacement du tonneau
    public float rollForce = 10f; // Force de rotation du tonneau

    public float bounceForce = 500f; // Force de rebondissement du tonneau

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

        // Faire rouler le tonneau sur le sol
        Quaternion rollRotation = Quaternion.Euler(rb.velocity.magnitude * rollForce * Time.fixedDeltaTime, 0f, 0f);
        rb.MoveRotation(rb.rotation * rollRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Calculer la direction du rebondissement
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 direction = transform.position - contactPoint;
            direction.Normalize();

            // Appliquer la force de rebondissement
            rb.AddForce(direction * bounceForce);
        }
    }
}
