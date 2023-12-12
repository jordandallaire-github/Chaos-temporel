using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UpdateFOV : MonoBehaviour
{

    private GameObject j1;
    private GameObject j2;

    private Voitures voitures1;

    private Voitures voitures2;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if(GameObject.Find("J1") != null){
            j1 = GameObject.Find("J1");
            voitures1 = j1.GetComponent<Voitures>();
        }

        if(GameObject.Find("J2") != null){
            j2 = GameObject.Find("J2");
            voitures2 = j2.GetComponent<Voitures>();
        }

        if(this.gameObject.name == "Virtual Camera J1" && j1 != null) {

            CinemachineVirtualCamera virtualCamera1 = this.gameObject.GetComponent<CinemachineVirtualCamera>();

            float speed = voitures1.speed; 
            float maxSpeed = voitures1.maxSpeedSol;
            float fov;

            if (speed > 9)
            {
                fov = Mathf.Lerp(60, 85, (speed - 9) / (maxSpeed - 9));
            }
            else
            {
                fov = 60;
            }

            virtualCamera1.m_Lens.FieldOfView = fov; // Modifie le FOV de la caméra virtuelle


        }

        if(this.gameObject.name == "Virtual Camera J2" && j2 != null) {
            CinemachineVirtualCamera virtualCamera2 = this.gameObject.GetComponent<CinemachineVirtualCamera>();

            float speed = voitures2.speed; 
            float maxSpeed = voitures2.maxSpeedSol;
            float fov;

            if (speed > 9)
            {
                fov = Mathf.Lerp(60, 85, (speed - 9) / (maxSpeed - 9));
            }
            else
            {
                fov = 60;
            }

            virtualCamera2.m_Lens.FieldOfView = fov; // Modifie le FOV de la caméra virtuelle


        }
        
    }
}
