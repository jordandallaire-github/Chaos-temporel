using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Depart : MonoBehaviour
{
    [SerializeField] private GameObject[] vehicules;
    private UnityEngine.Vector3[] positions;



    void Start()
    {

        // Place les véhicules sur les positions de départ
        positions = new UnityEngine.Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            UnityEngine.Vector3 position = transform.GetChild(i).position;
            positions[i] = position;
        }

        System.Random rand = new System.Random();
        List<int> usedIndices = new List<int>();

        foreach (GameObject vehicule in vehicules)
        {
            int index;
            do
            {
                index = rand.Next(positions.Length);
            } while (usedIndices.Contains(index));

            usedIndices.Add(index);
            vehicule.transform.position = positions[index];

            IaEnnemi iaEnnemi = vehicule.GetComponent<IaEnnemi>();
            if (iaEnnemi != null)
            {
                iaEnnemi.enabled = false;
            }

            UnityEngine.AI.NavMeshAgent navMeshAgent = vehicule.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }
        }

        // Désactiver le script SerialController
        GameObject serialControllerObject = GameObject.Find("SerialController");
        SerialController serialController = serialControllerObject.GetComponent<SerialController>();
        serialController.enabled = false;

        // Désactiver le script SampleMessageListener
        GameObject sampleMessageListenerObject = GameObject.Find("Arduino");
        SampleMessageListener sampleMessageListener = sampleMessageListenerObject.GetComponent<SampleMessageListener>();
        sampleMessageListener.enabled = false;

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {

        yield return new WaitForSeconds(3);

        // Réactive les mouvements des joueurs après 3 secondes
        foreach (GameObject vehicule in vehicules)
        {
            IaEnnemi iaEnnemi = vehicule.GetComponent<IaEnnemi>();
            if (iaEnnemi != null)
            {
                iaEnnemi.enabled = true;
            }

            UnityEngine.AI.NavMeshAgent navMeshAgent = vehicule.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = true;
            }
        }

            // Active le script SerialController
            GameObject serialControllerObject = GameObject.Find("SerialController");
            SerialController serialController = serialControllerObject.GetComponent<SerialController>();
            serialController.enabled = true;

            // Active le script SampleMessageListener
            GameObject sampleMessageListenerObject = GameObject.Find("Arduino");
            SampleMessageListener sampleMessageListener = sampleMessageListenerObject.GetComponent<SampleMessageListener>();
            sampleMessageListener.enabled = true;
            

    }
}
