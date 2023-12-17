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

    private const int SCREEN_1 = 0;
    private const int SCREEN_2 = 1;

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
        if(!isPressing){
            if(DebugToggleScreen1.ReadValue<float>() == 1){
                ToggleScreen(SCREEN_1);
            }else if(DebugToggleScreen2.ReadValue<float>() == 1){
                ToggleScreen(SCREEN_2);
            }
        }else if(DebugToggleScreen2.ReadValue<float>() == 0 && DebugToggleScreen1.ReadValue<float>() == 0){
            isPressing = false;
        }
    }

    private void ToggleScreen(int screen){
        isPressing = true;

        if(Screen.activeInHierarchy == false){
            Screen.SetActive(true);
            Screen.GetComponent<Canvas>().targetDisplay = screen;
        }else{
            Screen.SetActive(false);
        }
    }
}
