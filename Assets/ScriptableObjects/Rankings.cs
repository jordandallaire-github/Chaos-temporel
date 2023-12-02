using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ranking", menuName = "ScriptableObjects/Rankings")]
public class Rankings : ScriptableObject
{
   public GameObject[] ranking = new GameObject[4] {null, null, null, null};
   public float[] time = new float[4] {0, 0, 0, 0};
}
