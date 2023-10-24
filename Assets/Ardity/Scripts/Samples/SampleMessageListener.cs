/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/**
 * When creating your message listeners you need to implement these two methods:
 *  - OnMessageArrived
 *  - OnConnectionEvent
 */
public class SampleMessageListener : MonoBehaviour
{
    public GameObject cube;
    public Slider joystickG;
    public Slider joystickD;
    private float batonG;
    private float batonD;

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        string[] valeurs = msg.Split(char.Parse(";"));
        batonG = float.Parse(valeurs[0]); 
        batonD = float.Parse(valeurs[1]); 


    }

    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }

    void Update(){
        //Debug.Log(joystickD, joystickG);

            float conversionG = (Mathf.InverseLerp(0, 1024, batonG) * 2 - 1) * -1;
            float conversionD = (Mathf.InverseLerp(0, 1024, batonD) * 2 - 1) * -1;


            float rotation = conversionG - conversionD;
            float movement = (conversionG + conversionD) / 2;

            joystickD.value = conversionD;
            joystickG.value = conversionG;

            // Rotate around the Y-axis at a speed proportional to the rotation value.
            cube.transform.Rotate(0, rotation * 50 * Time.deltaTime, 0);

            // Move forward or backward at a speed proportional to the movement value.
            cube.transform.Translate(0, 0, movement * 5 * Time.deltaTime);


            Debug.Log("rotation: " + rotation);
            Debug.Log("mouvement: " + movement);

    }

}
