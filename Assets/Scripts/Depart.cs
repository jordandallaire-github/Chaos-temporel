using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Depart : MonoBehaviour
{

        [SerializeField] private GameObject[] vehicules;
        private UnityEngine.Vector3[] positions;
    
        void Start()
        {
            positions = new UnityEngine.Vector3[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                UnityEngine.Vector3 position = transform.GetChild(i).position;
                positions[i] = position;
                // Debug.Log(positions[i]);
            }
    
            System.Random rand = new System.Random();
            List<int> usedIndices = new List<int>();
            
            foreach (GameObject vehicule in vehicules)
            {
                int index;
                do
                {
                    index = rand.Next(positions.Length);
                } while (usedIndices.Contains(index));
                
                usedIndices.Add(index);
                vehicule.transform.position = positions[index];
            }
        }
}
