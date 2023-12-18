using UnityEngine;

public class TonneauController : MonoBehaviour
{
    public float speed = 10f; // Vitesse de déplacement du tonneau

    private Rigidbody rb;

    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        direction = transform.forward;
    }

    private void Update()
    {
        this.rb.velocity = direction * speed; 
        
        GraviterBas();
    }

    private void GraviterBas()
    {
        this.rb.AddForce(-transform.up * 0.81f);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Change direction on collision with a wall
        if (collision.gameObject.tag == "Wall")
        {
            direction = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal); // Change la direction
        }
    }

}
