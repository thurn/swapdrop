using System;
using UnityEngine;

public class OriginalDimensions : MonoBehaviour {
  public Vector3 Position { get; set; }
}

public class Scaler {
  private double kAspectRatio = 1.7;
  private double kStatusBarHeight = 40.0; // TODO: Try and get this from the OS
  private double kWorldWidth = 300.0;

  public void Scale(Transform transform) {
    double scaleFactor = TargetGameWidth() / kWorldWidth;
 
    OriginalDimensions dimensions = transform.gameObject.GetComponent<OriginalDimensions>();
    if (dimensions == null) {
      dimensions = transform.gameObject.AddComponent<OriginalDimensions>() as OriginalDimensions;
      dimensions.Position = transform.position;
    }

    transform.position = new Vector3((float) (dimensions.Position.x * scaleFactor),
                                     (float) (dimensions.Position.y * scaleFactor),
                                     dimensions.Position.z);
  }

  public void UpdateMainCamera() {
    Debug.Log("target width " + TargetGameWidth());
    double cameraHeight = (TargetGameHeight() + kStatusBarHeight) / 2.0f;
    Camera.main.transform.position = new Vector3((float) (TargetGameWidth() / 2.0),
                                                 (float) cameraHeight,
                                                 -10.0f);
    Camera.main.orthographicSize = (float) cameraHeight;
  }

  double TargetGameWidth() {
    return Math.Floor(Math.Min(Screen.width,
        (Screen.height - kStatusBarHeight) / kAspectRatio));
  }

  double TargetGameHeight() {
    return Math.Floor(Math.Min(Screen.height - kStatusBarHeight,
        Screen.width * kAspectRatio));
  }
}
