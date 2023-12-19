using System.Collections;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
  public Camera mainCamera;
  public Camera secondaryCamera;
  public float waitTime = 20f; // This is the waiting time

  private void Start()
  {
      StartCoroutine(SwitchCamera());
  }

  IEnumerator SwitchCamera()
  {
      yield return new WaitForSeconds(waitTime);

      mainCamera.enabled = false;
      secondaryCamera.enabled = true;
  }
}
