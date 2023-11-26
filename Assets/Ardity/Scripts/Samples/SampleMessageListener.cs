/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/**
 * When creating your message listeners you need to implement these two methods:
 *  - OnMessageArrived
 *  - OnConnectionEvent
 */
public class SampleMessageListener : MonoBehaviour
{
    private float batonG;
    private float batonD;
    private int btnValue;


     void Start()
    {

    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        string[] valeurs = msg.Split(char.Parse(";"));
        batonG = float.Parse(valeurs[0]); 
        batonD = float.Parse(valeurs[1]);
        btnValue = int.Parse(valeurs[2]);

    }

    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }

    void Update(){
        
    }

    // Envoie les valeurs du Joystick gauche 
    // à n'importe quel objet qui appelle cette méthode
    public float GetJoystickL(){
        return batonG;
    }

    // Envoie les valeurs du Joystick droite à 
    // n'importe quel objet qui appelle cette méthode
    public float GetJoystickR(){
        return batonD;
    }

    // Envoie les valeurs du bouton rouge 
    //à n'importe quel objet qui appelle cette méthode
    public int GetActionButton(){
        return btnValue;
    }
    

}
