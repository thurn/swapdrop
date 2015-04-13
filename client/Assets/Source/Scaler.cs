using UnityEngine;
using System;

public class OriginalDimensions : Component {
  public Vector3 Position { get; set; }
}

public class Scaler {
  private double kAspectRatio = 1.7;
  private double kStatusBarHeight = 40.0; // TODO: Try and get this from the OS
  private double worldWidth = 300.0;

  public void Scale(GameObject gameObject) {
    double gameWidth = Math.Min(Screen.width, (Screen.height - kStatusBarHeight) / kAspectRatio);
    double scaleFactor = gameWidth / worldWidth;
 
    OriginalDimensions dimensions = gameObject.GetComponent<OriginalDimensions>();
    if (dimensions == null) {
      dimensions = gameObject.AddComponent<OriginalDimensions>() as OriginalDimensions;
      dimensions.Position = gameObject.transform.position;
    }
 
    gameObject.transform.position = new Vector3((float) (dimensions.Position.x * scaleFactor),
                                                (float) (dimensions.Position.y * scaleFactor),
                                                dimensions.Position.z);
  }
}
