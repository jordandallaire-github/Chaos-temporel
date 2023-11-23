using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosMod : MonoBehaviour
{


    public Voitures j1;

    public Voitures j2;

    public List<GameObject> affectedObjects;

   // La liste des fonctions
   System.Action[] effetsPowerUps;

   // Start is called before the first frame update
   void Start()
   {
    //    const tailleInitial =  affectedObjects.transform.localScale = new Vector3(1, 1, 1);

      // Initialiser le tableau de fonctions
      effetsPowerUps = new System.Action[]
      {
          CallAugmenteTaille,
      };

      // Planifier l'appel de la fonction aléatoire à chaque 30 secondes
      InvokeRepeating(nameof(CallRandomFunction), 10, 10);
   }

   // Appel de la fonction aléatoire
   void CallRandomFunction()
   {
       // Choisir une fonction aléatoire
       int index = Random.Range(0, effetsPowerUps.Length);

       // Appeler la fonction
       effetsPowerUps[index]();
   }

    void CallFreeze()
    {
        j1.enabled = false;
        j2.enabled = false;
        FreezeGame();
        StartCoroutine(WaitAndResume(5f));
    }

    void FreezeGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {   
        j1.enabled = true;
        j2.enabled = true;
        Time.timeScale = 1;
    }

    IEnumerator WaitAndResume(float temps)
    {
        yield return new WaitForSecondsRealtime(temps); // Wait for 3 seconds
        ResumeGame(); // Resume the game
    }

    void CallSlowMo()
    {
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = Time.timeScale * 0.01f;
        StartCoroutine(WaitAndResume(8f));
    }

    void CallAugmenteTaille()
    {
        foreach (GameObject obj in affectedObjects)
        {
            StartCoroutine(ChangeSize(obj, 1.8f, 5f));
        }
        j1.maxSpeedSol = 7;
        j2.maxSpeedSol = 7;
    }

    void CallDiminueTaille()
    {
        foreach (GameObject obj in affectedObjects)
        {
            StartCoroutine(ChangeSize(obj, 0.2f, 5f));
        }
        j1.maxSpeedSol = 12;
        j2.maxSpeedSol = 12;
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
        j1.maxSpeedSol = 10;
        j2.maxSpeedSol = 10;
    }


}
