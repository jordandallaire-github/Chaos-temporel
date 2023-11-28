using UnityEngine;

public class TonneauController : MonoBehaviour
{
    public float speed = 10f; // Vitesse de déplacement du tonneau

    public float forceBas = 2f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        this.rb.velocity = transform.forward * speed;
        
        GraviterBas();

    }

        private void GraviterBas(){

        this.rb.AddForce(-transform.up * forceBas * rb.velocity.magnitude);
    }
}
