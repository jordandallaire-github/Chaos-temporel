using UnityEngine;
using UnityEngine.AI;

public class IaEnnemi : MonoBehaviour {
    public Transform target;
    private NavMeshAgent agent;
    private Rigidbody rb;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.destination = target.position;
    }

    void Update () {
        if (!agent.pathPending && agent.remainingDistance < 0.5f) {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                return;
            }
            agent.destination = target.position;
        }

    }
}