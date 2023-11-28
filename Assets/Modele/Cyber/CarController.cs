using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
   public float speed;
   public Vector3 minBoundary;
   public Vector3 maxBoundary;

   private Rigidbody carRigidbody;

   private void Start()
   {
       carRigidbody = GetComponent<Rigidbody>();
   }

   private void FixedUpdate()
   {
       // Check if the car is near the boundary
       if (transform.position.x > maxBoundary.x || transform.position.x < minBoundary.x ||
           transform.position.z > maxBoundary.z || transform.position.z < minBoundary.z)
       {
           // Change the direction of the car
           carRigidbody.velocity = -carRigidbody.velocity;
       }
   }
}
