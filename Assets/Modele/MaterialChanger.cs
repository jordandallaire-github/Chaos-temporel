using System.Collections;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
   public Material[] materials; // Assign the materials in the Unity Inspector
   public float minDelay = 1f; // Minimum time delay in seconds before the material changes
   public float maxDelay = 10f; // Maximum time delay in seconds before the material changes

   private MeshRenderer meshRenderer;

   void Start()
   {
       meshRenderer = GetComponent<MeshRenderer>();
       StartCoroutine(ChangeMaterial());
   }

   IEnumerator ChangeMaterial()
   {
       while (true)
       {
           float delay = Random.Range(minDelay, maxDelay);
           yield return new WaitForSeconds(delay);
           int index = Random.Range(0, materials.Length);
           meshRenderer.material = materials[index];
       }
   }
}
