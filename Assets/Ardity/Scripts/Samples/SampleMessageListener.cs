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

    public PowerUpsEffets currentPowerUp;
    public float speed = 1f;
    private float originalSpeed = 1f;
    public float maxSpeed = 10f; // Adjust this value to set the maximum speed
    public float brakeSpeed = 2f; // Adjust this value to set the braking speed
    public float rotationSpeed = 0.1f;

    public GameObject barrelPrefab; // Reference to the barrel prefab

    private GameObject barrelInstance; // Reference to the instantiated barrel

    public GameObject emplacemementTonneau; // Reference to the instantiated barrel

    private bool barrelCreated = false;

    public bool hasPowerUp = false;
    public GameObject arduino;

    private Rigidbody joueurRb;

    private VoitureCollision voitureCollision;

    public Slider joystickG;
    public Slider joystickD;
    private float batonG;
    private float batonD;
    private int btnValue;
    public bool isButtonPressed = false;



     void Start()
    {
        voitureCollision = cube.GetComponent<VoitureCollision>();
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
         if (currentPowerUp != null ){
            Debug.Log(currentPowerUp.name);
         }
            

            float conversionG = (Mathf.InverseLerp(0, 1024, batonG) * 2 - 1) * -1;
            float conversionD = (Mathf.InverseLerp(0, 1024, batonD) * 2 - 1) * -1;


            float rotation = conversionG - conversionD;
            float movement = (conversionG + conversionD) / 2;

            joystickD.value = conversionD;
            joystickG.value = conversionG;

            isButtonPressed = (btnValue == 1);

            if (isButtonPressed)
            {
                if (hasPowerUp)
                {
                    // Vérifier si le power-up est en cooldown
                    if (currentPowerUp != null )
                    {
                        
                        // Vérifier le nom du power-up actuel
                        if (currentPowerUp.name == "BuffVitesse")
                        {
                            // Appliquer le power-up sur l'Arduino
                            currentPowerUp.Appliquer(arduino);

                            if (speed >= maxSpeed)
                            {
                                speed = maxSpeed;
                            }

                        }

                        // Lancer la coroutine pour désactiver le power-up après la durée spécifiée
                        StartCoroutine(DesactiverPowerUp());
                    }
                    else
                    {
                        // Le power-up est en cooldown, ne rien faire
                    }
                }
                else
                {
                    // Le joueur n'a pas de power-up actuellement
                }
            }

            if (currentPowerUp != null && currentPowerUp.name == "SabotageBarrel")
            {
                CreerTonneau();
            }

            // // Rotate around the Y-axis at a speed proportional to the rotation value.
            // cube.transform.Rotate(0, rotation * 50 * Time.deltaTime, 0);

            // // Move forward or backward at a speed proportional to the movement value.
            // cube.transform.Translate(0, 0, movement * 5 * Time.deltaTime);

            if(voitureCollision.isOnGround){

                speed = originalSpeed;

                maxSpeed = 15f;

                // Rotate around the Y-axis at a speed proportional to the rotation value.
                joueurRb.angularVelocity = new Vector3(0, rotation * rotationSpeed, 0);

                // Move forward or backward at a speed proportional to the movement value.
                // joueurRb.velocity = cube.transform.forward * movement * 5;float speed = 0.1f; // Adjust this value to get the desired speed
                // Apply force to the Rigidbody

                // Smoothly change the speed using interpolation
                float targetSpeed = speed;
                float smoothSpeed = Mathf.Lerp(joueurRb.velocity.magnitude, targetSpeed, 0.1f);
                joueurRb.velocity = joueurRb.velocity.normalized * smoothSpeed;

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


                //Debug.Log("rotation: " + rotation);
                //Debug.Log("mouvement: " + movement);
                
            }

            if(voitureCollision.isOnGrass){

                speed = 0.5f;

                maxSpeed = 5f;

                // Rotate around the Y-axis at a speed proportional to the rotation value.
                joueurRb.angularVelocity = new Vector3(0, rotation * rotationSpeed, 0);

                // Move forward or backward at a speed proportional to the movement value.
                // joueurRb.velocity = cube.transform.forward * movement * 5;float speed = 0.1f; // Adjust this value to get the desired speed
                // Apply force to the Rigidbody

                // Smoothly change the speed using interpolation
                float targetSpeed = speed;
                float smoothSpeed = Mathf.Lerp(joueurRb.velocity.magnitude, targetSpeed, 0.1f);
                joueurRb.velocity = joueurRb.velocity.normalized * smoothSpeed;

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


                //Debug.Log("rotation: " + rotation);
                //Debug.Log("mouvement: " + movement);
                
            }      


    }

    IEnumerator DesactiverPowerUp()
    {
        yield return new WaitForSeconds(currentPowerUp.cooldown);
        
        // Vérifier si currentPowerUp et powerUpTarget sont définis avant de les utiliser
        if (currentPowerUp != null)
        {
            if (currentPowerUp.name == "BuffVitesse")
            {
                // Désactiver le power-up sur l'Arduino
                currentPowerUp.Desactiver(arduino);
            }
            else if (currentPowerUp.name == "SabotageBarrel")
            {
                currentPowerUp.Desactiver(barrelInstance);
            }
            else
            {
                // Le power-up n'a pas de cible spécifique, ne rien faire
            }

            speed = originalSpeed;
            // Réinitialiser l'état du power-up
            hasPowerUp = false;
            currentPowerUp = null;
            arduino = null;
            barrelCreated = false;
        }
    }

        private void CreerTonneau()
    {
        if (!barrelCreated )
        {
                if (barrelInstance == null || !barrelInstance.activeSelf)
                {
                    // Créer le tonneau devant le véhicule
                    barrelInstance = Instantiate(barrelPrefab, emplacemementTonneau.transform.position, emplacemementTonneau.transform.rotation);
                    barrelInstance.transform.parent = emplacemementTonneau.transform;
                    barrelCreated = true;
                }
        }

        if (isButtonPressed)
            {
                barrelInstance.transform.parent = null;
                Rigidbody tonneauRB  =  GameObject.Find("Barrel(Clone)").GetComponent<Rigidbody>();
                tonneauRB.isKinematic = false;
                TonneauController tonneauScript = GameObject.Find("Barrel(Clone)").GetComponent<TonneauController>();
                tonneauScript.enabled = true;
            }
    }

}
