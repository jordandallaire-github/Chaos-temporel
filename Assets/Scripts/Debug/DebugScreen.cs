using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugScreen : MonoBehaviour
{
    [SerializeField] private Configs configurations;
    [SerializeField] private TMP_InputField J1Port;
    [SerializeField] private TMP_InputField J2Port;
    
    // Start is called before the first frame update
    void Start()
    {
        J1Port.text = configurations.J1Port;
        J2Port.text = configurations.J2Port;
    }

    // When a field is changed
    public void OnTextChangedP1(string newPort){
        configurations.J1Port = newPort;
    }
    public void OnTextChangedP2(string newPort){
        configurations.J2Port = newPort;
    }

    // Quit the game
    public void OnQuitGame(){
        Application.Quit();
    }
}
