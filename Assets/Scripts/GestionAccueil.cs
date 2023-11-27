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
            rankings.ranking[i] = "";
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
    }

    void Update(){
        
        if(configurations.playerStarted[0] && configurations.playerStarted[1] && ecrans[0].ready && ecrans[1].ready){
        
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
