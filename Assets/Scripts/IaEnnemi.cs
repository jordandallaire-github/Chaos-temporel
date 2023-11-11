using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class IaEnnemi : MonoBehaviour {
    private NavMeshAgent agent;
    private Rigidbody rb;
    public PowerUpsEffets currentPowerUp;
    public bool hasPowerUp = false;
    public float detectionRange = 10f; // Ajustez cette valeur en fonction de votre jeu
    public float speed = 15f; // Vitesse initiale
    private float originalSpeed = 15f;
    public float rotationSpeed = 0.1f;
    public float maxSpeed = 20f; // Vitesse maximale
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

            if (hasPowerUp)
            {
                // Vérifier si le power-up est en cooldown
                 if (currentPowerUp != null )
                {
                    // Activer le power-up actuel sur l'objet cible
                    currentPowerUp.Appliquer(this.gameObject);

                    if (speed >= maxSpeed){
                         speed = maxSpeed;
                    }
                    // Lancer la coroutine pour désactiver le power-up après la durée spécifiée
                    StartCoroutine(DesactiverPowerUp());
                }
                else
                {
                    // Le power-up est en cooldown, ne rien faire
                }
                }
            else
            {
                    // Le joueur n'a pas de power-up actuellement
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

        IEnumerator DesactiverPowerUp()
        {
            yield return new WaitForSeconds(currentPowerUp.cooldown);
            
            // Vérifier si currentPowerUp et powerUpTarget sont définis avant de les utiliser
            if (currentPowerUp != null && this.gameObject != null)
            {
                // Désactiver le power-up
                currentPowerUp.Desactiver(this.gameObject);

                speed = originalSpeed;
                // Réinitialiser l'état du power-up
                hasPowerUp = false;
                currentPowerUp = null;
            }
        }

 }

