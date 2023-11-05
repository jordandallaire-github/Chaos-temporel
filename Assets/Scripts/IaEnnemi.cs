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

    private bool hasReachedEnd = false;

    private int currentCheckpointIndex = -1; 

    public Transform[] checkpoints;



    void Start () {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.stoppingDistance = 0f; // Make the AI reach exactly its destination
        GoToNextCheckpoint();
        agent.speed = speed;
        
    }

    void Update () {
        
            // Si l'IA n'a pas atteint la fin
            if (!hasReachedEnd) {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(transform.position, checkpoints[currentCheckpointIndex % checkpoints.Length].position, NavMesh.AllAreas, path)) {
                    agent.SetPath(path);
                }
            }
        
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

       void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Checkpoint")) {
            GoToNextCheckpoint();
        }
    }

        void GoToNextCheckpoint() {
            currentCheckpointIndex++;

            if (currentCheckpointIndex < checkpoints.Length) {
                // Définir la destination de l'IA sur la position du prochain checkpoint
                agent.SetDestination(checkpoints[currentCheckpointIndex].position);
            } else {
                hasReachedEnd = true;
            }
        }
 }

