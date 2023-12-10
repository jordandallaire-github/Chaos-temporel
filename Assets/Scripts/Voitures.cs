using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Voitures : MonoBehaviour
{
    public SampleMessageListener controls; // Donnée reçu par le Arduino
    public PowerUpsEffets currentPowerUp;

    public GameObject centreMass;
    public bool controlsEnabled = false;
    private bool isResetting = false;
    public float rotationSpeed = 0.1f;
    public float speed = 1f;
    public float maxSpeedSol = 12;
    public float maxSpeedGazon = 8;
    public float accelerationTime = 4f; // Temps en secondes pour atteindre la vitesse maximale
    public float minReverseSpeed = 4f;
    public float decelerationTime = 4f;
    public bool hasPowerUp = false;
    public float brakeSpeed = 2f; // Adjust this value to set the braking speed
    public float deadZoneTop = 0.9f;
    public float deadZoneBottom = -0.9f;
    public float deadZoneCenter = 0.1f;
    public float deadZoneCenterNegative = -0.1f;
    private Rigidbody joueurRB;
    private VoitureCollision collision; // Script de collision

    public LayerMask groundLayer;
    public LayerMask grassLayer;

    private bool isButtonPressed = false;

    private float timeUpsideDown = 0f;
    public GameObject barrelPrefab; // Reference to the barrel prefab

    private GameObject barrelInstance; // Reference to the instantiated barrel

    public GameObject emplacemementTonneau1; // Reference to the instantiated barrel

    public GameObject emplacemementTonneau2; // Reference to the instantiated barrel

    private static int barrelCount = 0; // Variable statique pour compter le nombre de tonneaux créés

    private static bool isBarrelLaunched1 = false;

    private static bool isBarrelLaunched2 = false;

    public float forceBas = 50f;
    public GameObject PowerUpBarrel;
    public GameObject PowerUpBoost;

    public float conversionD;

    public float conversionG;

    public float batonD;

    public float batonG;

    private GameObject GOChaosmod;

    private ChaosMod chaosMod;

    // Start is called before the first frame update
    void Start()
    {
        joueurRB = GetComponent<Rigidbody>();
        collision = GetComponent<VoitureCollision>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("GestionnaireChaosMod") != null){
            GOChaosmod = GameObject.Find("GestionnaireChaosMod");
            chaosMod = GOChaosmod.GetComponent<ChaosMod>();
        }

        if(controlsEnabled){
            Deplacement();
        }

        // Vérifiez si le véhicule est à l'envers
        Vector3 rotation = transform.rotation.eulerAngles;
        if (!isResetting && (Mathf.Abs(rotation.x - 180) < 90 || Mathf.Abs(rotation.z - 180) < 90))
        {
            timeUpsideDown += Time.deltaTime;
            if (!isResetting && timeUpsideDown >= 5f)
            {
                ResetRotation();
            }
        }
        else
        {
            timeUpsideDown = 0f;
        }

        float btnValue = controls.GetActionButton();

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
                            if(gameObject.tag == "Player1"){
                                // Appliquer le power-up sur l'Arduino
                                currentPowerUp.Appliquer(this.gameObject);
                            }

                            if(gameObject.tag == "Player2"){
                                // Appliquer le power-up sur l'Arduino
                                currentPowerUp.Appliquer(this.gameObject);
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
            PowerUpBarrel.SetActive(true);
         }

         if(currentPowerUp != null && currentPowerUp.name == "BuffVitesse"){
          PowerUpBoost.SetActive(true);

         }
        
         joueurRB.centerOfMass = centreMass.transform.localPosition;


    }

    // Prend les données des Joysticks envoyé par le Arduino
    // et faire déplacer le véhicule
    void Deplacement(){

        // Obtenir les valeur reçu par le arduino
        batonG = controls.GetJoystickL();
        batonD = controls.GetJoystickR();
        
        float rotation = conversionG - conversionD;
        float movement = (conversionG + conversionD) / 2;

        if(chaosMod.InverControle){

            conversionG = (Mathf.InverseLerp(0, 1024, batonG) * 2 - 1) * -1;
            conversionD = (Mathf.InverseLerp(0, 1024, batonD) * 2 - 1) * -1;

        }else{
                
            conversionG = Mathf.InverseLerp(0, 1024, batonG) * 2 - 1;
            conversionD = Mathf.InverseLerp(0, 1024, batonD) * 2 - 1;

        }

        // Dead Zones
        if(conversionG > deadZoneTop){
            conversionG = 1.0f;
        }else if(conversionG < deadZoneBottom){
            conversionG = -1.0f;
        }else if(conversionG < deadZoneCenter && conversionG > deadZoneCenterNegative){
            conversionG = 0;
        }

        if(conversionD > deadZoneTop){
            conversionD = 1.0f;
        }else if(conversionD < deadZoneBottom){
            conversionD = -1.0f;
        }else if(conversionD < deadZoneCenter && conversionD > deadZoneCenterNegative){
            conversionD = 0;
        }



        // Il peut se déplacer seulement si il touche le sol
        if(collision.isOnGround){

            // Rotate around the Y-axis at a speed proportional to the rotation value.
            joueurRB.angularVelocity = new Vector3(0, rotation * rotationSpeed, 0);

            // Smoothly change the speed using interpolation
            if (movement > 0.7) {
                // If the vehicle is moving forward, increase the speed
                speed = Mathf.Lerp(speed, maxSpeedSol, Time.deltaTime / accelerationTime);
            } else if (movement < deadZoneCenterNegative) {
                // If the vehicle is moving backward, decrease the speed to the minimum reverse speed
                speed = minReverseSpeed;
            } else  {
                // If the vehicle is not moving, decrease the speed
                speed = Mathf.Lerp(speed, 0, Time.deltaTime / decelerationTime);
            }

            joueurRB.velocity = joueurRB.velocity.normalized * speed * movement;

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

        else if(collision.isOnGrass){

                // Rotate around the Y-axis at a speed proportional to the rotation value.
                joueurRB.angularVelocity = new Vector3(0, rotation * rotationSpeed, 0);

                // Smoothly change the speed using interpolation
                if (movement > 0.6) {
                    // If the vehicle is moving forward, increase the speed
                    speed = Mathf.Lerp(speed, maxSpeedGazon, Time.deltaTime / accelerationTime);
                } else if (movement < deadZoneCenterNegative) {
                    // If the vehicle is moving backward, decrease the speed to the minimum reverse speed
                    speed = minReverseSpeed;
                } else  {
                    // If the vehicle is not moving, decrease the speed
                    speed = Mathf.Lerp(speed, 0, Time.deltaTime / decelerationTime);
                }

                joueurRB.velocity = joueurRB.velocity.normalized * speed * movement;


                joueurRB.AddForce(transform.forward * movement * speed * Time.deltaTime, ForceMode.VelocityChange);
                        
                // Clamp the Rigidbody's speed to the maximum speed
                if (joueurRB.velocity.magnitude > maxSpeedGazon)
                {
                    joueurRB.velocity = joueurRB.velocity.normalized * maxSpeedGazon;
                }
                        
                // Apply a braking force when the movement value is close to zero
                if (Mathf.Abs(movement) < 0.1f)
                {
                    joueurRB.velocity = Vector3.Lerp(joueurRB.velocity, Vector3.zero, brakeSpeed * Time.deltaTime);
                }
                joueurRB.AddForce(transform.forward * movement * speed, ForceMode.VelocityChange);
                
            } 

            else{

                 joueurRB.AddForce(-transform.up * 100000.81f);

            }




    }

    void ResetRotation()
    {
        isResetting = true;
        // Réinitialisez la position du véhicule
        transform.rotation = Quaternion.Euler(0, 130f, 0);

        timeUpsideDown = 0f;
        
        isResetting = false;
    }

    private void CreerTonneau()
    {
        if (barrelCount < 100)
        {
            
            if(gameObject.tag == "Player1" && !isBarrelLaunched1){
                // Créer le tonneau devant le véhicule
                barrelInstance = Instantiate(barrelPrefab, emplacemementTonneau1.transform.position, emplacemementTonneau1.transform.rotation);
                barrelInstance.transform.parent = emplacemementTonneau1.transform;
                barrelCount++;
                isBarrelLaunched1 = true;
            }

            if(gameObject.tag == "Player2" && !isBarrelLaunched2){
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



    IEnumerator DesactiverPowerUp()
    {
        yield return new WaitForSeconds(currentPowerUp.cooldown);
        
        // Vérifier si currentPowerUp et powerUpTarget sont définis avant de les utiliser
        if (currentPowerUp != null)
        {
            if (currentPowerUp.name == "BuffVitesse")
            {
                if(this.gameObject.tag == "Player1"){
                    // Appliquer le power-up sur l'Arduino
                     currentPowerUp.Desactiver(this.gameObject);
                }

                if(this.gameObject.tag == "Player2"){
                    // Appliquer le power-up sur l'Arduino
                    currentPowerUp.Desactiver(this.gameObject);
                }
                PowerUpBoost.SetActive(false);

            }
            else
            {
                // Le power-up n'a pas de cible spécifique, ne rien faire
            }

            if(gameObject.tag == "Player1" && currentPowerUp.name == "SabotageBarrel"){
                this.barrelInstance.SetActive(false);
                PowerUpBarrel.SetActive(false); 
                isBarrelLaunched1 = false;
            }

            if(gameObject.tag == "Player2" && currentPowerUp.name == "SabotageBarrel"){
                this.barrelInstance.SetActive(false);
                PowerUpBarrel.SetActive(false); 
                isBarrelLaunched2 = false;
            }  

            // Réinitialiser l'état du power-up
            hasPowerUp = false;
            currentPowerUp = null;
        }
    }

}
