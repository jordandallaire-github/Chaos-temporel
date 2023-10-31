using UnityEngine;
using UnityEngine.AI;

public class IaEnnemi : MonoBehaviour {
    private NavMeshAgent agent;
    private Rigidbody rb;
    public float detectionRange = 10f; // Ajustez cette valeur en fonction de votre jeu
    public float speed = 8f; // Vitesse initiale
    public float rotationSpeed = 0.1f;
    public float maxSpeed = 10f; // Vitesse maximale
    public float brakeSpeed = 2f; // Vitesse de freinage

    private int currentCheckpointIndex = 0; 

    public Transform[] checkpoints;


    void Start () {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        GoToNextCheckpoint();
        agent.speed = speed;
    }

    void Update () {

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextCheckpoint();
        }


        // On récupère la direction vers la cible
        Vector3 directionToTarget = (checkpoints[currentCheckpointIndex].position - transform.position).normalized;
            
        // On calcule la rotation nécessaire pour faire face à la cible
        float rotationToTarget = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);
            
        // On applique la rotation à l'IA si la rotation est active
            rb.angularVelocity = new Vector3(0, rotationToTarget * rotationSpeed, 0);

        // On déplace l'IA vers la cible si le mouvement est actif
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

        void GoToNextCheckpoint()
    {
        if (currentCheckpointIndex >= checkpoints.Length)
        {
            currentCheckpointIndex = 0;
        }

        agent.SetDestination(checkpoints[currentCheckpointIndex].position);
        currentCheckpointIndex++;
    }
 }

