using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionAccueil : MonoBehaviour
{
    [SerializeField] private Configs configurations;
    [SerializeField] private SerialController sc1;
    [SerializeField] private SerialController sc2;
    [SerializeField] private Rankings rankings;
    [SerializeField] private UIEcranTitre[] ecrans;

    // Start is called before the first frame update
    void Start()
    {
        // Vider le ranking
        for (int i = 0; i < rankings.ranking.Length; i++)
        {
            rankings.ranking[i] = null;
            rankings.time[i] = 0;
        }

        // Ajuster port Serial
        sc1.portName = configurations.J1Port;
        sc2.portName = configurations.J2Port;

        // Reset player playing
        for (int i = 0; i < configurations.playerStarted.Length; i++)
        {
            configurations.playerStarted[i] = false;
        }

        // Reset les choix de joueurs
        configurations.J1VehiculeChoisi = null;
        configurations.J2VehiculeChoisi = null;
    }

    void Update(){
        
        if(configurations.playerStarted[0] && configurations.playerStarted[1]){
            if(ecrans[0].ready && ecrans[1].ready){
                SceneManager.LoadScene("Piste01");
            }
        }else{
            for (int i = 0; i < configurations.playerStarted.Length; i++)
            {
                if(configurations.playerStarted[i] && ecrans[i].ready){
                    SceneManager.LoadScene("Piste01");
                }
            }
        }
    }

}
