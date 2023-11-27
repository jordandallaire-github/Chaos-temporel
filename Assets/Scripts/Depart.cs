using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Cinemachine;
using UnityEngine;

public class Depart : MonoBehaviour
{
    [SerializeField] private GameObject[] vehicules = new GameObject[4] {null, null, null, null};
    [SerializeField] private GameObject[] PrefabAdversaires;
    [SerializeField] private RaceTimer globalTimer;
    [SerializeField] private SampleMessageListener arduino01;
    [SerializeField] private SampleMessageListener arduino02;
    [SerializeField] private SerialController sc1;
    [SerializeField] private SerialController sc2;
    [SerializeField] private Configs configurations;
    [SerializeField] private CinemachineVirtualCamera CameraJ1;
    [SerializeField] private CinemachineVirtualCamera CameraJ2;

    [SerializeField] private AudioSource audioSourceIntro;
    [SerializeField] public AudioSource audioSourceMusique;

    [SerializeField] private Voitures[] Joueurs;
    private UnityEngine.Vector3[] positions;



    void Start()
    {
        // Instancie les joueurs et les CPUs
        InstancierVehicules();

        audioSourceIntro.Play();

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
        
        sc1.portName = configurations.J1Port;
        sc1.enabled = false;
        sc2.portName = configurations.J2Port;
        sc2.enabled = false;

        // Désactiver le script SampleMessageListener
        
        arduino01.enabled = false;
        arduino02.enabled = false;

        audioSourceMusique.enabled = false;
  

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {

        yield return new WaitForSeconds(4);

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
            sc1.enabled = true;
            sc2.enabled = true;

            // Active le script SampleMessageListener
            arduino01.enabled = true;
            arduino02.enabled = true;

            // Active le contrôle du joueur
            for (int i = 0; i < Joueurs.Length; i++)
            {
                Joueurs[i].controlsEnabled = true;
            }

            // Active le timer de la course
            globalTimer.StartTimer();

            audioSourceMusique.enabled = true;


    }

    // Instancier les joueurs et les CPU
    void InstancierVehicules(){
        int index = 0;
        int NbCPUs = PrefabAdversaires.Length;
        int NbPlayers = 0;

        for (int i = 0; i < configurations.playerStarted.Length; i++)
        {
            if (configurations.playerStarted[i]){
                NbPlayers++;
            }
        }

        Joueurs = new Voitures[NbPlayers];

        // Instanciate the players
        int joueurIndex = 0;
        for (int i = 0; i < NbPlayers; i++)
        {
            GameObject vehiculeJoueur;
            if(configurations.playerStarted[i]){
                if(i == 0){
                    vehiculeJoueur = Instantiate(configurations.J1VehiculeChoisi);
                    vehiculeJoueur.name = "J1";
                    CameraJ1.Follow = vehiculeJoueur.transform;
                    CameraJ1.LookAt = vehiculeJoueur.transform;
                    Debug.Log(CameraJ1.Follow);
                    Debug.Log(CameraJ1.LookAt);
                }else{
                    vehiculeJoueur = Instantiate(configurations.J2VehiculeChoisi);
                    vehiculeJoueur.name = "J2";
                    CameraJ2.Follow = vehiculeJoueur.transform;
                    CameraJ2.LookAt = vehiculeJoueur.transform;
                    Debug.Log(CameraJ2.Follow);
                    Debug.Log(CameraJ2.LookAt);
                }
                vehicules[index] = vehiculeJoueur;
                
                // Get the Voitures component and add it to the Joueurs array
                Voitures voiture = vehiculeJoueur.GetComponent<Voitures>();
                if (voiture != null)
                {
                    Joueurs[joueurIndex] = voiture;
                    if(joueurIndex == 0){
                        voiture.controls = arduino01;
                    }else{
                        voiture.controls = arduino02;
                    }
                    joueurIndex++;
                }

                index++;
            }
        }
        
        //Instanciate the opponents filling the remaining spots
        for (int i = index; i < vehicules.Length; i++)
        {
            GameObject vehiculeCPU = Instantiate(PrefabAdversaires[Random.Range(0, NbCPUs)]);
            vehiculeCPU.name = "CPU" + index;
            vehicules[i] = vehiculeCPU;
        }
    }
}
