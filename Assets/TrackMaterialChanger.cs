using UnityEngine;

public class TrackMaterialChanger : MonoBehaviour
{
    public Material cartoonMaterial;
    public Material cyberpunkMaterial;
    public GameObject cartoonGameObject;
    public GameObject cyberpunkGameObject;    
    public GameObject Track;


    void Update()
    {
        if (cartoonGameObject.activeInHierarchy)
        {
            Track.GetComponent<Renderer>().material = cartoonMaterial;
        }
        else if (cyberpunkGameObject.activeInHierarchy)
        {
            Track.GetComponent<Renderer>().material = cyberpunkMaterial;
        }
    }
}
