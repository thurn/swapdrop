using System;
using UnityEngine;

public class OriginalDimensions : MonoBehaviour {
  public Vector3 Position { get; set; }
}

public class Scaler {
  private double kAspectRatio = 1.7;
  private double kStatusBarHeight = 40.0; // TODO: Try and get this from the OS
  private double worldWidth = 300.0;

  public void Scale(Transform transform) {
    double gameWidth = Math.Min(Screen.width, (Screen.height - kStatusBarHeight) / kAspectRatio);
    double scaleFactor = gameWidth / worldWidth;
 
    OriginalDimensions dimensions = transform.gameObject.GetComponent<OriginalDimensions>();
    if (dimensions == null) {
      dimensions = transform.gameObject.AddComponent<OriginalDimensions>() as OriginalDimensions;
      dimensions.Position = transform.position;
    }

    transform.position = new Vector3((float) (dimensions.Position.x * scaleFactor),
                                     (float) (dimensions.Position.y * scaleFactor),
                                     dimensions.Position.z);
  }
}
