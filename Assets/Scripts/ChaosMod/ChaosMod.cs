using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChaosMod : MonoBehaviour
{


    public Voitures j1;

    public Voitures j2;

    public List<GameObject> affectedObjects;

    [SerializeField] public Slider sliderTempsChaosJ1;

     [SerializeField] public TextMeshProUGUI effectNameTextJ1;

    [SerializeField] public Slider sliderTempsChaosJ2;

    [SerializeField] public TextMeshProUGUI effectNameTextJ2;

    [SerializeField] public GameObject cartoon;

    [SerializeField] public GameObject cyperpunk;

    [SerializeField] public Material skyboxCyberpunk;

    [SerializeField] public Material skyboxCartoon;

   // La liste des fonctions
   System.Action[] effetsPowerUps;

   // Start is called before the first frame update
   void Start()
   {    
         if(GameObject.Find("J1") != null ){
            j1 = GameObject.Find("J1").GetComponent<Voitures>();
         }


        if(GameObject.Find("J2") != null ){
            j2 = GameObject.Find("J2").GetComponent<Voitures>();
        }

        effectNameTextJ1.text = "";
        effectNameTextJ2.text = "";

         // Trouver tous les objets ayant le tag "Player"
        GameObject[] player1Objects = GameObject.FindGameObjectsWithTag("Player1");
        GameObject[] player2Objects = GameObject.FindGameObjectsWithTag("Player2");
        // Trouver tous les objets ayant le tag "Adversaire"
        GameObject[] adversaireObjects = GameObject.FindGameObjectsWithTag("Adversaire");

        // Ajouter tous les objets trouvés à la liste
        affectedObjects.AddRange(player1Objects);
        affectedObjects.AddRange(player2Objects);
        affectedObjects.AddRange(adversaireObjects);

      // Initialiser le tableau de fonctions
      effetsPowerUps = new System.Action[]
      {
          ChangeMaps,

      };

      // Planifier l'appel de la fonction aléatoire à chaque 30 secondes
      InvokeRepeating(nameof(CallRandomFunction), 30, 30);
        StartCoroutine(UpdateChaosModeSlider(30));
   }

   // Appel de la fonction aléatoire
   void CallRandomFunction()
   {
       // Choisir une fonction aléatoire
       int index = Random.Range(0, effetsPowerUps.Length);

       // Appeler la fonction
       effetsPowerUps[index]();


        // Définir le texte du composant TextMeshPro à afficher le nom de l'effet actif
       effectNameTextJ1.text = effetsPowerUps[index].Method.Name;

               // Définir le texte du composant TextMeshPro à afficher le nom de l'effet actif
       effectNameTextJ2.text = effetsPowerUps[index].Method.Name;

       // Désactiver le texte de l'effet précédent après 30 secondes
       StartCoroutine(DisableEffectNameText(5f));


       StartCoroutine(UpdateChaosModeSlider(30));

   }

    void Geler()
    {
        if(GameObject.Find("J1") != null ){
            j1.enabled = false;
        }

        if(GameObject.Find("J2") != null ){
            j2.enabled = false;
        }
        FakeCrash();
        StartCoroutine(WaitAndResume(5f));
    }

    void FakeCrash()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {   
        if(GameObject.Find("J1") != null ){
            j1.enabled = true;
        }

        if(GameObject.Find("J2") != null ){
            j2.enabled = true;
        }
        Time.timeScale = 1;
    }

    IEnumerator WaitAndResume(float temps)
    {
        yield return new WaitForSecondsRealtime(temps); // Wait for 3 seconds
        ResumeGame(); // Resume the game
    }

    void SlowMo()
    {
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = Time.timeScale * 0.01f;
        StartCoroutine(WaitAndResume(8f));
    }

    void AugmenteTaille()
    {
        foreach (GameObject obj in affectedObjects)
        {
            StartCoroutine(ChangeSize(obj, 1.8f, 5f));
        }
        if(GameObject.Find("J1") != null ){
            j1.maxSpeedSol = 7;
        }

        if(GameObject.Find("J2") != null ){
            j2.maxSpeedSol = 7;
        }

    }

    void DiminueTaille()
    {
        foreach (GameObject obj in affectedObjects)
        {
            StartCoroutine(ChangeSize(obj, 0.2f, 5f));
        }
        if(GameObject.Find("J1") != null ){
            j1.maxSpeedSol = 20;
        }
        if(GameObject.Find("J2") != null ){
            j2.maxSpeedSol = 20;
        }

    }

    IEnumerator ChangeSize(GameObject obj, float targetScale, float duration)
    {
        Vector3 initialScale = obj.transform.localScale;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            obj.transform.localScale = Vector3.Lerp(initialScale, initialScale * targetScale, t);
            yield return null;
        }

        obj.transform.localScale = initialScale;
        if(GameObject.Find("J1") != null ){
            j1.maxSpeedSol = 16;
        }

        if(GameObject.Find("J2") != null ){
            j2.maxSpeedSol = 16;
        }
    }

    IEnumerator UpdateChaosModeSlider(float duration)
    {
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            sliderTempsChaosJ1.value = t * 180;
            sliderTempsChaosJ2.value = t * 180;
            yield return null;
        }

            sliderTempsChaosJ1.value = 0;
            sliderTempsChaosJ2.value = 0;
            
    }

       IEnumerator DisableEffectNameText(float delay)
   {
       yield return new WaitForSeconds(delay);
       effectNameTextJ1.text = "";
       effectNameTextJ2.text = "";
   }

   void ChangeMaps(){

    if(cartoon.activeSelf == true){
        cartoon.SetActive(false);
        cyperpunk.SetActive(true);
        RenderSettings.skybox = skyboxCyberpunk;
    }

    else{
        cartoon.SetActive(true);
        cyperpunk.SetActive(false);
        RenderSettings.skybox = skyboxCartoon;
    }

   }




}
