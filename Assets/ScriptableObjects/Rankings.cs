using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ranking", menuName = "ScriptableObjects/Rankings")]
public class Rankings : ScriptableObject
{
   public String[] ranking;
   public float[] time;
}
