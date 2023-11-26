using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ranking", menuName = "ScriptableObjects/Rankings")]
public class Rankings : ScriptableObject
{
   public String[] ranking = new string[4] {"", "", "", ""};
   public float[] time = new float[4] {0, 0, 0, 0};
}
