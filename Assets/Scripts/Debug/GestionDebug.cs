using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GestionDebug : MonoBehaviour
{
    [SerializeField] private InputAction DebugToggleScreen1;
    [SerializeField] private InputAction DebugToggleScreen2;
    [SerializeField] private GameObject Screen;
    private bool isPressing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Toggle();
    }

    private void OnEnable(){
        DebugToggleScreen1.Enable();
        DebugToggleScreen2.Enable();
    }

    private void OnDisable(){
        DebugToggleScreen1.Disable();
        DebugToggleScreen2.Disable();
    }

    private void Toggle(){

        if(!isPressing && DebugToggleScreen1.ReadValue<float>() == 1){ // Open/Close Debug Screen on Screen 1
            isPressing = true;

            if(Screen.activeInHierarchy == false){
                Screen.SetActive(true);
                Screen.GetComponent<Canvas>().targetDisplay = 0;
            }else{
                Screen.SetActive(false);
            }
        }else if(!isPressing && DebugToggleScreen2.ReadValue<float>() == 1){ // Open/Close Debug Screen on Screen 2
            isPressing = true;

            if(Screen.activeInHierarchy == false){
                Screen.SetActive(true);
                Screen.GetComponent<Canvas>().targetDisplay = 1;
            }else{
                Screen.SetActive(false);
            }
        }else if(DebugToggleScreen2.ReadValue<float>() == 0 && DebugToggleScreen1.ReadValue<float>() == 0){
            isPressing = false;
        }
    }
}
