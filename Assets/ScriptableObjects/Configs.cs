using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Configs", menuName = "ScriptableObjects/Configs")]
public class Configs : ScriptableObject
{
    public string J1Port = "COM3";
    public string J2Port = "COM4";
    public bool[] playerStarted = new bool[2] { false, false };

    public GameObject J1VehiculeChoisi;
    public GameObject J2VehiculeChoisi;
}
