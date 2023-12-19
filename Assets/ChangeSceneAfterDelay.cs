using UnityEngine;
using System.Collections;

public class ChangeSceneAfterDelay : MonoBehaviour
{
   public float delayTime = 25.3f; // Declare delayTime as a public variable

   void Start()
   {
       StartCoroutine(ChangeScene(delayTime));
   }

   IEnumerator ChangeScene(float seconds)
   {
       yield return new WaitForSeconds(seconds);
       UnityEngine.SceneManagement.SceneManager.LoadScene("Piste01");
   }
}
