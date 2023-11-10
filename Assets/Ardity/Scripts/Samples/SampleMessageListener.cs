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
    public float speed = 0.1f;
    public float maxSpeed = 10f; // Adjust this value to set the maximum speed
    public float brakeSpeed = 2f; // Adjust this value to set the braking speed
    public float rotationSpeed = 0.1f;

    private Rigidbody joueurRb;

    private VoitureJoueur voitureJoueur;

    public Slider joystickG;
    public Slider joystickD;
    private float batonG;
    private float batonD;
    private int btnValue;



     void Start()
    {
        voitureJoueur = cube.GetComponent<VoitureJoueur>();
        joueurRb = cube.GetComponent<Rigidbody>();
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
        //Debug.Log(joystickD, joystickG);

            float conversionG = (Mathf.InverseLerp(0, 1024, batonG) * 2 - 1) * -1;
            float conversionD = (Mathf.InverseLerp(0, 1024, batonD) * 2 - 1) * -1;


            float rotation = conversionG - conversionD;
            float movement = (conversionG + conversionD) / 2;

            joystickD.value = conversionD;
            joystickG.value = conversionG;

            // // Rotate around the Y-axis at a speed proportional to the rotation value.
            // cube.transform.Rotate(0, rotation * 50 * Time.deltaTime, 0);

            // // Move forward or backward at a speed proportional to the movement value.
            // cube.transform.Translate(0, 0, movement * 5 * Time.deltaTime);

            if(voitureJoueur.isOnGround){

                // Rotate around the Y-axis at a speed proportional to the rotation value.
                joueurRb.angularVelocity = new Vector3(0, rotation * rotationSpeed, 0);

                // Move forward or backward at a speed proportional to the movement value.
                // joueurRb.velocity = cube.transform.forward * movement * 5;float speed = 0.1f; // Adjust this value to get the desired speed
                // Apply force to the Rigidbody
                joueurRb.AddForce(cube.transform.forward * movement * speed * Time.deltaTime, ForceMode.VelocityChange);
                        
                // Clamp the Rigidbody's speed to the maximum speed
                if (joueurRb.velocity.magnitude > maxSpeed)
                {
                    joueurRb.velocity = joueurRb.velocity.normalized * maxSpeed;
                }
                        
                // Apply a braking force when the movement value is close to zero
                if (Mathf.Abs(movement) < 0.1f)
                {
                    joueurRb.velocity = Vector3.Lerp(joueurRb.velocity, Vector3.zero, brakeSpeed * Time.deltaTime);
                }
                joueurRb.AddForce(cube.transform.forward * movement * speed, ForceMode.VelocityChange);


                Debug.Log("rotation: " + rotation);
                Debug.Log("mouvement: " + movement);
                
            }


    }

}
