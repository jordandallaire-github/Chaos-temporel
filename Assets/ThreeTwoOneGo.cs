using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeTwoOneGo : MonoBehaviour
{
    public Material newMaterial; // Assign in Inspector
    public float delay = 5f; // Time to wait in seconds

    private void Start()
    {
        StartCoroutine(ChangeMaterialAfterDelay());
    }

    private IEnumerator ChangeMaterialAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = newMaterial;
        }
    }
}
