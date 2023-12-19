using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Image image;
   public Sprite[] sprites;
   
   void Start()
   {
       StartCoroutine(Countdown(4)); // start a 10 second countdown
   }
   
   public IEnumerator Countdown(int seconds)
   {
       int counter = seconds;
       int spriteIndex = 0;
       while (counter > 0)
       {
           Debug.Log("Counter: " + counter);
           image.sprite = sprites[spriteIndex];
           spriteIndex = (spriteIndex + 1) % sprites.Length; // cycle through sprites
           yield return new WaitForSeconds(1);
           counter--;
       }

       // Deactivate the GameObject when the countdown finishes
       gameObject.SetActive(false);
   }
}
