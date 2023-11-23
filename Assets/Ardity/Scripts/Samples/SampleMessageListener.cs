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
using System.Collections.Generic;

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
    private float originalSpeed = 4f;

    public float accelerationTime = 4f; // Temps en secondes pour atteindre la vitesse maximale
    public float minReverseSpeed = 4f;
    public float decelerationTime = 4f;
    public float maxSpeedSol = 12;
    public float maxSpeedGazon = 8;
    public float brakeSpeed = 2f; // Adjust this value to set the braking speed


    public float rotationSpeed = 0.1f;

    public GameObject barrelPrefab; // Reference to the barrel prefab

    private GameObject barrelInstance; // Reference to the instantiated barrel

    public GameObject emplacemementTonneau1; // Reference to the instantiated barrel

    public GameObject emplacemementTonneau2; // Reference to the instantiated barrel

    private List<GameObject> barrelPrefabs = new List<GameObject>();

    public bool hasPowerUp = false;
    public GameObject arduino1;

    public GameObject arduino2;

    private Rigidbody joueurRb;

    private VoitureCollision voitureCollision;
    private float batonG;
    private float batonD;
    private int btnValue;
    public bool isButtonPressed = false;

    private static int barrelCount = 0; // Variable statique pour compter le nombre de tonneaux créés

    private static bool isBarrelLaunched1 = false;

    private static bool isBarrelLaunched2 = false;



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
            
            float conversionG = (Mathf.InverseLerp(0, 1024, batonG) * 2 - 1) * -1;
            float conversionD = (Mathf.InverseLerp(0, 1024, batonD) * 2 - 1) * -1;


            float rotation = conversionG - conversionD;
            float movement = (conversionG + conversionD) / 2;


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
                            if(gameObject.tag == "Arduino1"){
                                // Appliquer le power-up sur l'Arduino
                                currentPowerUp.Appliquer(arduino1);
                            }

                            if(gameObject.tag == "Arduino2"){
                                // Appliquer le power-up sur l'Arduino
                                currentPowerUp.Appliquer(arduino2);
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

            /* if(voitureCollision.isOnGround){


                // Rotate around the Y-axis at a speed proportional to the rotation value.
                joueurRb.angularVelocity = new Vector3(0, rotation * rotationSpeed, 0);

                // Smoothly change the speed using interpolation
                if (movement > 0.6) {
                    // If the vehicle is moving forward, increase the speed
                    speed = Mathf.Lerp(speed, maxSpeedSol, Time.deltaTime / accelerationTime);
                } else if (movement <= 0) {
                    // If the vehicle is moving backward, decrease the speed to the minimum reverse speed
                    speed = minReverseSpeed;
                } else  {
                    // If the vehicle is not moving, decrease the speed
                    speed = Mathf.Lerp(speed, 0, Time.deltaTime / decelerationTime);
                }

                joueurRb.velocity = joueurRb.velocity.normalized * speed;

                joueurRb.AddForce(cube.transform.forward * movement * speed * Time.deltaTime, ForceMode.VelocityChange);
                        
                // Clamp the Rigidbody's speed to the maximum speed
                if (joueurRb.velocity.magnitude > maxSpeedSol)
                {
                    joueurRb.velocity = joueurRb.velocity.normalized * maxSpeedSol;
                }
                        
                // Apply a braking force when the movement value is close to zero
                if (Mathf.Abs(movement) < 0.1f)
                {
                    joueurRb.velocity = Vector3.Lerp(joueurRb.velocity, Vector3.zero, brakeSpeed * Time.deltaTime);
                }
                joueurRb.AddForce(cube.transform.forward * movement * speed, ForceMode.VelocityChange);



                
            } */

           /* if(voitureCollision.isOnGrass){

                // Rotate around the Y-axis at a speed proportional to the rotation value.
                joueurRb.angularVelocity = new Vector3(0, rotation * rotationSpeed, 0);

                // Smoothly change the speed using interpolation
                if (movement > 0.6) {
                    // If the vehicle is moving forward, increase the speed
                    speed = Mathf.Lerp(speed, maxSpeedGazon, Time.deltaTime / accelerationTime);
                } else if (movement <= 0) {
                    // If the vehicle is moving backward, decrease the speed to the minimum reverse speed
                    speed = minReverseSpeed;
                } else  {
                    // If the vehicle is not moving, decrease the speed
                    speed = Mathf.Lerp(speed, 0, Time.deltaTime / decelerationTime);
                }

                joueurRb.velocity = joueurRb.velocity.normalized * speed;


                joueurRb.AddForce(cube.transform.forward * movement * speed * Time.deltaTime, ForceMode.VelocityChange);
                        
                // Clamp the Rigidbody's speed to the maximum speed
                if (joueurRb.velocity.magnitude > maxSpeedGazon)
                {
                    joueurRb.velocity = joueurRb.velocity.normalized * maxSpeedGazon;
                }
                        
                // Apply a braking force when the movement value is close to zero
                if (Mathf.Abs(movement) < 0.1f)
                {
                    joueurRb.velocity = Vector3.Lerp(joueurRb.velocity, Vector3.zero, brakeSpeed * Time.deltaTime);
                }
                joueurRb.AddForce(cube.transform.forward * movement * speed, ForceMode.VelocityChange);
                
            } */


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
    public float GetActionButton(){
        return btnValue;
    }
    
    IEnumerator DesactiverPowerUp()
    {
        yield return new WaitForSeconds(currentPowerUp.cooldown);
        
        // Vérifier si currentPowerUp et powerUpTarget sont définis avant de les utiliser
        if (currentPowerUp != null)
        {
            if (currentPowerUp.name == "BuffVitesse")
            {
                if(this.gameObject.tag == "Arduino1"){
                    // Appliquer le power-up sur l'Arduino
                     currentPowerUp.Desactiver(arduino1);
                }

                if(this.gameObject.tag == "Arduino2"){
                    // Appliquer le power-up sur l'Arduino
                    currentPowerUp.Desactiver(arduino2);
                }
            }
            else if (currentPowerUp.name == "SabotageBarrel")
            {

            }
            else
            {
                // Le power-up n'a pas de cible spécifique, ne rien faire
            }

            // Réinitialiser l'état du power-up
            hasPowerUp = false;
            currentPowerUp = null;

            if(gameObject.tag == "Arduino1"){
                this.barrelInstance.SetActive(false);
                isBarrelLaunched1 = false;
            }

            if(gameObject.tag == "Arduino2"){
                this.barrelInstance.SetActive(false);
                isBarrelLaunched2 = false;
            }      

        }
    }

    private void CreerTonneau()
    {
        if (barrelCount < 5)
        {
            
            if(gameObject.tag == "Arduino1" && !isBarrelLaunched1){
                // Créer le tonneau devant le véhicule
                barrelInstance = Instantiate(barrelPrefab, emplacemementTonneau1.transform.position, emplacemementTonneau1.transform.rotation);
                barrelInstance.transform.parent = emplacemementTonneau1.transform;
                barrelCount++;
                isBarrelLaunched1 = true;
            }

            if(gameObject.tag == "Arduino2" && !isBarrelLaunched2){
                // Créer le tonneau devant le véhicule
                barrelInstance = Instantiate(barrelPrefab, emplacemementTonneau2.transform.position, emplacemementTonneau2.transform.rotation);
                barrelInstance.transform.parent = emplacemementTonneau2.transform;
                barrelCount++;
                isBarrelLaunched2 = true;
            }


        }

        if (isButtonPressed && barrelInstance != null)
        {
            barrelInstance.transform.parent = null;
            Rigidbody tonneauRB = barrelInstance.GetComponent<Rigidbody>();
            tonneauRB.isKinematic = false;
            Collider tonneauC = barrelInstance.GetComponent<Collider>();
            tonneauC.enabled = true;
            TonneauController tonneauScript = barrelInstance.GetComponent<TonneauController>();
            tonneauScript.enabled = true;
        }
    }

}
