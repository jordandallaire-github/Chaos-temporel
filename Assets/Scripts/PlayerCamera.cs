using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public GameObject joueur;

    public GameObject vue;

    public float vitesse = 1.5f;

    public int index;



    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        this.transform.LookAt(joueur.transform);
        float voitureMouvement = Mathf.Abs(Vector3.Distance(this.transform.position, vue.transform.position)* vitesse);
        this.transform.position = Vector3.MoveTowards(this.transform.position, vue.transform.position, voitureMouvement * Time.deltaTime);

    }
}
