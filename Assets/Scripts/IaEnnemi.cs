using UnityEngine;
using UnityEngine.AI;

public class IaEnnemi : MonoBehaviour {
    public Transform target;
    private NavMeshAgent agent;
    private Rigidbody rb;
    public float detectionRange = 10f; // Ajustez cette valeur en fonction de votre jeu
    public float speed = 8f; // Vitesse initiale
    public float rotationSpeed = 0.1f;
    public float maxSpeed = 10f; // Vitesse maximale
    public float brakeSpeed = 2f; // Vitesse de freinage

    private bool isMoving = false;
    private bool isRotating = false;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.destination = target.position;
        agent.speed = speed;
    }

    void Update () {
        agent.destination = target.position;

        // Détecter les obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange)) {
            if (hit.collider.gameObject.tag == "Obstacle") {
                // Si un obstacle est détecté, activer le mouvement et désactiver la rotation
                isMoving = true;
                isRotating = false;
            } else {
                // Si aucun obstacle n'est détecté, désactiver le mouvement et activer la rotation
                isMoving = false;
                isRotating = true;
            }
        }

        // On récupère la direction vers la cible
        Vector3 directionToTarget = (target.position - transform.position).normalized;
            
        // On calcule la rotation nécessaire pour faire face à la cible
        float rotationToTarget = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);
            
        // On applique la rotation à l'IA si la rotation est active
        if (isRotating) {
            rb.angularVelocity = new Vector3(0, rotationToTarget * rotationSpeed, 0);
        }

        // On déplace l'IA vers la cible si le mouvement est actif
        if (isMoving) {
            rb.AddForce(directionToTarget * speed, ForceMode.VelocityChange);

            // Limite la vitesse maximale
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            // Applique une force de freinage lorsque la valeur de mouvement est proche de zéro
            if (Mathf.Abs(rb.velocity.magnitude) < 0.1f)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeSpeed * Time.deltaTime);
            }
        }
    }
}
