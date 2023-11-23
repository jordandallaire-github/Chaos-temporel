using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voitures : MonoBehaviour
{
    [SerializeField] private SampleMessageListener controls; // Donnée reçu par le Arduino
    public bool controlsEnabled = false;
    public float rotationSpeed = 0.1f;
    public float speed = 1f;
    public float maxSpeedSol = 12;
    public float accelerationTime = 4f; // Temps en secondes pour atteindre la vitesse maximale
    public float minReverseSpeed = 4f;
    public float decelerationTime = 4f;
    public float brakeSpeed = 2f; // Adjust this value to set the braking speed
    private Rigidbody joueurRB;
    private VoitureCollision collision; // Script de collision
    private bool isButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        joueurRB = GetComponent<Rigidbody>();
        collision = GetComponent<VoitureCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controlsEnabled){
            Deplacement();
        }
    }

    // Prend les données des Joysticks envoyé par le Arduino
    // et faire déplacer le véhicule
    void Deplacement(){

        // Obtenir les valeur reçu par le arduino
        float batonG = controls.GetJoystickL();
        float batonD = controls.GetJoystickR();

        // Convertir en valeur numérique entre -1 et 1
        float conversionG = (Mathf.InverseLerp(0, 1024, batonG) * 2 - 1) * -1;
        float conversionD = (Mathf.InverseLerp(0, 1024, batonD) * 2 - 1) * -1;

        float rotation = conversionG - conversionD;
        float movement = (conversionG + conversionD) / 2;

        // Il peut se déplacer seulement si il touche le sol
        if(collision.isOnGround){

            // Rotate around the Y-axis at a speed proportional to the rotation value.
            joueurRB.angularVelocity = new Vector3(0, rotation * rotationSpeed, 0);

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

            joueurRB.velocity = joueurRB.velocity.normalized * speed;

                joueurRB.AddForce(transform.forward * movement * speed * Time.deltaTime, ForceMode.VelocityChange);
                        
                // Clamp the Rigidbody's speed to the maximum speed
                if (joueurRB.velocity.magnitude > maxSpeedSol)
                {
                    joueurRB.velocity = joueurRB.velocity.normalized * maxSpeedSol;
                }
                        
                // Apply a braking force when the movement value is close to zero
                if (Mathf.Abs(movement) < 0.1f)
                {
                    joueurRB.velocity = Vector3.Lerp(joueurRB.velocity, Vector3.zero, brakeSpeed * Time.deltaTime);
                }
                joueurRB.AddForce(transform.forward * movement * speed, ForceMode.VelocityChange);
        }
    }
}
